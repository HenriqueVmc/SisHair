INSERT INTO Clientes (Nome, Data_nascimento, Celular, Telefone, Email)
VALUES ('Alan', '2000-05-05', '479998956', '4566666666', 'alaneduardoalves2018@gmail.com')

INSERT INTO Cargoes (Nome) VALUES ('Cabeleireiro');
INSERT INTO Funcionarios (CargoId, Nome, DataNascimento, Telefone, Cpf) VALUES (1,'Administrador', '1999-09-09', '11991578765', '98798798776');
INSERT INTO LoginFuncionarios (FuncionarioId, Usuario, Senha ) VALUES (1, 'adm', 'adm');

SELECT * FROM Clientes
SELECT * FROM LoginClientes

INSERT INTO LoginClientes (Usuario, Senha, ClienteId) 
VALUES ('Alanedu2000', '123456', 1)






SELECT * FROM CodigoClientes
DELETE FROM CodigoClientes WHERE Id = 9