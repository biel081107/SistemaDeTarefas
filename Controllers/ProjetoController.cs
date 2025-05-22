using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using _2MVC.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;

namespace _2MVC.Controllers;

public class ProjetoController : Controller
{
    private readonly ILogger<ProjetoController> _logger;
    private readonly GestaoContext _context;

    public ProjetoController(ILogger<ProjetoController> logger, GestaoContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }
    public async Task<IActionResult> ProjetosIndex()
    {
        var projetos = await _context.Projetos.Where(p => p.Status == 0).ToListAsync();
        return View(projetos);
    }
    public IActionResult ProjetosAdicionar()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> ProjetosAdicionar(Projeto projeto)
    {
        if (ModelState.IsValid)
        {
            await _context.Projetos.AddAsync(projeto);
            await _context.SaveChangesAsync();
            return RedirectToAction("ProjetosIndex");
        }
        return View(projeto);
    }

    [HttpPost]
    public async Task<IActionResult> ProjetosCancelar(int Idprojeto)
    {
        //EU NÃO EXCLUO OS PROJETOS, APENAS MUDO O STATUS PARA 2
        Console.WriteLine(Idprojeto);
        if (ModelState.IsValid)
        {
            
            var projetoDelete = await _context.Projetos.FindAsync(Idprojeto);
            if (projetoDelete != null)
            {
                projetoDelete.Status = 2; // Cancelado
                await _context.SaveChangesAsync();
            }
        }
        return RedirectToAction("ProjetosIndex");
    }

    [HttpGet("ProjetosDetalhes/{idProjeto}")]
    public async Task<IActionResult> ProjetosDetalhesGet(int IdProjeto)
    {
        var projeto = await _context.Projetos.FindAsync(IdProjeto);
        if (projeto == null)
        {
            return NotFound();
        }
        var tarefas = await _context.Tarefas.Where(t => t.IdProjeto == IdProjeto).ToListAsync();
        ViewBag.Tarefas = tarefas;
        return View("ProjetosDetalhes",projeto);
    }

    [HttpPost]
    public async Task<IActionResult> ProjetosDetalhes(int IdProjeto)
    {
        if (ModelState.IsValid)
        {

            var projeto = await _context.Projetos.FindAsync(IdProjeto);
            if (projeto != null)
            {
                var tarefas = await _context.Tarefas.Where(t => t.IdProjeto == IdProjeto).ToListAsync();
                ViewBag.Tarefas = tarefas;
                return View(projeto);
            }
        }
        return RedirectToAction("ProjetosIndex");
    }

    

    [HttpPost]
    public async Task<IActionResult> ProjetosEditar(Projeto projeto)
    {
        if (ModelState.IsValid)
        {
            var projetoEdit = await _context.Projetos.FindAsync(projeto.IdProjeto);
            if (projetoEdit != null)
            {
                projetoEdit.Nome = projeto.Nome;
                projetoEdit.Descricao = projeto.Descricao;
                projetoEdit.DataInicio = projeto.DataInicio;
                projetoEdit.DataFim = projeto.DataFim;
                await _context.SaveChangesAsync();
            }
        }
        return RedirectToAction("ProjetosIndex");
    }

    [HttpPost]
    public async Task<IActionResult> ProjetosConcluir(int IdProjeto)
    {
        if (ModelState.IsValid)
        {
            var projeto = await _context.Projetos.FindAsync(IdProjeto);
            if (projeto != null)
            {
                projeto.Status = 1; // Concluído
                await _context.SaveChangesAsync();
            }
        }
        return RedirectToAction("ProjetosIndex");
    }

    public async Task<IActionResult> ProjetosConcluido()
    {
        var projetos = await _context.Projetos.Where(p => p.Status == 1).ToListAsync();
        return View(projetos);
    }

    public async Task<IActionResult> ProjetosCancelados()
    {
        var projetos = await _context.Projetos.Where(p => p.Status == 2).ToListAsync();
        return View(projetos);
    }

    [HttpPost]
    public async Task<IActionResult> ProjetosExcluir(int IdProjeto)
    {
        if (ModelState.IsValid)
        {
            var projeto = await _context.Projetos.FindAsync(IdProjeto);
            if (projeto != null)
            {
                _context.Projetos.Remove(projeto);
                await _context.SaveChangesAsync();
            }
        }
        return RedirectToAction("ProjetosIndex");
    }


    public async Task<IActionResult> ProjetosAtivar(int IdProjeto)
    {
        if (ModelState.IsValid)
        {
            var projeto = await _context.Projetos.FindAsync(IdProjeto);
            if (projeto != null)
            {
                projeto.Status = 0; // Em Andamento
                await _context.SaveChangesAsync();
            }
        }
        return RedirectToAction("ProjetosIndex");
    }
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
