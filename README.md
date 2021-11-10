# AssessoriaWeb
AssessoriaWeb é um trabalho da disciplina de Projeto Integrador 2 da UNIJUI <br/>
O sistema permite cadastrar turmas, avaliações e treinamentos para fazer o controle de Atletas dentro de uma Assessoria
### Dependências
* Nuget MySql.Data versão 8.0.24
* Nuget MySql.Data.EntityFramework versão 8.0.24
### Banco de Dados
A conexão com o banco de dados é definida na tag `connectionStrings` do arquivo [Web.Config](Web.config)
```
<connectionStrings>
  <add name="AssessoriaWebContext" connectionString="Server=200.132.195.95,3306;Database=AssessoriaWeb;Uid=assessoria;Pwd=jEr54gati$s)As;" providerName="MySql.Data.MySqlClient" />
</connectionStrings>
```
### Problemas conhecidos
* The type or namespace name 'EntityFramework' does not exist in the namespace 'MySql.Data'
  * Diferença de versões no `References`, verifique se `MySql.Data` e `MySql.Data.EntityFramework` instalados são da versão 8.0.24
* The provider type names have to be unique for each configured provider
  * Alguma tag de `provider` está duplicada no arquivo `Web.config`

### Possíveis melhorias
* Converter MySql para SqlServer
* Usar EF Migration para criar o banco
* Add SeedData
* Adicionar filtros de pesquisa
* Add tag ValidateAntiForgeryToken
