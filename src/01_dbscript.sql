CREATE DATABASE cardpark_db;

USE cardpark_db;

CREATE TABLE clientes (
    id INT IDENTITY(1,1) PRIMARY KEY,
    tipo_pessoa INT NOT NULL,
    cpf_cnpj VARCHAR(20) NOT NULL UNIQUE,
    nome_razao_social VARCHAR(255) NOT NULL,
    nome_fantasia VARCHAR(255) NULL,
    sexo CHAR(1) NULL,
    data_nascimento DATE NULL,
    rg VARCHAR(20) NULL,
    cep VARCHAR(10) NULL,
    logradouro VARCHAR(255) NULL,
    numero VARCHAR(10) NULL,
    complemento VARCHAR(100) NULL,
    bairro VARCHAR(100) NULL,
    cidade VARCHAR(100) NULL,
    estado CHAR(2) NULL,
    telefone VARCHAR(15) NULL,
    telefone_alt VARCHAR(15) NULL,
    email VARCHAR(100) NULL,
    email_alt VARCHAR(100) NULL,
    created_at DATETIME DEFAULT GETDATE()
);