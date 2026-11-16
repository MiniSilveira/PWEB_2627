using System.ComponentModel.DataAnnotations;

namespace MyHotels.API.DTOs;

/// <summary>Os dados necessários para alterar uma categoria existente. O id viaja no URL.</summary>
public class AtualizarCategoriaDto
{
    [Required(ErrorMessage = "O nome é obrigatório.")]
    [StringLength(100, ErrorMessage = "O nome tem no máximo 100 caracteres.")]
    public string Nome { get; set; } = string.Empty;

    [Range(0, 1000, ErrorMessage = "A ordem tem de estar entre 0 e 1000.")]
    public int Ordem { get; set; }

    public byte[]? Imagem { get; set; }
}