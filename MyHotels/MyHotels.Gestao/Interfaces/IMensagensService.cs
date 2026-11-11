namespace MyHotels.Gestao.Interfaces;

public interface IMensagensService
{
    string? Mensagem { get; }
    bool Sucesso { get; }
    void Mostrar(string mensagem, bool sucesso = true);
    void Limpar();
    event Action OnChange;
}