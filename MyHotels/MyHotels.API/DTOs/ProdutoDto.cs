namespace MyHotels.API.DTOs;

public class ProdutoDto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string? Detalhe { get; set; }
    public decimal PrecoBase { get; set; }
    public int Disponibilidade { get; set; }
    public bool Visivel { get; set; }

    // A app recebe a categoria aninhada com o id e o nome, pronta a mostrar
    public ReferenciaDto Categoria { get; set; } = null!;

    public ReferenciaDto? ModoDisponibilizacao { get; set; }

    public byte[]? Imagem { get; set; }
}