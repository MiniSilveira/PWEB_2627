using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MyHotels.API.Data;
using MyHotels.API.Erros;
using MyHotels.API.Repositories;
using MyHotels.API.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// 1. Serviços: o que o contentor sabe criar[cite: 57, 89]
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

// Repositories (Acesso a dados)[cite: 56, 89]
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<IModoDisponibilizacaoRepository, ModoDisponibilizacaoRepository>();
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();

// Services (Regras de negócio)[cite: 56, 89]
builder.Services.AddScoped<ICategoriaService, CategoriaService>();
builder.Services.AddScoped<IModoDisponibilizacaoService, ModoDisponibilizacaoService>();
builder.Services.AddScoped<IProdutoService, ProdutoService>();

builder.Services.AddControllers();

// Ligar o formato único dos erros para respostas mais limpas e padronizadas[cite: 61, 89]
builder.Services.AddProblemDetails(o =>
    o.CustomizeProblemDetails = ErrosApi.Personalizar);

// CORS - Permitir que a página web frontend consiga chamar a API sem ser bloqueada pelo browser[cite: 102]
builder.Services.AddCors(o => o.AddDefaultPolicy(p => p
    .AllowAnyOrigin() // Em desenvolvimento aceita de qualquer lado. Em produção restringe-se[cite: 102]
    .AllowAnyHeader()
    .AllowAnyMethod()));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "MyHotels API", Version = "v1" });

    // Lê os comentários XML (os ///) para documentar o Swagger[cite: 82, 89]
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }
});

var app = builder.Build();

// 2. Pipeline: por onde passa cada pedido, por ordem[cite: 57, 89]
app.UseExceptionHandler(); // Transforma exceções (erro 500) no formato ProblemDetails[cite: 61, 89]
app.UseStatusCodePages();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(); // Tem de estar antes da autorização[cite: 102]

app.UseAuthorization();

app.MapControllers(); // Encaminha o pedido para o método HTTP certo[cite: 57]

// Sem base de dados, a API não arranca (evita erros em runtime se a BD estiver desligada)[cite: 57, 84]
await VerificacaoBaseDados.ExecutarAsync(app);

app.Run();