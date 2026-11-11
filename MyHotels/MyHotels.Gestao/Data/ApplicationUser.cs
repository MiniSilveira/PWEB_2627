using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using MyHotels.Gestao.Entities;

namespace MyHotels.Gestao.Data;

public class ApplicationUser : IdentityUser
{
    [StringLength(100)]
    public string? Nome { get; set; }

    // Relação com a Morada (Opcional, pois no registo inicial pode não ter morada)
    public int? MoradaId { get; set; }
    public Morada? morada { get; set; }

    // Validação personalizada do NIF adicionada conforme a Ficha 5
    [validarNIF(ErrorMessage = "O NIF é inválido.")]
    public long? NIF { get; set; }

    [StringLength(20)]
    public string Estado { get; set; } = "Pendente";
}

// O algoritmo de validação do NIF usado pelas Finanças (módulo 11)
public class validarNIF : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null)
        {
            return new ValidationResult("O NIF é obrigatório.");
        }

        if (value is not long nif || nif.ToString().Length != 9)
        {
            return new ValidationResult("O NIF deve ter exatamente 9 dígitos.");
        }

        if (!IsValidNIF(nif))
        {
            return new ValidationResult("O NIF é inválido.");
        }

        return ValidationResult.Success;
    }

    private bool IsValidNIF(long nif)
    {
        string nifString = nif.ToString();
        int primeiroDigito = int.Parse(nifString[0].ToString());
        if (primeiroDigito < 1 || primeiroDigito > 9) return false;

        int total = 0;
        for (int i = 0; i < 8; i++)
        {
            total += int.Parse(nifString[i].ToString()) * (9 - i);
        }

        int digitoDeControlo = 11 - (total % 11);
        if (digitoDeControlo >= 10) digitoDeControlo = 0;

        return digitoDeControlo == int.Parse(nifString[8].ToString());
    }
}