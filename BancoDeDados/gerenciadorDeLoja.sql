create database GerenciamentoDeLojasADM;

use GerenciamentoDeLojasADM;

CREATE TABLE Cliente
(
    id_cliente    INT AUTO_INCREMENT,
    nome_cliente  VARCHAR(100) NOT NULL,
    rg_cliente    VARCHAR(20),
    cpf_cliente   VARCHAR(20),
    telefone VARCHAR(11),
    rua           VARCHAR(100),
    bairro        VARCHAR(100),
    cidade        VARCHAR(100),
    estado        VARCHAR(100),
    email_cliente VARCHAR(100) NOT NULL,
    senha_hash    VARCHAR(100) NOT NULL,
    PRIMARY KEY (id_cliente),
    CONSTRAINT unique_email UNIQUE (email_cliente)
);

create table Produto
(
    id_produto     integer auto_increment,
    nome_produto   varchar(45)    not null,
    descri_produto varchar(255),
    marca_produto  varchar(45)    not null,
    preco_prod     decimal(10, 2) not null,
    tipo_prod      varchar(45)    not null,
    tamanho        varchar(5)     not null,
    cor_prod       varchar(25)    not null,
    estoque        int            not null,
    primary key (id_produto)
);


CREATE TABLE Pedido_Produto
(
    id_Pedido_Produto INTEGER UNSIGNED AUTO_INCREMENT,
    id_pedido         INTEGER,
    id_produto        INTEGER,
    Quantidade Int,
    PRIMARY KEY (id_Pedido_Produto),
    CONSTRAINT fk_idproduto FOREIGN KEY (id_produto) REFERENCES Produto (id_produto),
    constraint fk_pedido_id foreign key (id_pedido) references Pedido (id_pedido)

);



CREATE TABLE Estoque
(
    id_estoque   INTEGER UNSIGNED NOT NULL AUTO_INCREMENT,
    id_produto   INTEGER,
    Quantidade   INTEGER UNSIGNED,
    Data_Entrada DATE,
    Data_Saida   DATE,
    Motivo_Saida VARCHAR(45),
    PRIMARY KEY (id_Estoque),
    constraint fk_pk_estoque foreign key (id_produto) references Produto (id_produto)
);


CREATE TABLE Pedido
(
    id_pedido       INTEGER AUTO_INCREMENT,
    data_pedido     DATE,
    id_cliente      integer,
    id_produto integer,
    forma_pagamento varchar(200),
    parcelas        int,
    valor_total     DECIMAL(10, 2),
    PRIMARY KEY (id_pedido),
    CONSTRAINT fk_id_clientePedido FOREIGN KEY (id_cliente) REFERENCES Cliente (id_cliente),
	CONSTRAINT fk_id_ProdutoPedido FOREIGN KEY (id_produto) REFERENCES Produto (id_produto)

);

create table Users
(
    id_users integer auto_increment,
    nome_loja  varchar(255) not null,
    contato    varchar(16),
    email      varchar(255),
    cnpj       varchar(14),
    senha_hash varchar(255),
    primary key (id_loja),
    CONSTRAINT un_cnpj unique (cnpj),
    CONSTRAINT un_email unique (email)
);
 