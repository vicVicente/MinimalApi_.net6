using MnimalApiCatalogo.ApiEndpoints;
using MnimalApiCatalogo.AppServicesExtensions;

var builder = WebApplication.CreateBuilder(args);

//Inclusão de serviços e configurações, com esses métodos de extensões.
builder.AddApiSwagger();
builder.AddPersistence();
builder.Services.AddCors();
builder.AddAutenticationJwt();

var app = builder.Build();

app.MapAutenticacaoEndpoints();//Endpoint Login
app.MapCategoriasEndpoints();//Endpoints Categoria
app.MapProdutosEndpoints();//Endpoints Produtos

// Este "/" é a URL padrão para o endpoint, o delegate, é o que vai se tratar o request, sendo ele assíncrono.
//O método "ExcludeFromDescription" faz com que este mapeamento não apareça na interface.
app.MapGet("/", () => "Catálogo de Produtos 2022").ExcludeFromDescription();

var environment = app.Environment;

//Obtendo o contexto e configuração para o tratamento de exeções, habilitação do middleware do swagger e a definição do cors.
app.UseExceptionHandling(environment)
    .UseSwaggerMiddleware()
    .UseAppCors();

app.UseAuthentication();
app.UseAuthorization(); 

app.Run();