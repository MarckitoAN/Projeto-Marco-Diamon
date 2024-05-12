create database GerenciamentoDeLojasADM;

use GerenciamentoDeLojasADM;


CREATE TABLE Cliente
(
    id       INT AUTO_INCREMENT,
    nome     VARCHAR(100) NOT NULL,
    rg       VARCHAR(20),
    cpf      VARCHAR(20),
    telefone VARCHAR(20),
    rua      VARCHAR(100),
    bairro   VARCHAR(100),
    cidade   VARCHAR(100),
    estado   VARCHAR(100),
    email    VARCHAR(100) NOT NULL,
    senha    VARCHAR(100) NOT NULL,
    PRIMARY KEY (id),
    CONSTRAINT unique_email UNIQUE (email)
);

create table Produto
(
    id        integer auto_increment,
    nome      varchar(45)    not null,
    descricao varchar(255),
    marca     varchar(45)    not null,
    preco     decimal(10, 2) not null,
    tipo      varchar(45)    not null,
    tamanho   varchar(5)     not null,
    cor       varchar(25)    not null,
    primary key (id)
);

show tables;

CREATE TABLE Pedido_Produto
(
    id_pedido_produto INTEGER UNSIGNED AUTO_INCREMENT,
    id_pedido         INTEGER,
    id_produto        INTEGER,
    quantidade        int,
    PRIMARY KEY (id_pedido_produto),
    CONSTRAINT id_produto_fk FOREIGN KEY (id_produto) REFERENCES Produto (id),
    constraint id_pedido_fk foreign key (id_pedido) references Pedido (id)
);



CREATE TABLE Estoque
(
    id           INTEGER UNSIGNED NOT NULL AUTO_INCREMENT,
    id_produto   INTEGER,
    Quantidade   INTEGER UNSIGNED,
    Data_Entrada DATE,
    Data_Saida   DATE,
    Motivo_Saida VARCHAR(45),
    PRIMARY KEY (id),
    constraint fk_pk_estoque foreign key (id_produto) references Produto (id)
);


CREATE TABLE Pedido
(
    id              INTEGER AUTO_INCREMENT,
    data            DATE,
    id_cliente      integer,
    forma_pagamento varchar(200),
    parcelas        int,
    valor_total     DECIMAL(10, 2),
    PRIMARY KEY (id),
    CONSTRAINT fk_id_clientePedido FOREIGN KEY (id_cliente) REFERENCES Cliente (id),
    CONSTRAINT valor_check check (valor_total >= 0)
);

create table User
(
    id         integer auto_increment,
    nome_loja  varchar(255) not null,
    contato    varchar(16)  not null,
    email      varchar(255) not null,
    cnpj       varchar(14)  not null,
    senha_hash varchar(255) not null,
    primary key (id)
);

select *from User;

drop table User;