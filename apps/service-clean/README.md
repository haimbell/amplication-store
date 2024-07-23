<p align="right">
  <a href="https://amplication.com" target="_blank">
    <img alt="amplication-logo" height="70" alt="Amplication Logo" src="https://amplication.com/images/logo.svg"/>
  </a>
</p>

# Introduction

This service was generated with Amplication. The server-side of the generated project. This component provides the different backend services - i.e., REST API, authentication, authorization, logging, data validation and the connection to the database. Additional information about the server component and the architecture around it, can be found on the [documentation](https://docs.amplication.com/guides/getting-started) site.

# Getting started

## Step 1: Configuration

## Step 2.1: Scripts - pre-requisites


```sh
# installation of the dependencies
$ dotnet restore .

# create and apply initial migration
# (from within the src folder)
$ dotnet ef migrations add initialMigration
```

## Step 2.1: Scripts - database schema migrations

```sh
# For any database model change, create the new migrations
# (from within the src folder)
$ dotnet ef migrations add <new migration name>
```

> Migration will be automatically applied on docker compose up

## Step 2.2: Scripts - local development

- Run the service's dependencies in Docker `docker-compose up --build`
- Run the service  `dotnet run --project ./src`

## Step 2.2: Scripts - container based development

- Run the service and its dependencies in Docker `docker-compose --profile complete up --build`

## Apply database migration in non-local environment 

Database modification in non-local enviroment would be applied through different strategies depending on the requirements. 
Follow Microsoft directions for your strategy: https://learn.microsoft.com/en-us/ef/core/managing-schemas/migrations/applying