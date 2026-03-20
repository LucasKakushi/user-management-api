# 🔐 User Management API

API RESTful desenvolvida em ASP.NET Core para gerenciamento de usuários, com autenticação JWT, autorização por roles e arquitetura em camadas (Clean Architecture básica).

---

## 🚀 Tecnologias utilizadas

- ASP.NET Core
- Entity Framework Core (SQLite)
- JWT Authentication
- BCrypt (hash de senha)
- Swagger (documentação)

---

## 🧱 Arquitetura

O projeto segue uma separação em camadas:

- **Controllers** → entrada HTTP
- **Application**
  - DTOs → contratos de entrada/saída
  - Services → regras de negócio
- **Domain**
  - Models → entidades do sistema
- **Infrastructure**
  - Repositories → acesso a dados
  - DbContext → conexão com banco

---

## 🔐 Autenticação e autorização

- Login com JWT
- Claims utilizadas:
  - Id do usuário
  - Nome
  - Email
  - Role

### Roles:
- `Admin` → acesso total
- `User` → acesso limitado

---

## 📌 Funcionalidades

- CRUD de usuários
- Autenticação com JWT
- Autorização por roles
- Hash de senha com BCrypt
- Validação de dados
- Tratamento global de exceções
- Paginação
- Filtro por nome e email
- Normalização de dados (email e role)

---

## ▶️ Como rodar o projeto

# restaurar dependências
dotnet restore

# aplicar migrations
dotnet ef database update

# rodar a aplicação
dotnet run

---

## 🔑 Usuário padrão (seed)
Email: admin@admin.com
Senha: 123456
Role: Admin

---

## 📡 Endpoints principais

### 🔐 Autenticação
POST /auth/login

### 👤 Usuários
GET    /users
GET    /users/{id}
POST   /users        (Admin)
PUT    /users/{id}   (Admin)
DELETE /users/{id}   (Admin)

### 📄 Paginação
GET /users?page=1&pageSize=10

###🔎 Filtros
GET /users?nome=lucas
GET /users?email=gmail.com
GET /users?nome=lucas&page=1&pageSize=5

---

## ⚠️ Tratamento de erros

A API possui middleware global de exceções.

## 🔒 Segurança

- Senhas armazenadas com hash (BCrypt)
- Autenticação via JWT
- Controle de acesso por roles
- Validação de entrada de dados

---

## 📚 Objetivo do projeto

- Este projeto foi desenvolvido com foco em prática de:
- construção de APIs RESTful
- organização de código em camadas
- boas práticas de backend
- preparação para ambiente profissional

---

## 👨‍💻 Autor

Desenvolvido por Lucas Kakushi
---
```md
