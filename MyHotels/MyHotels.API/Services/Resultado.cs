namespace MyHotels.API.Services;

public class Resultado<T>
{
    public bool Sucesso { get; private set; }
    public T? Valor { get; private set; }
    public string? Codigo { get; private set; }
    public string? Mensagem { get; private set; }
    public int? StatusErro { get; private set; }

    public static Resultado<T> Ok(T valor) => new() { Sucesso = true, Valor = valor };
    public static Resultado<T> Ok() => new() { Sucesso = true }; // Para PUT e DELETE

    public static Resultado<T> NaoEncontrado(string codigo, string mensagem) =>
        new() { Sucesso = false, Codigo = codigo, Mensagem = mensagem, StatusErro = 404 };

    public static Resultado<T> Invalido(string codigo, string mensagem) =>
        new() { Sucesso = false, Codigo = codigo, Mensagem = mensagem, StatusErro = 400 };

    public static Resultado<T> Conflito(string codigo, string mensagem) =>
        new() { Sucesso = false, Codigo = codigo, Mensagem = mensagem, StatusErro = 409 };
}