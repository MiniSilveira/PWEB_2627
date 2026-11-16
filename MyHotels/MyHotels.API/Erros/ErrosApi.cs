using Microsoft.AspNetCore.Mvc;

namespace MyHotels.API.Erros;

public static class ErrosApi
{
    public static void Personalizar(ProblemDetailsContext contexto)
    {
        var pd = contexto.ProblemDetails;
        var (titulo, codigo, detalhe) = pd.Status switch
        {
            400 => ("Os dados enviados têm erros.", CodigosErro.DadosInvalidos, "Corrija os campos assinalados e tente outra vez."),
            404 => ("Não encontrado.", CodigosErro.NaoEncontrado, "O recurso pedido não existe."),
            409 => ("Conflito com os dados existentes.", CodigosErro.Conflito, "A operação não pode ser concluída com o estado atual dos dados."),
            _ => ("Erro interno do servidor.", CodigosErro.ErroInterno, "Ocorreu um erro inesperado.")
        };

        pd.Title = titulo;
        if (pd.Status >= 500 || pd.Detail is null) pd.Detail = detalhe;

        // Traz o código específico gerado pelo nosso Service (se houver)[cite: 61]
        if (contexto.HttpContext.Items["codigoErro"] is string c) codigo = c;
        pd.Extensions["codigo"] = codigo;
        pd.Extensions["traceId"] = contexto.HttpContext.TraceIdentifier;
    }
}