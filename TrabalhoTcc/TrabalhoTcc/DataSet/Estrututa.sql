

INSERT INTO Servicoes(Nome, Valor, Duracao) VALUES ('Corte', 30, 10);
INSERT INTO Cargoes (Nome) VALUES ('Cabeleireiro');

INSERT INTO Funcionarios (CargoId, Nome, DataNascimento, Telefone, Cpf) VALUES (1,'Rosimeire de Oliveira', '1999-09-09', '11991578765', '98798798776');
INSERT INTO Funcionarios (CargoId, Nome, DataNascimento, Telefone, Cpf) VALUES (1,'Cláudia dos Santos', '2000-11-11', '22222222222', '11111111111');
INSERT INTO Permissoes (TipoPermissao) VALUES ('Administrador')
INSERT INTO Permissoes (TipoPermissao) VALUES ('Funcionario')

INSERT INTO LoginFuncionarios (Usuario, Senha, FuncionarioId, PermissoesId) VALUES ('adm','b09c600fddc573f117449b3723f23d64',1,1);
INSERT INTO LoginFuncionarios (Usuario, Senha, FuncionarioId, PermissoesId) VALUES ('fun','77004ea213d5fc71acf74a8c9c6795fb', 2,2);

SELECT * FROM Cargoes;
SELECT * FROM Servicoes;
SELECT * FROM Permissoes;
SELECT * FROM LoginFuncionarios;
SELECT * FROM Funcionarios;
SELECT * FROM Clientes;
SELECT * FROM LoginClientes;

INSERT INTO Clientes (Nome, Data_nascimento, Celular, Telefone, Email) VALUES ('Alan', '2000-05-05', '479998956', '4566666666', 'alaneduardoalves2018@gmail.com')
INSERT INTO LoginClientes (Usuario, Senha, ClienteId) VALUES ('cli','cli', 1);

-- DATABASE GENERATE BY ENTITY
CREATE TABLE [dbo].[Agendamentoes] (
    [Id] [int] NOT NULL IDENTITY,
    [DataHoraInicio] [datetime] NOT NULL,
    [DataHoraFinal] [datetime] NOT NULL,
    [Situacao] [nvarchar](max),
    [Descricao] [nvarchar](max),
    [Servicos] [nvarchar](max),
    [FuncionarioId] [int] NOT NULL,
    [ClienteId] [int] NOT NULL,
    [RegistroAgendamentoAtivo] [bit] NOT NULL,
    [AvaliouSalao] [bit] NOT NULL,
    CONSTRAINT [PK_dbo.Agendamentoes] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_FuncionarioId] ON [dbo].[Agendamentoes]([FuncionarioId])
CREATE INDEX [IX_ClienteId] ON [dbo].[Agendamentoes]([ClienteId])
CREATE TABLE [dbo].[Clientes] (
    [Id] [int] NOT NULL IDENTITY,
    [Nome] [nvarchar](100) NOT NULL,
    [Data_nascimento] [datetime] NOT NULL,
    [Celular] [nvarchar](15) NOT NULL,
    [Telefone] [nvarchar](15) NOT NULL,
    [Email] [nvarchar](max) NOT NULL,
    [RegistroClienteAtivo] [bit] NOT NULL,
    CONSTRAINT [PK_dbo.Clientes] PRIMARY KEY ([Id])
)
CREATE TABLE [dbo].[Funcionarios] (
    [Id] [int] NOT NULL IDENTITY,
    [Nome] [nvarchar](100) NOT NULL,
    [DataNascimento] [datetime] NOT NULL,
    [Cpf] [nvarchar](max) NOT NULL,
    [Celular] [nvarchar](15),
    [Telefone] [nvarchar](15),
    [Email] [nvarchar](max),
    [CargoId] [int] NOT NULL,
    [RegistroFuncionarioAtivo] [bit] NOT NULL,
    CONSTRAINT [PK_dbo.Funcionarios] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_CargoId] ON [dbo].[Funcionarios]([CargoId])
