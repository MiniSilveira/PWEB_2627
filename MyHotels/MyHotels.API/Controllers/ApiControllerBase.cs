using Microsoft.AspNetCore.Mvc;
using MyHotels.API.Services;

namespace MyHotels.API.Controllers;

[ApiController]
public abstract class ApiControllerBase : ControllerBase // ControllerBase em vez de Controller porque a API não devolve HTML/Views[cite: 46]
{
    // Traduz o Resultado do Service para o respetivo Status Code HTTP[cite: 59, 90]
    protected ActionResult Erro<T>(Resultado<T> resultado)
    {
        // Guarda o código de erro para o ErrosApi apanhar e enviar no ProblemDetails[cite: 61]
        HttpContext.Items["codigoErro"] = resultado.Codigo;

        return resultado.StatusErro switch
        {
            404 => NotFound(),
            400 => ValidationProblem(),
            409 => Problem(resultado.Mensagem, statusCode: 409),
            _ => Problem(resultado.Mensagem, statusCode: 500)
        };
    }
}