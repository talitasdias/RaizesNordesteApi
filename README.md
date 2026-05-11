# Raízes do Nordeste API

API REST desenvolvida em ASP.NET Core para gerenciamento operacional da rede fictícia de restaurantes Raízes do Nordeste.

O projeto contempla:

* autenticação JWT;
* controle de permissões por perfil;
* gerenciamento de produtos;
* gerenciamento de unidades;
* controle de estoque;
* criação de pedidos;
* pagamento mock;
* programa de fidelidade.

## Requisitos

Antes de executar o projeto, é necessário possuir os seguintes itens instalados:
| Requisito  | Versão |
| ------------- |:-------------:|
| [.NET SDK](https://dotnet.microsoft.com/pt-br/download/dotnet/8.0)     | 8.0    |
| [PostgreSQL](https://www.postgresql.org/download/)      | 14+     |
| Entity Framework CLI      | Última versão     |
| [Postman](https://www.postman.com/downloads/) (opcional)      | Última versão     |

###  Entity Framework CLI

Executar no terminal:
```
dotnet tool install --global dotnet-ef
```

Verificação instalação:
```
dotnet ef
```

## Configuração das Variáveis de Ambiente

O projeto utiliza o arquivo appsettings.json para configuração da connection string.

### Exemplo de Configuração

No arquivo appsettings.json:
```
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=raizesnordeste;Username=postgres;Password=sua_senha"
}
```

## Como Instalar as Dependências

Após clonar o projeto:
```
dotnet restore
```

## Como Criar o Banco e Executar as Migrations
1. Criar banco PostgreSQL

Exemplo:
```
raizesnordeste
```

2. Executar migrations

As migrations já estão incluídas no projeto.

Executar:
```
dotnet ef database update
```

## Como Executar o Seed

O projeto possui um script para popular o banco.

Arquivo:
```
Scripts/seed.sql
```
Executar utilizando:
* DBeaver
* PgAdmin
* ou outra ferramenta PostgreSQL

O seed cria:
* usuários;
* unidades;
* produtos;
* estoques;
* pedidos;
* pagamentos.

## Como Iniciar a API

Executar:
```
dotnet run
```

## Como Acessar a Documentação Swagger

Após iniciar a aplicação:
```
https://localhost:5113/swagger
```

## Coleção Postman (Evidência)

Os arquivos Postman encontram-se na pasta:
```
/postman
```
Arquivos:

* RaizesNordeste.postman_collection.json
* RaizesNordeste.postman_environment.json

## Como Rodar os Testes

### Observação: rodar os testes primeiro, somente depois rodar o fluxo principal
1. Abrir o Postman
2. Importar os arquivos da pasta /postman
3. Selecionar o ambiente importado
4. Executar seguindo a sequência de testes conforme a tabela:

| ID  | Cenário                              | Endpoint                                         | Pré-condição                                              | Entrada                                          | Esperado (status + pontos do response)                                      | Evidência (nome na coleção)                |
|-----|--------------------------------------|--------------------------------------------------|------------------------------------------------------------|--------------------------------------------------|------------------------------------------------------------------------------|---------------------------------------------|
| T01 | Login válido                         | `POST /api/auth/login`                           | Usuário cadastrado no seed                                 | Body com email e senha válidos                   | `200 OK` + token JWT retornado                                              | Auth/Login com sucesso                      |
| T02 | Login para preencher tokenCozinha   | `POST /api/auth/login`                           | Usuário cadastrado no seed                                 | Body com email e senha válidos                   | `201 OK` + token JWT retornado                                              | Auth/Login para preencher tokenCozinha      |
| T03 | Consultar cardápio por unidade       | `GET /api/Unidades/1/cardapio`                   | Unidade e produtos cadastrados                             | Path `id=1`                                      | `200 OK` + lista de produtos                                                | Unidades/Consultar cardápio                 |
| T04 | Listar produtos paginados            | `GET /api/Produtos?pagina=1&tamanhoPagina=5`     | Produtos cadastrados                                       | QueryString `pagina` e `tamanhoPagina`           | `200 OK` + lista paginada                                                   | Produtos/Listar produtos paginados          |
| T05 | Criar pedido válido                  | `POST /api/Pedidos`                              | Cliente autenticado + estoque disponível                   | Body com unidade e itens válidos                 | `201 Created` + pedido criado                                               | Pedidos/Criar pedido válido                 |
| T06 | Pagamento PIX aprovado               | `POST /api/Pagamentos`                           | Pedido aguardando pagamento                                | Body com `pedidoId` e método PIX                 | `200 OK` + pagamento aprovado + status Pago                                 | Pagamentos/Pagamento PIX aprovado           |
| T07 | Atualizar status válido              | `PATCH /api/Pedidos/{id}/status`                 | Pedido com status compatível + usuário autorizado          | Path `id` + body com novo status                 | `204 NoContent` + status atualizado                                         | Pedidos/Atualizar status válido             |
| T08 | Acesso sem token                     | `POST /api/Pedidos`                              | Nenhuma                                                    | Body válido sem JWT                              | `401 Unauthorized` + erro padrão                                            | Erros/Acesso sem token                      |
| T09 | Perfil sem permissão                 | `POST /api/Produtos`                             | Usuário Cliente autenticado                                | Body válido + token Cliente                      | `403 Forbidden` + erro padrão                                               | Erros/Perfil sem permissão                  |
| T10 | Cadastro com e-mail já existente     | `POST /api/auth/register`                        | Usuário com e-mail já cadastrado no banco                  | Body com email já existente                      | `409 Conflict` + mensagem informando que o e-mail já está cadastrado        | Erros/Cadastro email existente              |
| T11 | Pedido com estoque insuficiente      | `POST /api/Pedidos`                              | Cliente autenticado + estoque insuficiente                 | Body com quantidade acima do estoque             | `422 Unprocessable Entity` + mensagem de estoque insuficiente               | Erros/Pedido estoque insuficiente           |

## Fluxo Principal MVP

O projeto implementa o fluxo obrigatório:

Pedido → Pagamento Mock → Atualização de Status

Fluxo disponível na pasta:
```
Fluxo Principal MVP
```
Etapas:

* Login cliente
* Criar pedido
* Realizar pagamento
* Login cozinha
* Atualizar status para EmPreparo
* Atualizar status para Pronto

# Observações

A funcionalidade de logs/auditoria não foi implementada nesta versão do projeto, devido à priorização do fluxo principal obrigatório e das funcionalidades essenciais do sistema.