CREATE TABLE [dbo].[Cargoes] (
    [Id] [int] NOT NULL IDENTITY,
    [Nome] [nvarchar](max) NOT NULL,
    [Descricao] [nvarchar](max),
    [RegistroCargoAtivo] [bit] NOT NULL,
    CONSTRAINT [PK_dbo.Cargoes] PRIMARY KEY ([Id])
)
CREATE TABLE [dbo].[Avaliacaos] (
    [Id] [int] NOT NULL IDENTITY,
    [AvaliacaoUsuario] [nvarchar](max) NOT NULL,
    [NotaVoltarNovamente] [tinyint] NOT NULL,
    [NotaAgendamento] [tinyint] NOT NULL,
    [NotaExperienciaAtendimento] [tinyint] NOT NULL,
    [NotaCondicoesFisicasEstabelecimento] [tinyint] NOT NULL,
    [VoltariaNovamente] [bit] NOT NULL,
    [RecomendariaAlguem] [bit] NOT NULL,
    [AvaliacaoAprovadaParaIndex] [bit] NOT NULL,
    [AvaliouSalao] [bit] NOT NULL,
    [RegistroAvaliacaoAtivo] [bit] NOT NULL,
    [AgendamentoId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.Avaliacaos] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_AgendamentoId] ON [dbo].[Avaliacaos]([AgendamentoId])
CREATE TABLE [dbo].[Caixas] (
    [Id] [int] NOT NULL IDENTITY,
    [ValorTotal] [decimal](18, 2) NOT NULL,
    [ValorPago] [decimal](18, 2) NOT NULL,
    [Divida] [decimal](18, 2) NOT NULL,
    [FormaPagamento] [nvarchar](max),
    [DataPagamento] [datetime] NOT NULL,
    [Status] [nvarchar](max),
    [RegistroCaixaAtivo] [bit] NOT NULL,
    [AgendamentoId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.Caixas] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_AgendamentoId] ON [dbo].[Caixas]([AgendamentoId])
CREATE TABLE [dbo].[CodigoClientes] (
    [Id] [int] NOT NULL IDENTITY,
    [Id_Usuario] [int] NOT NULL,
    [Email] [nvarchar](max),
    [Codigo] [nvarchar](max),
    CONSTRAINT [PK_dbo.CodigoClientes] PRIMARY KEY ([Id])
)
CREATE TABLE [dbo].[EnderecoFuncionarios] (
    [Id] [int] NOT NULL IDENTITY,
    [Rua] [nvarchar](max),
    [Bairro] [nvarchar](max),
    [Numero] [int] NOT NULL,
    [Complemento] [nvarchar](max),
    [Cidade] [nvarchar](max),
    [Estado] [nvarchar](max),
    [Cep] [nvarchar](max),
    [FuncionarioId] [int] NOT NULL,
    [RegistroEnderecoFuncionarioAtivo] [bit] NOT NULL,
    CONSTRAINT [PK_dbo.EnderecoFuncionarios] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_FuncionarioId] ON [dbo].[EnderecoFuncionarios]([FuncionarioId])
CREATE TABLE [dbo].[LoginClientes] (
    [Id] [int] NOT NULL IDENTITY,
    [Usuario] [nvarchar](max) NOT NULL,
    [Senha] [nvarchar](max) NOT NULL,
    [ClienteId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.LoginClientes] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_ClienteId] ON [dbo].[LoginClientes]([ClienteId])
CREATE TABLE [dbo].[LoginFuncionarios] (
    [Id] [int] NOT NULL IDENTITY,
    [Usuario] [nvarchar](max) NOT NULL,
    [Senha] [nvarchar](max) NOT NULL,
    [FuncionarioId] [int] NOT NULL,
    [PermissoesId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.LoginFuncionarios] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_FuncionarioId] ON [dbo].[LoginFuncionarios]([FuncionarioId])
CREATE INDEX [IX_PermissoesId] ON [dbo].[LoginFuncionarios]([PermissoesId])
CREATE TABLE [dbo].[Permissoes] (
    [Id] [int] NOT NULL IDENTITY,
    [TipoPermissao] [nvarchar](max),
    CONSTRAINT [PK_dbo.Permissoes] PRIMARY KEY ([Id])
)
CREATE TABLE [dbo].[Servicoes] (
    [Id] [int] NOT NULL IDENTITY,
    [Nome] [nvarchar](max),
    [Valor] [decimal](18, 2) NOT NULL,
    [Duracao] [int] NOT NULL,
    [Descricao] [nvarchar](max),
    [RegistroServicoAtivo] [bit] NOT NULL,
    CONSTRAINT [PK_dbo.Servicoes] PRIMARY KEY ([Id])
)
CREATE TABLE [dbo].[ServicosAgendamentoes] (
    [Id] [int] NOT NULL IDENTITY,
    [AgendamentoId] [int] NOT NULL,
    [ServicoId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.ServicosAgendamentoes] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_AgendamentoId] ON [dbo].[ServicosAgendamentoes]([AgendamentoId])
CREATE INDEX [IX_ServicoId] ON [dbo].[ServicosAgendamentoes]([ServicoId])
CREATE TABLE [dbo].[ServicosSolicitacaos] (
    [Id] [int] NOT NULL IDENTITY,
    [SolicitacaoId] [int] NOT NULL,
    [ServicoId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.ServicosSolicitacaos] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_SolicitacaoId] ON [dbo].[ServicosSolicitacaos]([SolicitacaoId])
