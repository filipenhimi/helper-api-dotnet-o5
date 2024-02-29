# helper-api-dotnet-o5

HELPER OPENED API

# Grupo 12
- Wagner Cipriano

## API

A API utilizada fornece informações da liga de basquete americano (NBA):

- `https://www.balldontlie.io/`
- `https://api.balldontlie.io/v1/teams`

Esta API _necessita KEY_, mas não precisamos nem usar dados reais para criar, foi criada com email de teste sem problemas, conforme dados abaixo:

```
Email: teste@gmail.com
API Key: 5acf76f6-e117-4641-8981-64cb78b460e6
```

### Endpoints utilizados

- https://api.balldontlie.io/v1/teams
- https://api.balldontlie.io/v1/games?period=4&seasons[]=2023

### Restrições

- A versão gratuita disponibiliza apenas dados da temporada atual (`seasons[]=2023`)
- Esta API possui uma restrição de 30 requisições por minuto, conforme print abaixo:

![Dados de acesso API NBA - https://new.balldontlie.io/signup](./Acesso_API_NBA.png)

## Importante

Em um projeto real, esta Key da API **NUNCA** deveria ficar chapada no código, deveria claro ficar em uma _variável de ambiente_, arquivo .env.
Porém para evitar causar "dificuldades" para os colegas que forem testar, ficou _hard coded_ mesmo.

## REFS:

- [API discoverability with Swagger and Swashbuckle](https://learn.microsoft.com/en-us/samples/dotnet/aspnetcore.docs/getstarted-swashbuckle-aspnetcore/?tabs=visual-studio-code)
- [Swashbuckle.AspNetCore Repo](https://github.com/domaindrivendev/Swashbuckle.AspNetCore#swashbuckleaspnetcoreannotations)
- [Enum representation in C# Web APIs & Swagger with JsonStringEnumConverter](https://www.linkedin.com/pulse/simplifying-enum-representation-c-web-apis-swagger-agrawal/)
- [Best practice to return errors in ASP.NET Web API](https://stackoverflow.com/questions/10732644/best-practice-to-return-errors-in-asp-net-web-api)
- [Adding headers in httpClient](https://stackoverflow.com/a/38077835/1809554)
