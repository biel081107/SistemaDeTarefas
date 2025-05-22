using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using _2MVC.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
namespace _2MVC.Controllers;

public class ContatoController : Controller
{
    private readonly ILogger<ProjetoController> _logger;

    public ContatoController(ILogger<ProjetoController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }
}