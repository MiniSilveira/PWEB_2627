using System.ComponentModel.DataAnnotations;

namespace MyHotels.Gestao.Entities;

public class Morada
{
    public int Id { get; set; }

    [StringLength(100)]
    public string? Rua { get; set; }

    [StringLength(100)]
    public string? Localidade { get; set; }

    [StringLength(100)]
    public string? Cidade { get; set; }

    [StringLength(8)]
    [RegularExpression(@"^\d{4}-\d{3}$", ErrorMessage = "O código postal deve estar no formato '1234-567'.")]
    public string? CodigoPostal { get; set; }

    [StringLength(100)]
    public string? Pais { get; set; }
}