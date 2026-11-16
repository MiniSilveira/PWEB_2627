using System.ComponentModel.DataAnnotations;

namespace MyHotels.API.Entities;

public class ModoDisponibilizacao
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Nome { get; set; } = string.Empty;

    public ICollection<Produto>? Produtos { get; set; }
}