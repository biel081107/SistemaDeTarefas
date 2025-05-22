namespace _2MVC.Models;

public class Projeto
{
    public int IdProjeto { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public DateTime DataInicio { get; set; }
    public DateTime? DataFim { get; set; }
    public int Status { get; set; } // 0 - Em Andamento, 1 - Concluido, 2 - Cancelado

    public List<Tarefa> Tarefas { get; set; } = new List<Tarefa>();
}