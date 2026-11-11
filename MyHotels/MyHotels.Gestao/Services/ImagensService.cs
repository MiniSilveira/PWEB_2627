using MyHotels.Gestao.Interfaces;

namespace MyHotels.Gestao.Services;

public class ImagensService : IImagensService
{
    public long TamanhoMaximo => 500 * 1024; // Máximo de 500 KB conforme regras

    public string? Valida(string nome, long tamanho)
    {
        if (tamanho > TamanhoMaximo) return "A imagem é demasiado grande (máximo 500 KB).";
        if (!nome.EndsWith(".png") && !nome.EndsWith(".jpg") && !nome.EndsWith(".jpeg")) return "A imagem tem de ser PNG ou JPG.";
        return null;
    }

    public async Task<byte[]> LerBytesAsync(Stream conteudo)
    {
        using var memoryStream = new MemoryStream();
        await conteudo.CopyToAsync(memoryStream);
        return memoryStream.ToArray();
    }

    public string Src(byte[]? imagem, string? nome)
    {
        if (imagem == null || imagem.Length == 0) return "/img/default.png";
        string format = nome?.EndsWith(".png") == true ? "image/png" : "image/jpeg";
        return $"data:{format};base64,{Convert.ToBase64String(imagem)}";
    }
}