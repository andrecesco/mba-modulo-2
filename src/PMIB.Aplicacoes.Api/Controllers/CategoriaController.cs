using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PMIB.Aplicacoes.Api.Dtos;
using PMIB.Core.Business.Dtos;
using PMIB.Core.Business.Interfaces;
using PMIB.Core.Business.Models;
using PMIB.Core.Business.Requests;
using System.Collections.Generic;

namespace PMIB.Aplicacoes.Api.Controllers;

[Authorize]
[Route("api/categorias")]
public class CategoriasController(ICategoriaService categoriaService,
                            IMapper mapper) : MainController
{
    [AllowAnonymous]
    [HttpGet("{id:Guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CategoriaDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> ObterPorId(Guid id)
    {
        var categoria = await categoriaService.ObterPorId(id);

        if (categoria == null)
        {
            return NotFound();
        }

        return CustomResponse(categoria);
    }

    [AllowAnonymous]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<CategoriaDto>))]
    public async Task<IActionResult> ObterTodas()
    {
        var categorias = await categoriaService.ObterTodos();

        return CustomResponse(categorias.ToList());
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Criar(CategoriaRequest request)
    {
        var categoria = await categoriaService.Adicionar(request);

        if (!OperacaoValida())
        {
            return CustomResponse();
        }

        return CreatedAtAction(nameof(ObterPorId), new { categoriaModel.Id }, request);
    }

    [HttpPut("{id:Guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> PutCategoria(Guid id, Models.CategoriaViewModel categoria)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(new ValidationProblemDetails(ModelState)
            {
                Title = "Ocorreu um ou mais erros ao tentar editar a categoria!"
            });
        }

        var categoriaBd = await categoriaRepositorio.ObterPorId(id);
        if (categoriaBd == null)
            return NotFound();

        mapper.Map(categoria, categoriaBd);

        try
        {
            await categoriaRepositorio.Atualizar(categoriaBd);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (await categoriaRepositorio.ObterPorId(id) == null)
            {
                return NotFound();
            }

            throw;
        }

        return NoContent();
    }

    [HttpDelete("{id:Guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> DeleteCategoria(Guid id)
    {
        var categoriaBd = await categoriaRepositorio.ObterCategoriaProduto(id);
        if (categoriaBd == null)
            return NotFound();

        if (categoriaBd.Produtos.Any())
            return Problem("Não é possível excluir uma categoria com produtos associados");

        await categoriaRepositorio.Remover(id);
        return NoContent();
    }

    private ActionResult<ProdutoViewModel> ReturnValidationProblem()
    {
        return ValidationProblem(new ValidationProblemDetails(ModelState)
        { Title = "Ocorreu um ou mais erros ao enviar informações da categoria" });
    }
}
