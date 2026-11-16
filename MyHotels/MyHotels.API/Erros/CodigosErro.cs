namespace MyHotels.API.Erros;

public static class CodigosErro
{
    public const string DadosInvalidos = "DADOS_INVALIDOS";
    public const string NaoEncontrado = "NAO_ENCONTRADO";
    public const string Conflito = "CONFLITO";
    public const string ErroInterno = "ERRO_INTERNO";

    public const string CategoriaNaoEncontrada = "CATEGORIA_NAO_ENCONTRADA";
    public const string CategoriaNomeRepetido = "CATEGORIA_NOME_REPETIDO";
    public const string CategoriaComProdutos = "CATEGORIA_COM_PRODUTOS";

    public const string ModoNaoEncontrado = "MODO_DISPONIBILIZACAO_NAO_ENCONTRADO";
    public const string ModoNomeRepetido = "MODO_DISPONIBILIZACAO_NOME_REPETIDO";
    public const string ModoEmUso = "MODO_DISPONIBILIZACAO_EM_USO";

    public const string ProdutoNaoEncontrado = "PRODUTO_NAO_ENCONTRADO";
    public const string ProdutoNomeRepetido = "PRODUTO_NOME_REPETIDO";

    public const string ImagemInvalida = "IMAGEM_INVALIDA";
    public const string ImagemDemasiadoGrande = "IMAGEM_DEMASIADO_GRANDE";

    public const string GravacaoRecusada = "GRAVACAO_RECUSADA";
}