create database GerenciamentoDeLojasADM;

use GerenciamentoDeLojasADM;

CREATE TABLE Cliente
(
    id    INT AUTO_INCREMENT,
    nome  VARCHAR(100) NOT NULL,
    rg    VARCHAR(20),
    cpf   VARCHAR(20),
    telefone VARCHAR(11),
    rua           VARCHAR(100),
    bairro        VARCHAR(100),
    cidade        VARCHAR(100),
    estado        VARCHAR(100),
    email VARCHAR(100) NOT NULL,
    senha_hash    VARCHAR(100) NOT NULL,
    PRIMARY KEY (id),
    CONSTRAINT unique_email UNIQUE (email)
);

create table Produto
(
    id     integer auto_increment,
    nome   varchar(45)    not null,
    descri varchar(255),
    marca  varchar(45)    not null,
    preco     decimal(10, 2) not null,
    tipo      varchar(45)    not null,
    tamanho        varchar(5)     not null,
    cor       varchar(25)    not null,
    estoque        int            not null,
    primary key (id)
);


CREATE TABLE Pedido_Produto
(
    id_Pedido_Produto INTEGER UNSIGNED AUTO_INCREMENT,
    id_pedido         INTEGER,
    id_produto        INTEGER,
    Quantidade Int,
    PRIMARY KEY (id_Pedido_Produto),
    CONSTRAINT id_produto_fk FOREIGN KEY (id_produto) REFERENCES Produto (id),
    constraint id_pedido_fk foreign key (id_pedido) references Pedido (id)

);



CREATE TABLE Estoque
(
    id   INTEGER UNSIGNED NOT NULL AUTO_INCREMENT,
    id   INTEGER,
    Quantidade   INTEGER UNSIGNED,
    Data_Entrada DATE,
    Data_Saida   DATE,
    Motivo_Saida VARCHAR(45),
    PRIMARY KEY (id_Estoque),
    constraint id_produto_fk foreign key (id_produto) references Produto (id_produto)
);


CREATE TABLE Pedido
(
    id       INTEGER AUTO_INCREMENT,
    data     DATE,
    id      integer,
    id integer,
    forma_pagamento varchar(200),
    parcelas        int,
    valor_total     DECIMAL(10, 2),
    PRIMARY KEY (id_pedido),
    CONSTRAINT id_cliente_fk FOREIGN KEY (id_cliente) REFERENCES Cliente (id_cliente),
	CONSTRAINT id_produto_fk FOREIGN KEY (id_produto) REFERENCES Produto (id_produto)

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
 
