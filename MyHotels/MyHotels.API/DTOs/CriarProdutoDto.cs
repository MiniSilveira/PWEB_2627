using System.ComponentModel.DataAnnotations;

namespace MyHotels.API.DTOs;

/// <summary>Os dados para criar um novo alojamento.</summary>
public class CriarProdutoDto : IValidatableObject
{
    [Required(ErrorMessage = "O nome é obrigatório.")]
    [StringLength(100, ErrorMessage = "O nome tem no máximo 100 caracteres.")]
    public string Nome { get; set; } = string.Empty;

    [StringLength(500)]
    public string? Detalhe { get; set; }

    /// <summary>O preço base, maior do que zero, com duas casas decimais no máximo.</summary>
    /// <example>150.00</example>
    [Required(ErrorMessage = "O preço é obrigatório.")]
    [Range(0.01, 99999999.99, ErrorMessage = "O preço tem de ser maior do que zero.")]
    public decimal PrecoBase { get; set; }

    [Range(0, 9999999, ErrorMessage = "A disponibilidade não pode ser negativa.")]
    public int Disponibilidade { get; set; }

    public bool Visivel { get; set; } = true;

    /// <summary>A categoria do alojamento.</summary>
    /// <example>1</example>
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "A categoria é obrigatória.")]
    public int CategoriaId { get; set; }

    public int? ModoDisponibilizacaoId { get; set; }

    public byte[]? Imagem { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (Visivel && Disponibilidade <= 0)
        {
            yield return new ValidationResult("Um alojamento sem disponibilidade não pode estar visível para reserva.", new[] { nameof(Visivel) });
        }
    }
}