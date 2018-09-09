INSERT INTO Clientes (Nome, Data_nascimento, Celular, Telefone, Email)
VALUES ('Alan', '2000-05-05', '479998956', '4566666666', 'alaneduardoalves2018@gmail.com')

INSERT INTO Cargoes (Nome) VALUES ('Cabeleireiro');
INSERT INTO Funcionarios (CargoId, Nome, DataNascimento, Telefone, Cpf) VALUES (1,'Administrador', '1999-09-09', '11991578765', '98798798776');
INSERT INTO Funcionarios (CargoId, Nome, DataNascimento, Telefone, Cpf) VALUES (1,'Funcionario', '2000-11-11', '22222222222', '11111111111');
INSERT INTO LoginFuncionarios (Senha, FuncionarioId, PermissaoId , Usuario) VALUES ('fun',1007, 2,'fun');

INSERT INTO Permissoes (TipoPermissao) VALUES ('Funcionario')


SELECT * FROM Permissoes
SELECT * FROM LoginFuncionarios
SELECT * FROM Funcionarios

INSERT INTO LoginClientes (Usuario, Senha, ClienteId) 
VALUES ('Alanedu2000', '123456', 1)






SELECT * FROM Cargoes
DELETE FROM Funcionarios WHERE Id = 1006