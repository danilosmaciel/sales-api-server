<div align="center">
    <h1>
      Sales Api
    </h1>
</div>

<div align="center">
  Fornce os recursos para o app client
</div>

<div align="center">

<p>

</p>

<div align="center">

[![Status](https://img.shields.io/badge/Status-Desenvolvimento-green)]()

</div>

</div>

## Sobre

O projeto fornece o acesso a api de dados para o app cliente

### Bibliotecas utilizadas

- [AutoMapper](https://github.com/AutoMapper/AutoMapper) - Ferramenta para mapeamento de objetos
- [Authentication JwtBearer](https://www.nuget.org/packages/Microsoft.AspNetCore.Authentication.JwtBearer) - Para trabalhar com autenticação de usuário via  jwrBearer
- [EntityFramework Core](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore) - Orm
- [NewtonsoftJson](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore) - Fornece funcionalidades para manipular json
- [OpenApi](https://www.nuget.org/packages/Microsoft.AspNetCore.OpenApi) - Para documentar e fornecer recursos para a api
- [Swagger](https://swagger.io/solutions/getting-started-with-oas/) - Facilita implementação do OpenAi
- [Sqlite](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.Sqlite)- Adiciona ao EntityFramework a capacidade de trabalhar com banco de dados relacional SQLite
- [Identity](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.Sqlite) - Adiciona funcionalidades que abstraem a criação e validações de usuáios.

## Abrir e rodar

**Para executar este projeto você precisa:**

1. Ter o [Dotnetcore 6](https://dotnet.microsoft.com) instalado na sua máquina. <br/>
2. Ter alguma IDE como o [vsCode](https://code.visualstudio.com/). <br/>
3. Ter a porta 5041 disponível em sua máquina
4. Baixar e instalar o [Git](https://git-scm.com/downloads). <br/>
5. Abrir o Git e clonar o projeto do [GitHub]([https://gitlab.com/physical-solutions/app-physical](https://github.com/danilosmaciel/sales-api-server)) usando o comando `git clone `. <br/>
6. Ao abrir o projeto, execute o comando `dotnet run` na raiz do projeto ou clique ou do botão play de sua ide. As dependências serão baixadas e o banco será criado no diretório Database, na raiz da aplicação <br/>
7. Depois disso a api ficará disponível.
8. Assim que a api estiver disponível acesse o endereço http://localhost:5041/swagger no browser, para acessar o Swagger.
9. Antes de utilizar o client(https://github.com/danilosmaciel/sales-api-client) ou mesmo os recursos via Swagger, será necessário criar um usuário no endpoint /api/user/create.
10. Para usar o Swagger, realize o login com o usuário criado em /api/user/signin que irá devolver um token que deve ser utilizado no botão "Authorize" no topo da página, para autorizar deve se utilizar no campo especifico o formato "Bearer " + o token que a api gerou no login, a patir disso os endpoints ficarão acessíveis.
   


[Voltar ao topo](#physical-solutions)<br>
