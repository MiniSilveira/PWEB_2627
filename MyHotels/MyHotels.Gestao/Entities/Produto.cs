using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyHotels.Gestao.Entities;

public class Produto : IValidatableObject
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório.")]
    [StringLength(100, ErrorMessage = "O nome tem no máximo 100 caracteres.")]
    public string? Nome { get; set; }

    [StringLength(500)]
    public string? Detalhe { get; set; }

    [StringLength(200)]
    public string? UrlImagem { get; set; }

    public byte[]? Imagem { get; set; }

    [Required(ErrorMessage = "O preço é obrigatório.")]
    [Column(TypeName = "decimal(10,2)")]
    [Range(0.01, 99999999.99, ErrorMessage = "O preço tem de ser maior do que zero.")]
    public decimal PrecoBase { get; set; }

    [Range(0, 9999999, ErrorMessage = "A disponibilidade não pode ser negativa.")]
    public int Disponibilidade { get; set; }

    public bool Visivel { get; set; } = true;

    public int CategoriaId { get; set; }
    public Categoria? Categoria { get; set; }

    public int? ModoDisponibilizacaoId { get; set; }
    public ModoDisponibilizacao? ModoDisponibilizacao { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (Visivel && Disponibilidade <= 0)
        {
            yield return new ValidationResult("Um alojamento sem disponibilidade não pode estar visível para reserva.", new[] { nameof(Visivel) });
        }
    }
}