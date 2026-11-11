using System.ComponentModel.DataAnnotations;

namespace MyHotels.Gestao.Entities;

public class Categoria
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório.")]
    [StringLength(100, ErrorMessage = "O nome tem no máximo 100 caracteres.")]
    public string? Nome { get; set; }

    [Range(0, 1000, ErrorMessage = "A ordem tem de estar entre 0 e 1000.")]
    public int Ordem { get; set; }

    [StringLength(200)]
    public string? UrlImagem { get; set; }

    public byte[]? Imagem { get; set; }

    public ICollection<Produto>? Produtos { get; set; }
}