CREATE INDEX [IX_ServicoId] ON [dbo].[ServicosSolicitacaos]([ServicoId])
CREATE TABLE [dbo].[Solicitacaos] (
    [Id] [int] NOT NULL IDENTITY,
    [DataHoraInicio] [datetime] NOT NULL,
    [DataHoraFinal] [datetime] NOT NULL,
    [Situacao] [nvarchar](max),
    [FuncionarioId] [int] NOT NULL,
    [ClienteId] [int] NOT NULL,
    [Descricao] [nvarchar](max),
    [Servicos] [nvarchar](max),
    [RegistroSolicitacaoAtivo] [bit] NOT NULL,
    CONSTRAINT [PK_dbo.Solicitacaos] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_FuncionarioId] ON [dbo].[Solicitacaos]([FuncionarioId])
CREATE INDEX [IX_ClienteId] ON [dbo].[Solicitacaos]([ClienteId])

CREATE TABLE [dbo].[segurancaLogins] (
    [Id] [int] NOT NULL IDENTITY,
    [IdUsuario] [int] NOT NULL,
    [EmailUsuario] [nvarchar](max),
    [Quantidade] [int] NOT NULL,
    CONSTRAINT [PK_dbo.segurancaLogins] PRIMARY KEY ([Id])
)

ALTER TABLE [dbo].[Agendamentoes] ADD CONSTRAINT [FK_dbo.Agendamentoes_dbo.Clientes_ClienteId] FOREIGN KEY ([ClienteId]) REFERENCES [dbo].[Clientes] ([Id]) ON DELETE CASCADE
ALTER TABLE [dbo].[Agendamentoes] ADD CONSTRAINT [FK_dbo.Agendamentoes_dbo.Funcionarios_FuncionarioId] FOREIGN KEY ([FuncionarioId]) REFERENCES [dbo].[Funcionarios] ([Id]) ON DELETE CASCADE
ALTER TABLE [dbo].[Funcionarios] ADD CONSTRAINT [FK_dbo.Funcionarios_dbo.Cargoes_CargoId] FOREIGN KEY ([CargoId]) REFERENCES [dbo].[Cargoes] ([Id]) ON DELETE CASCADE
ALTER TABLE [dbo].[Avaliacaos] ADD CONSTRAINT [FK_dbo.Avaliacaos_dbo.Agendamentoes_AgendamentoId] FOREIGN KEY ([AgendamentoId]) REFERENCES [dbo].[Agendamentoes] ([Id]) ON DELETE CASCADE
ALTER TABLE [dbo].[Caixas] ADD CONSTRAINT [FK_dbo.Caixas_dbo.Agendamentoes_AgendamentoId] FOREIGN KEY ([AgendamentoId]) REFERENCES [dbo].[Agendamentoes] ([Id]) ON DELETE CASCADE
ALTER TABLE [dbo].[EnderecoFuncionarios] ADD CONSTRAINT [FK_dbo.EnderecoFuncionarios_dbo.Funcionarios_FuncionarioId] FOREIGN KEY ([FuncionarioId]) REFERENCES [dbo].[Funcionarios] ([Id]) ON DELETE CASCADE
ALTER TABLE [dbo].[LoginClientes] ADD CONSTRAINT [FK_dbo.LoginClientes_dbo.Clientes_ClienteId] FOREIGN KEY ([ClienteId]) REFERENCES [dbo].[Clientes] ([Id]) ON DELETE CASCADE
ALTER TABLE [dbo].[LoginFuncionarios] ADD CONSTRAINT [FK_dbo.LoginFuncionarios_dbo.Funcionarios_FuncionarioId] FOREIGN KEY ([FuncionarioId]) REFERENCES [dbo].[Funcionarios] ([Id]) ON DELETE CASCADE
ALTER TABLE [dbo].[LoginFuncionarios] ADD CONSTRAINT [FK_dbo.LoginFuncionarios_dbo.Permissoes_PermissoesId] FOREIGN KEY ([PermissoesId]) REFERENCES [dbo].[Permissoes] ([Id]) ON DELETE CASCADE
ALTER TABLE [dbo].[ServicosAgendamentoes] ADD CONSTRAINT [FK_dbo.ServicosAgendamentoes_dbo.Agendamentoes_AgendamentoId] FOREIGN KEY ([AgendamentoId]) REFERENCES [dbo].[Agendamentoes] ([Id]) ON DELETE CASCADE
ALTER TABLE [dbo].[ServicosAgendamentoes] ADD CONSTRAINT [FK_dbo.ServicosAgendamentoes_dbo.Servicoes_ServicoId] FOREIGN KEY ([ServicoId]) REFERENCES [dbo].[Servicoes] ([Id]) ON DELETE CASCADE
ALTER TABLE [dbo].[ServicosSolicitacaos] ADD CONSTRAINT [FK_dbo.ServicosSolicitacaos_dbo.Servicoes_ServicoId] FOREIGN KEY ([ServicoId]) REFERENCES [dbo].[Servicoes] ([Id]) ON DELETE CASCADE
ALTER TABLE [dbo].[ServicosSolicitacaos] ADD CONSTRAINT [FK_dbo.ServicosSolicitacaos_dbo.Solicitacaos_SolicitacaoId] FOREIGN KEY ([SolicitacaoId]) REFERENCES [dbo].[Solicitacaos] ([Id]) ON DELETE CASCADE
ALTER TABLE [dbo].[Solicitacaos] ADD CONSTRAINT [FK_dbo.Solicitacaos_dbo.Clientes_ClienteId] FOREIGN KEY ([ClienteId]) REFERENCES [dbo].[Clientes] ([Id]) ON DELETE CASCADE
ALTER TABLE [dbo].[Solicitacaos] ADD CONSTRAINT [FK_dbo.Solicitacaos_dbo.Funcionarios_FuncionarioId] FOREIGN KEY ([FuncionarioId]) REFERENCES [dbo].[Funcionarios] ([Id]) ON DELETE CASCADE


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
	nome VARCHAR(50) NOT NULL,
	descricao VARCHAR(100)
);
SELECT * FROM cargos
INSERT INTO cargos (nome, descricao) VALUES ('cabelereiro', 'cortes masculinos');

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

