using Microsoft.AspNetCore.Mvc;

namespace PMIB.Aplicacoes.Api.Controllers;

public class CategoriaController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
