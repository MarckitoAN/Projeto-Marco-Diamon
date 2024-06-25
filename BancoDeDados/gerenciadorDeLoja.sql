create database GerenciadorLojasPraADM;

use GerenciadorLojasPraADM;

create table User
(
    id         integer auto_increment,
    nome_loja  varchar(255) not null,
    contato    varchar(25)  not null,
    email      varchar(255) not null,
    cnpj       varchar(25)  not null,
    senha_hash varchar(255) not null,
    primary key (id)
);

CREATE TABLE Cliente
(
    id       INT AUTO_INCREMENT,
    id_user  integer,
    nome     VARCHAR(100) NOT NULL,
    rg       VARCHAR(50),
    cpf      VARCHAR(50),
    telefone VARCHAR(50),
    rua      VARCHAR(100),
    bairro   VARCHAR(100),
    cidade   VARCHAR(100),
    estado   VARCHAR(100),
    email    VARCHAR(100) NOT NULL,
    senha    VARCHAR(100) NOT NULL,
    PRIMARY KEY (id),
    CONSTRAINT unique_email UNIQUE (email),
    constraint id_userFK2 foreign key (id_user) references User (id)

);



create table Produto
(
    id           integer auto_increment,
    id_user      integer,
    nome         varchar(45)    not null,
    descricao    varchar(255),
    marca        varchar(45)    not null,
    preco        decimal(10, 2) not null,
    tipo         varchar(45)    not null,
    tamanho      varchar(50)    not null,
    precoDeCusto decimal(10, 2) not null,
    imagem longblob,
    primary key (id),
    constraint id_userFK foreign key (id_user) references User (id)
);


CREATE TABLE Estoque
(
    id           INTEGER UNSIGNED NOT NULL AUTO_INCREMENT,
    id_user      integer,
    id_produto   INTEGER,
    Quantidade   INTEGER UNSIGNED,
    Data_Entrada DATETIME,
    Data_Saida   DATETIME,
    Motivo_Saida VARCHAR(45),
    PRIMARY KEY (id),
    constraint fk_pk_estoque foreign key (id_produto) references Produto (id),
    constraint id_userFK4 foreign key (id_user) references User (id)

);

CREATE TABLE Pedido
(
    id              INTEGER AUTO_INCREMENT,
    data            DATE,
    id_user         integer,
    id_cliente      integer,
    forma_pagamento varchar(200),
    parcelas        int,
    valor_total     DECIMAL(10, 2),
    PRIMARY KEY (id),
    CONSTRAINT fk_id_clientePedido FOREIGN KEY (id_cliente) REFERENCES Cliente (id),
    CONSTRAINT valor_check check (valor_total >= 0),
    constraint id_userFK5 foreign key (id_user) references User (id)

);



CREATE TABLE Fornecedor
(
    id     INTEGER PRIMARY KEY AUTO_INCREMENT,
    nome   VARCHAR(50),
    rua    VARCHAR(100),
    bairro VARCHAR(100),
    cidade VARCHAR(100),
    estado VARCHAR(100),
    email  VARCHAR(100) NOT NULL,
    cnpj   VARCHAR(50) UNIQUE
);

create table Produto_Fornecedor
(
    id_Produto_Fornecedor integer auto_increment primary key,
    id_produto            integer,
    id_fornecedor         integer,
    constraint fk_pk_produtoFornecedor foreign key (id_produto) references Produto (id),
    constraint fk_pk_FornecedorProduto foreign key (id_fornecedor) references Fornecedor (id)
);

CREATE TABLE Pedido_Produto
(
    id_pedido_produto INTEGER UNSIGNED AUTO_INCREMENT,
    id_pedido         INTEGER,
    id_user           integer,
    id_produto        INTEGER,
    id_fornecedor     integer,
    quantidade        int,
    PRIMARY KEY (id_pedido_produto),
    CONSTRAINT id_produto_fk FOREIGN KEY (id_produto) REFERENCES Produto (id),
    constraint id_pedido_fk foreign key (id_pedido) references Pedido (id),
    constraint id_fornecedor_fk foreign key (id_fornecedor) references fornecedor (id),
    constraint id_userFK3 foreign key (id_user) references User (id)
);

CREATE TABLE Despesas (
    id_despesa INT PRIMARY KEY AUTO_INCREMENT,
    descricao VARCHAR(255) NOT NULL,
    data DATE NOT NULL,
    valor DECIMAL(10, 2) NOT NULL,
    tipo_despesa VARCHAR(100) default "Saida"
);
