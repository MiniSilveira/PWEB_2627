using System.ComponentModel.DataAnnotations;

namespace MyHotels.API.DTOs;

/// <summary>Os dados para criar um modo de disponibilização (ex: Tudo Incluído).</summary>
public class CriarModoDisponibilizacaoDto
{
    /// <summary>O nome do modo. Máximo de 100 caracteres.</summary>
    /// <example>Meia-Pensão</example>
    [Required(ErrorMessage = "O nome é obrigatório.")]
    [StringLength(100, ErrorMessage = "O nome tem no máximo 100 caracteres.")]
    public string Nome { get; set; } = string.Empty;
}