using MnimalApiCatalogo.ApiEndpoints;
using MnimalApiCatalogo.AppServicesExtensions;

var builder = WebApplication.CreateBuilder(args);

//Inclus�o de servi�os e configura��es, com esses m�todos de extens�es.
builder.AddApiSwagger();
builder.AddPersistence();
builder.Services.AddCors();
builder.AddAutenticationJwt();

var app = builder.Build();

app.MapAutenticacaoEndpoints();//Endpoint Login
app.MapCategoriasEndpoints();//Endpoints Categoria
app.MapProdutosEndpoints();//Endpoints Produtos

// Este "/" � a URL padr�o para o endpoint, o delegate, � o que vai se tratar o request, sendo ele ass�ncrono.
//O m�todo "ExcludeFromDescription" faz com que este mapeamento n�o apare�a na interface.
app.MapGet("/", () => "Cat�logo de Produtos 2022").ExcludeFromDescription();

var environment = app.Environment;

//Obtendo o contexto e configura��o para o tratamento de exe��es, habilita��o do middleware do swagger e a defini��o do cors.
app.UseExceptionHandling(environment)
    .UseSwaggerMiddleware()
    .UseAppCors();

app.UseAuthentication();
app.UseAuthorization(); 

app.Run();