<div align="center">
  <h1>🧹 Broooms</h1>
  <p>An e-commerce to sell all kinds of broooms. My first microservices project!</p>
  <img alt="License" src="https://img.shields.io/badge/license-MIT-191929?style=flat-square">
  <img alt="Stars" src="https://img.shields.io/github/stars/vassourita/broooms?style=flat-square">
  <img alt="Last Commit" src="https://img.shields.io/github/last-commit/vassourita/broooms?style=flat-square" />
  <div><a href="#getting-started">Getting Started</a> • <a href="#application-overview">Application overview</a> • <a href="#features">Features</a> • <a href="#license">License</a></div>
</div>
<br>
<br>

## 🚀 Getting Started

Requirements:
- .NET 6
- Docker and Docker Compose

For now, you can use the docker-compose file to run the application. There is a script to run it correctly.

In the root folder, run:
```sh
./scripts/create-settings.sh # makes a copy of example config files to development config files
./scripts/compose-up.dev.sh # runs compose containers
```

The APIs are not running on docker now, so I'll need to run them manually with the <code>dotnet run</code> command. As the applications grow and I develop the main clients for them, I'll be adding them to the compose file so they can all be run at once.

---

## 💻 Application overview

Broooms is based on a microservices architecture. The main services are:

| Name                  | Description                              | Tech stack                                |
| --------------------- | ---------------------------------------- | ----------------------------------------- |
| Broooms.Catalog       | Manages products and categories          | .NET 6 WebAPI, EFCore, PostgreSQL         |
| Broooms.Cart          | Manages user carts                       | .NET 6 MinimalAPI, Redis                  |
| Broooms.Discount      | Manages discount coupons                 | .NET 6 GRPC Service, MongoDB              |
| Broooms.Orders        | Manages user orders                      | .NET 6 WebAPI, EFCore, PostgreSQL         |
| Broooms.Notifications | Sends emails and real-time notifications | .NET 6 SignalR Server, Dapper, PostgreSQL |
| Broooms.Reports       | Generates reports                        | Python, FastAPI, Pandas                   |
| Broooms.Auth          | Manages user accounts and auth           | .NET 6 WebAPI, EFCore, SQL Server         |
| Broooms.Admin.Web     | Admin UI                                 | ReactJs, NextJs, Tailwind                 |
| Broooms.Shop.Web      | Shop UI                                  | .NET 6 Razor WebApp, Tailwind             |
| Broooms.Aggregator    | Gateway/BFF for all services             | .NET 6 GraphQL API, Redis                 |

Some architecture/app design/data flow/event diagrams will be added with time.

---

## 💫 Features

### Catalog

- [x] Search broooms by keyword and category with pagination
- [x] Show a brooom by id
- [x] Create a new broom
- [x] Update a broom
- [x] Delete a broom
- [x] Add a product image and upload it to a storage service
- [x] List all categories
- [x] Show a category by id
- [x] Create a new category
- [x] Update a category
- [x] Delete a category
- [x] Add a category to a product
- [x] Remove a category from a product

### Cart

- [x] Show user cart
- [x] Update a product quantity in cart
- [x] Clear cart
- [ ] Add coupon code
- [ ] Remove coupon code
- [ ] Checkout

### Coupon

- [ ] List all coupons
- [ ] Show a coupon by code
- [ ] Create a new coupon
- [ ] Disable a coupon
- [ ] Reenable a coupon

### Auth

- [ ] Register customer
- [ ] Register admin
- [ ] Confirm email
- [ ] Login + Access token
- [ ] Refresh token
- [ ] Claim based authentication
- [ ] Logout
- [ ] Get user info
- [ ] Update user info
- [ ] Change password
- [ ] Disable user
- [ ] Reenable user

### Orders

> Features description soon

### Notifications

> Features description soon

### Reports

> Features description soon

### Admin panel

> Features description soon

### Shop WebApp

> Features description soon

---

## 📝 License

This project is licensed under the terms of the MIT license.
