namespace Estudo.Model;

public class Cliente
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Documento { get; set; } // Adicionado para compatibilidade com DTO
}