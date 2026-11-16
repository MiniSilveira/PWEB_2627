namespace MyHotels.API.Services;

public static class Imagens
{
    public const int TamanhoMaximo = 2 * 1024 * 1024; // 2 MB

    public static bool FormatoValido(byte[] bytes)
    {
        if (bytes.Length < 4) return false;

        // Verifica a "assinatura mágica" dos bytes do ficheiro
        var png = bytes[0] == 0x89 && bytes[1] == 0x50 && bytes[2] == 0x4E && bytes[3] == 0x47;
        var jpeg = bytes[0] == 0xFF && bytes[1] == 0xD8 && bytes[2] == 0xFF;
        var gif = bytes[0] == 0x47 && bytes[1] == 0x49 && bytes[2] == 0x46;
        var webp = bytes[0] == 0x52 && bytes[1] == 0x49 && bytes[2] == 0x46 && bytes[3] == 0x46;

        return png || jpeg || gif || webp;
    }
}