# API de Reservas de Mesas

Esta API foi desenvolvida para gerenciar mesas e reservas de um restaurante. 
O sistema oferece funcionalidades para autenticação de usuários, gestão de mesas e
controle de reservas, garantindo uma experiência segura e eficiente.

## Funcionalidades Principais 

### Autenticação de Usuários

- Registro de novos usuários com nome, e-mail e senha.
  
- Login seguro com geração de token JWT.
  
- Restrição de acesso para que apenas usuários autenticados acessem as funcionalidades principais.

### Gestão de Mesas

- Listagem de todas as mesas e seus respectivos status (disponível, reservada, inativa).

- Criação de novas mesas (restrito a administradores).

- Atualização e remoção de mesas (restrito a administradores).

### Sistema de Reservas

- Criação de reservas para mesas disponíveis.

- Validação da capacidade e disponibilidade das mesas.

- Cancelamento de reservas com liberação automática da mesa.

## Tecnologias Utilizadas

- Banco de Dados: SqlServer

- ORM: Entity Framework Core

- Autenticação: JWT (JSON Web Tokens)

- Documentação: Swagger (Swashbuckle)

- Conversão de Dados: AutoMapper

## Como Rodar o Projeto Localmente

### Requisitos

- .NET 7.0 ou superior
- SqlServer
- Postman (opcional)

### Passos

1. Clone o repositório:
 
```
git clone https://github.com/diegoNZ04/ResturantAPI.git
```

2. Configure o banco de dados SqlServer no arquivo `appsettings.json`

```
"ConnectionStrings": {
    "DefaultConnection": "Server=localhost\\sqlexpress;Initial Catalog=ResturantDb; Integrated Security=True; TrustServerCertificate=true"
}
```

3. Aplique as migrações do banco de dados:

```
dotnet ef database update
```

4. Rode o projeto:

```
dotnet run
```

5. Acesse o Swagger para explorar os endpoins:

```
http://localhost:5002/swagger
```

## Endpoints Principais

### Autenticação

- `POST /account/register`: Registra um novo usuário.

- `POST /account/login`: Realiza login e retorna o token JWT.

### Mesas

- `GET /table`: Lista todas as mesas.

- `POST /table`: Adiciona uma nova mesa (restrito a administradores).

- `PATCH /table/{id}`: Atualiza informações de uma mesa.

- `DELETE /table/{id}`: Remove uma mesa (restrito a administradores).

### Reserves

- `POST /reserve`: Cria uma nova reserva.

- `GET /reserve`: Lista as reservas do usuário autenticado.

- `PATCH /reserve/{id}/cancelar`: Cancela uma reserva.

## Demonstração

![restaurantapi-demo](https://github.com/user-attachments/assets/6cf268f5-562d-44ec-aa21-8a0be72213b3)

