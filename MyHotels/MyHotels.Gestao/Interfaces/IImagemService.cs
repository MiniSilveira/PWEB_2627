namespace MyHotels.Gestao.Interfaces;

public interface IImagensService
{
    long TamanhoMaximo { get; }
    string? Valida(string nome, long tamanho);
    Task<byte[]> LerBytesAsync(Stream conteudo);
    string Src(byte[]? imagem, string? nome);
}