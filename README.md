# AssessoriaWeb
AssessoriaWeb é um trabalho da disciplina de Projeto Integrador 2 da UNIJUI <br/>
O sistema permite cadastrar turmas, avaliações e treinamentos para fazer o controle de Atletas dentro de uma Assessoria
### Dependências
* Nuget MySql.Data
* Nuget MySql.Data.EntityFramework
### Banco de Dados
A conexão com o banco de dados é definida na tag `connectionStrings` do arquivo [Web.Config](Web.config)
```
<connectionStrings>
  <add name="AssessoriaWebContext" connectionString="Server=200.132.195.95,3306;Database=AssessoriaWeb;Uid=assessoria;Pwd=jEr54gati$s)As;" providerName="MySql.Data.MySqlClient" />
</connectionStrings>
```
### Problemas conhecidos
* The type or namespace name 'EntityFramework' does not exist in the namespace 'MySql.Data'
  * Problema com o `References`, exclui a referência e instale o `MySql.Data` e o `MySql.Data.EntityFramework` novamente
* The provider type names have to be unique for each configured provider
  * Alguma tag de `provider` está duplicada no arquivo `Web.config`
