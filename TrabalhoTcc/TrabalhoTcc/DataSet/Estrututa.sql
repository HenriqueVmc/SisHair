-- ADM -> Funcionarios

CREATE TABLE funcionarios(
	id INT IDENTITY NOT NULL,
	nome VARCHAR(100) NOT NULL,
	data_nascimento DATETIME,
	cpf VARCHAR(11) NOT NULL,
	ctps VARCHAR(20),
    id_cargo INT,
    id_contato INT NOT NULL,
	id_endereco INT
);

CREATE TABLE contatos_funcionarios(
    id INT IDENTITY NOT NULL,
    telefone VARCHAR(15),
    celular VARCHAR(15),
    email VARCHAR(100),
    id_funcionario INT
);

CREATE TABLE cargos(
	id INT IDENTITY(1, 1),
	nome VARCHAR(50),
	descricao VARCHAR(100)
);

CREATE TABLE logins_funcionarios(
	id INT IDENTITY(1, 1),
	usuario VARCHAR(50),
	senha VARCHAR(20),
    id_funcionario INT NOT NULL
);

CREATE TABLE enderecos(
	id INT IDENTITY(1, 1),
	rua VARCHAR(100),
	bairro VARCHAR(100),
	numero VARCHAR(10),
	complemento VARCHAR(30),
	estado VARCHAR(2),
	cidade VARCHAR(60)
);


CREATE TABLE servicos(
	id INT IDENTITY(1, 1),
	nome_servico VARCHAR(100),
	valor FLOAT,
	descricao VARCHAR(100)
);

CREATE TABLE agendamento(
	id INT IDENTITY(1, 1),
	id_funcionario INT,
	data DATE,
	hora_inicio TIME,
	hora_final TIME,
	id_cliente INT,
	id_servico INT
);

-- Funcionario --> cliente || adm --> cliente || WEB

CREATE TABLE clientes(
	id INT IDENTITY(1, 1),
	nome VARCHAR(100),
	data_nascimento DATETIME,
	celular VARCHAR(100),
	telefone VARCHAR(100)
);

CREATE TABLE solicitacao(
    id_solicitacao INT IDENTITY,
    id_cliente INT IDENTITY,
    id_funcionario INT IDENTITY,
    situacao VARCHAR(100),
    descricao VARCHAR(200)
);






