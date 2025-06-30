using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PMIB.Aplicacoes.Api.Extensions;
using PMIB.Aplicacoes.Api.ViewModels;
using PMIB.Core.Business.Interfaces;
using PMIB.Core.Business.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PMIB.Aplicacoes.Api.Controllers;

[ApiController]
[Route("api/conta")]
public class AuthController(SignInManager<IdentityUser> signInManager,
                       UserManager<IdentityUser> userManager,
                       IOptions<JwtSettings> jwtSettings,
                       IVendedorRepositorio vendedorRepositorio
        ) : ControllerBase
{
    private readonly SignInManager<IdentityUser> _signInManager = signInManager;
    private readonly UserManager<IdentityUser> _userManager = userManager;
    private readonly JwtSettings _jwtSettings = jwtSettings.Value;
    private readonly IVendedorRepositorio _vendedorRepositorio = vendedorRepositorio;

    [HttpPost("registrar")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Registrar(RegisterUserViewModel registerUserViewModel)
    {
        if (!ModelState.IsValid)
            return ValidationProblem(new ValidationProblemDetails(ModelState)
            { Title = "Ocorreu um erro ao cadastrar o usuário!" });

        var user = new IdentityUser()
        {
            UserName = registerUserViewModel.Email,
            Email = registerUserViewModel.Email,
            EmailConfirmed = true
        };

        IdentityResult result = await _userManager.CreateAsync(user, registerUserViewModel.Password);
        if (result.Succeeded)
        {
            await _signInManager.SignInAsync(user, false);

            await _vendedorRepositorio.Adicionar(new Vendedor()
            {
                Id = Guid.Parse(user.Id),
                Nome = registerUserViewModel.Name,
                Email = registerUserViewModel.Email
            });

            return Ok(GerarJwt(registerUserViewModel.Email));
        }

        var errors = result.Errors
                    .GroupBy(e => e.Code)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(e => e.Description).ToArray()
                    );

        return ValidationProblem(new ValidationProblemDetails(errors)
        { Title = "Ocorreu um erro ao cadastrar o usuário!" });

    }

    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Login(UserLoginViewModel userLoginViewModel)
    {
        if (!ModelState.IsValid)
            return ValidationProblem(new ValidationProblemDetails(ModelState)
            { Title = "Ocorreu um erro ao cadastrar o usuário!" });

        var result = await _signInManager.PasswordSignInAsync(userLoginViewModel.Email, userLoginViewModel.Password, false, true);
        if (result.Succeeded)
        {
            return Ok(GerarJwt(userLoginViewModel.Email));
        }

        return Problem("Usuário ou senha inválidos!");
    }

    private string GerarJwt(string email)
    {
        var user = _userManager.FindByEmailAsync(email);
        var claims = new[]
        {
                new Claim(ClaimTypes.NameIdentifier, user.Result.Id)
            };

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtSettings.Segredo);

        var token = tokenHandler.CreateToken(new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(claims),
            Issuer = _jwtSettings.Emissor,
            Audience = _jwtSettings.Audiencia,
            Expires = DateTime.UtcNow.AddHours(_jwtSettings.ExpiracaoHoras),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)

        });

        var encodedToken = tokenHandler.WriteToken(token);
        return encodedToken;
    }
}