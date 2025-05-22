using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using _2MVC.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;

namespace _2MVC.Controllers;

public class TarefasController : Controller
{
    private readonly ILogger<ProjetoController> _logger;
    private readonly GestaoContext _context;

    public TarefasController(ILogger<ProjetoController> logger, GestaoContext context)
    {
        _logger = logger;
        _context = context;
    }
    public IActionResult TarefasAdicionar(int idProjeto)
    {
        ViewBag.IdProjeto = idProjeto;
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> TarefasAdicionar(Tarefa tarefa)
    {
        if (ModelState.IsValid)
        {
            await _context.Tarefas.AddAsync(tarefa);
            await _context.SaveChangesAsync();
            return RedirectToAction("ProjetosDetalhesGet", "Projeto", new { IdProjeto = tarefa.IdProjeto });
        }
        return View(tarefa);
    }

    [HttpPost]
    public async Task<IActionResult> TarefasEditar(Tarefa tarefa)
    {
        if (ModelState.IsValid)
        {
            var tarefaX = await _context.Tarefas.FindAsync(tarefa.IdTarefa);
            if (tarefaX == null)
            {
                return NotFound();
            }

            tarefaX.Titulo = tarefa.Titulo;
            tarefaX.Descricao = tarefa.Descricao;
            tarefaX.DataEntrega = tarefa.DataEntrega;
            tarefaX.Status = tarefa.Status;
            tarefaX.IdProjeto = tarefa.IdProjeto;
            await _context.SaveChangesAsync();
            return RedirectToAction("ProjetosDetalhesGet", "Projeto", new { IdProjeto = tarefa.IdProjeto });
        }
        return RedirectToAction("ProjetosIndex", "Projeto");
    }

    [HttpPost]
    public async Task<IActionResult> TarefasConcluir(int IdTarefa)
    {
        if (ModelState.IsValid)
        {
            var tarefaConcluida = await _context.Tarefas.FindAsync(IdTarefa);
            if (tarefaConcluida != null)
            {
                tarefaConcluida.Status = 1; // Concluido
                await _context.SaveChangesAsync();
                return RedirectToAction("ProjetosDetalhesGet", "Projeto", new { IdProjeto = tarefaConcluida.IdProjeto });
            }

        }
        return NotFound();
    }
    [HttpPost]
    public async Task<IActionResult> TarefasAtivar(int IdTarefa)
    {
        if (ModelState.IsValid)
        {
            var tarefaConcluida = await _context.Tarefas.FindAsync(IdTarefa);
            if (tarefaConcluida != null)
            {
                tarefaConcluida.Status = 0; // Concluido
                await _context.SaveChangesAsync();
                return RedirectToAction("ProjetosDetalhesGet", "Projeto", new { IdProjeto = tarefaConcluida.IdProjeto });
            }

        }
        return NotFound();
    }
    [HttpPost]
    public async Task<IActionResult> TarefasCancelar(int IdTarefa)
    {
        if (ModelState.IsValid)
        {
            var tarefaConcluida = await _context.Tarefas.FindAsync(IdTarefa);
            if (tarefaConcluida != null)
            {
                tarefaConcluida.Status = 2; // Concluido
                await _context.SaveChangesAsync();
                return RedirectToAction("ProjetosDetalhesGet", "Projeto", new { IdProjeto = tarefaConcluida.IdProjeto });
            }

        }
        return NotFound();
    }
    [HttpPost]
    public async Task<IActionResult> TarefasExcluir(int IdTarefa)
    {
        if (ModelState.IsValid)
        {
            var tarefaConcluida = await _context.Tarefas.FindAsync(IdTarefa);
            if (tarefaConcluida != null)
            {
                _context.Tarefas.Remove(tarefaConcluida);
                await _context.SaveChangesAsync();
                return RedirectToAction("ProjetosDetalhesGet", "Projeto", new { IdProjeto = tarefaConcluida.IdProjeto });
            }

        }
        return NotFound();
    }

}