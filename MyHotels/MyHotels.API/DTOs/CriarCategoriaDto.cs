using System.ComponentModel.DataAnnotations;

namespace MyHotels.API.DTOs;

/// <summary>Os dados necessários para criar uma nova categoria.</summary>
public class CriarCategoriaDto
{
    /// <summary>O nome da categoria. Máximo de 100 caracteres.</summary>
    /// <example>Aparthotéis</example>
    [Required(ErrorMessage = "O nome é obrigatório.")]
    [StringLength(100, ErrorMessage = "O nome tem no máximo 100 caracteres.")]
    public string Nome { get; set; } = string.Empty;

    /// <summary>A ordem de apresentação da categoria (0 a 1000).</summary>
    /// <example>3</example>
    [Range(0, 1000, ErrorMessage = "A ordem tem de estar entre 0 e 1000.")]
    public int Ordem { get; set; }

    /// <summary>A imagem da categoria, codificada em formato Base64. Apenas PNG ou JPG, até 500KB. Null se não houver imagem.</summary>
    public byte[]? Imagem { get; set; }
}