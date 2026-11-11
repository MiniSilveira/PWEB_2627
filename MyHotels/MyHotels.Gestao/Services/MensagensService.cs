using MyHotels.Gestao.Interfaces;

namespace MyHotels.Gestao.Services;

public class MensagensService : IMensagensService
{
    public string? Mensagem { get; private set; }
    public bool Sucesso { get; private set; }
    public event Action? OnChange;

    public void Mostrar(string mensagem, bool sucesso = true)
    {
        Mensagem = mensagem;
        Sucesso = sucesso;
        OnChange?.Invoke();
    }

    public void Limpar()
    {
        Mensagem = null;
        OnChange?.Invoke();
    }
}