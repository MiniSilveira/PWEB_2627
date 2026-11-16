namespace MyHotels.API.DTOs;

public class CategoriaDto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public int Ordem { get; set; }
    public byte[]? Imagem { get; set; } // O array de bytes vai em Base64
}