CREATE TABLE usuario_funcionarios(
	id INT IDENTITY NOT NULL,
	id_funcionario INT NOT NULL,
	usuario VARCHAR(40) NOT NULL CONSTRAINT UqUsuarioFuncionario UNIQUE,
	senha VARCHAR(20) NOT NULL
);

CREATE TABLE horarios_funcionarios(
    id INT IDENTITY NOT NULL,
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

DROP TABLE clientes;
CREATE TABLE clientes(
	id INT IDENTITY NOT NULL,
	nome VARCHAR(100) NOT NULL,
	data_nascimento DATE,
	celular VARCHAR(15),
	telefone VARCHAR(15),
	email VARCHAR(100)
);

SELECT * FROM clientes

CREATE TABLE usuario_clientes(
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
ALTER TABLE usuario_funcionarios ADD CONSTRAINT PKUsuarioFuncionarios PRIMARY KEY(id);
ALTER TABLE servicos ADD CONSTRAINT PKServicos PRIMARY KEY(id);
ALTER TABLE agendamentos ADD CONSTRAINT PKAgendamentos PRIMARY KEY(id);
ALTER TABLE horarios_funcionarios ADD CONSTRAINT PKHorarios PRIMARY KEY(id);
ALTER TABLE clientes ADD CONSTRAINT PKClientes PRIMARY KEY(id);
ALTER TABLE usuario_clientes ADD CONSTRAINT PKUsuarioClientes PRIMARY KEY(id);
ALTER TABLE solicitacoes ADD CONSTRAINT PKSolicitacoes PRIMARY KEY(id);

-- CHAVES ESTRANGEIRAS (RELACIONAMENTOS)
ALTER TABLE funcionarios ADD CONSTRAINT FKCargoFuncionario FOREIGN KEY(id_cargo) REFERENCES cargos(id);
ALTER TABLE funcionarios ADD CONSTRAINT FKEnderecoFuncionario FOREIGN KEY(id_endereco) REFERENCES enderecos(id);
ALTER TABLE usuario_funcionarios ADD CONSTRAINT FKUsuarioFuncionario FOREIGN KEY(id_funcionario) REFERENCES funcionarios(id);
ALTER TABLE agendamentos ADD CONSTRAINT FKFuncionarioAgendamento FOREIGN KEY(id_funcionario) REFERENCES funcionarios(id);
ALTER TABLE agendamentos ADD CONSTRAINT FKClienteAgendamento FOREIGN KEY(id_cliente) REFERENCES clientes(id);
ALTER TABLE agendamentos ADD CONSTRAINT FKServicoAgendamento FOREIGN KEY(id_servico) REFERENCES servicos(id);
ALTER TABLE usuario_clientes ADD CONSTRAINT FKUsuarioCliente FOREIGN KEY(id_cliente) REFERENCES clientes(id);

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