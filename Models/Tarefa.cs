namespace _2MVC.Models;

public class Tarefa
{
    public int IdTarefa { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public DateTime DataEntrega { get; set; }
    public int Status { get; set; } // 0 - Em Andamento, 1 - Concluido, 2 - Cancelado
    public int IdProjeto { get; set; }
    public Projeto? Projeto { get; set; }
}