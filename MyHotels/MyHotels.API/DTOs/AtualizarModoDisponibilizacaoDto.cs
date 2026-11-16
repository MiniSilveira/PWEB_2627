using System.ComponentModel.DataAnnotations;

namespace MyHotels.API.DTOs;

public class AtualizarModoDisponibilizacaoDto
{
    [Required(ErrorMessage = "O nome é obrigatório.")]
    [StringLength(100, ErrorMessage = "O nome tem no máximo 100 caracteres.")]
    public string Nome { get; set; } = string.Empty;
}
