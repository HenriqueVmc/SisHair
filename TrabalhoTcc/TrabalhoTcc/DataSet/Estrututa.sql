﻿-- TABELAS

CREATE TABLE funcionarios(
	id INT IDENTITY(1, 1) NOT NULL,
	nome VARCHAR(100) NOT NULL,
	data_nascimento DATE,
	cpf VARCHAR(15) NOT NULL CONSTRAINT UqCPF UNIQUE,
	telefone VARCHAR(15),
    celular VARCHAR(15),
	email VARCHAR(100),
	descricao VARCHAR(200),
	id_cargo INT,
	id_endereco INT
);

CREATE TABLE cargos(
	id INT IDENTITY NOT NULL,
	cargo VARCHAR(50) NOT NULL,
	descricao VARCHAR(100)
);

CREATE TABLE enderecos(
	id INT IDENTITY NOT NULL,
	rua VARCHAR(100),
	bairro VARCHAR(100),
	numero_casa VARCHAR(5),
	complemento VARCHAR(30),
	estado VARCHAR(2),
	cidade VARCHAR(60),
	cep VARCHAR(8)
);

CREATE TABLE login_funcionarios(
	id INT IDENTITY NOT NULL,
	id_funcionario INT NOT NULL,
	usuario VARCHAR(40) NOT NULL CONSTRAINT UqUsuarioFuncionario UNIQUE,
	senha VARCHAR(20) NOT NULL
);

CREATE TABLE horarios_funcionarios(
	hora TIME NOT NULL,
	disponibilidade BIT NOT NULL,
	id_funcionario INT NOT NULL
);

CREATE TABLE servicos(
	id INT IDENTITY NOT NULL,
	servico VARCHAR(100) NOT NULL,
	valor MONEY,
	duracao TIME,
	descricao VARCHAR(100)
);

CREATE TABLE agendamentos(
	id INT IDENTITY NOT NULL,
	id_funcionario INT NOT NULL,
	id_cliente INT NOT NULL,
	id_servico INT NOT NULL,
	hora_inicio TIME NOT NULL,
	hora_final TIME NOT NULL,
	data DATE NOT NULL	
);

CREATE TABLE clientes(
	id INT IDENTITY NOT NULL,
	nome VARCHAR(100) NOT NULL,
	data_nascimento DATE,
	celular VARCHAR(15),
	telefone VARCHAR(15),
	email VARCHAR(100)
);

SELECT * FROM clientes

CREATE TABLE login_clientes(
	id INT IDENTITY NOT NULL,
	id_cliente INT NOT NULL,
	usuario VARCHAR(30) NOT NULL CONSTRAINT UqUsuarioCliente UNIQUE,
	senha VARCHAR(20) NOT NULL,
);

-- Necessário??
CREATE TABLE solicitacoes(
	id INT IDENTITY NOT NULL,
	hora_inicio TIME NOT NULL,
	data DATE NOT NULL,
	situacao VARCHAR(50),
	id_cliente INT NOT NULL,
	id_funcionario INT NOT NULL
);

-- CHAVES PRIMÁRIAS
ALTER TABLE funcionarios ADD CONSTRAINT PKFuncionarios PRIMARY KEY(id);
ALTER TABLE enderecos ADD CONSTRAINT PKEnderecos PRIMARY KEY(id);
ALTER TABLE cargos ADD CONSTRAINT PKCargos PRIMARY KEY(id);
ALTER TABLE login_funcionarios ADD CONSTRAINT PKLoginFuncionarios PRIMARY KEY(id);
ALTER TABLE servicos ADD CONSTRAINT PKServicos PRIMARY KEY(id);
ALTER TABLE agendamentos ADD CONSTRAINT PKAgendamentos PRIMARY KEY(id);
ALTER TABLE horarios_funcionarios ADD CONSTRAINT PKHorarios PRIMARY KEY(id);
ALTER TABLE clientes ADD CONSTRAINT PKClientes PRIMARY KEY(id);
ALTER TABLE login_clientes ADD CONSTRAINT PKLoginClientes PRIMARY KEY(id);
ALTER TABLE solicitacoes ADD CONSTRAINT PKSolicitacoes PRIMARY KEY(id);

-- CHAVES ESTRANGEIRAS (RELACIONAMENTOS)
ALTER TABLE funcionarios ADD CONSTRAINT FKCargoFuncionario FOREIGN KEY(id_cargo) REFERENCES cargos(id);
ALTER TABLE funcionarios ADD CONSTRAINT FKEnderecoFuncionario FOREIGN KEY(id_endereco) REFERENCES enderecos(id);
ALTER TABLE login_funcionarios ADD CONSTRAINT FKLoginFuncionario FOREIGN KEY(id_funcionario) REFERENCES funcionarios(id);
ALTER TABLE agendamentos ADD CONSTRAINT FKFuncionarioAgendamento FOREIGN KEY(id_funcionario) REFERENCES funcionarios(id);
ALTER TABLE agendamentos ADD CONSTRAINT FKClienteAgendamento FOREIGN KEY(id_cliente) REFERENCES clientes(id);
ALTER TABLE agendamentos ADD CONSTRAINT FKServicoAgendamento FOREIGN KEY(id_servico) REFERENCES servicos(id);
ALTER TABLE login_clientes ADD CONSTRAINT FKLoginCliente FOREIGN KEY(id_cliente) REFERENCES clientes(id);

ALTER TABLE solicitacoes ADD CONSTRAINT FKFuncionarioSolicitacao FOREIGN KEY(id_funcionario) REFERENCES funcionarios(id);
ALTER TABLE solicitacoes ADD CONSTRAINT FKClienteSolicitacao FOREIGN KEY(id_cliente) REFERENCES clientes(id);


/* DÙVIDAS

CREATE TABLE agendamento_servicos(
	id INT IDENTITY,
	id_agendamento INT,
	id_servico INT,
	situacao BOOLEAN,
	valor_total MONEY,
);

CREATE TABLE contatos_funcionarios(
	id INT IDENTITY,
	valor VARCHAR(10),
    tipo_contato VARCHAR(11),
	id_funcionario INT
);

*/