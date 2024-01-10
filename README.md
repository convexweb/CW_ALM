# Template para dev de app Blazor WASM (FluentUI) e Web API

O principal objetivo desse template é fornecer uma base para desenvolvimento de Blazor Wasm App, baseado em FluentUI Blazor com comunicação com uma WEB API independente, utilizando conceitos de desenvolvimento orientado a domínio. Esse template foi desenvolvido usando a "Database First".

Se você achar que esse projeto é útil, fique a vontade para usar e contribuir com sugestões (há muito o que melhorar nele). Obrigado!

# Antes de tudo
 * `Restaure o .bak do banco de dados do projeto, veja a pasta "Database BKP"` (recomendo usar SQL Server 2019)
 * `Certifique-se de que o .NET8 está instalado`
 * `Abra o projeto no Visual Studio 2022`
 * `Configure "Multiple startup projects" iniciando CW_ALM.Fluent e CW_ALM.WebAPI`
 * `Defina a string de conexão com o banco de dados no arquivo API/CW_ALM.WebAPI/appsettings.json`
 * O projeto "Client.CW_ALM.Client" pode ser ignorado/removido, ele foi criado antes da adoção do FluentUI-Blazor

# Tradução para outros idiomas
Utilize o projeto "Shared.CW_ALM.Resources" para editar os arquivos *.resx. Criei uma convenção de nomear os recursos da seguinte forma: 
* "Resx_UI" para recursos usados na interface do usuário, veja projeto "Client.CW_ALM.Fluent"
* "Resx_Domain" para recursos usados no domínio, veja projetos "Shared.CW_ALM.Domain" e "Shared.CW_ALM.Infra"
* "Resx_API" para recursos usados na API, veja projeto "API.CW_ALM.WebAPI" foi criado mas ainda não foi usado

# Database First
Pessoalmente prefiro criar o banco de dados antes de escrever as entidades do domínio, isso é uma preferência pessoal. Para isso executo o script, usando o "Package Manage Console", indicado no arquivo "ScaffoldSintax" do projeto "ProjetoAuxiliar" para que o EF faça a engenharia reversa do meu banco e me entregue a estrutura pronta. Como disse, isso é uma escolha pessoal. Fique a vontade para criar suas entidades usando o conceito de "Code First"

# Tecnologias
* [ASP.NET Core 8](https://docs.microsoft.com/en-us/aspnet/core/introduction-to-aspnet-core)
* [Blazor](https://dotnet.microsoft.com/en-us/apps/aspnet/web-apps/blazor)
* [FluentUI Blazor](https://github.com/microsoft/fluentui-blazor?tab=readme-ov-file)
* [Entity Framework Core 8](https://docs.microsoft.com/en-us/ef/core/)
* [MediatR](https://github.com/jbogard/MediatR)
* [FluentValidation](https://fluentvalidation.net/)

