INSERT INTO Clients (IdDoc, Name, Email, Street, City, State, PostalCode, PhoneNumber, CreatedAt, IsDeleted)
VALUES
-- Clientes com CPF (11 caracteres)
('12345678909', 'Carlos Oliveira', 'carlos.oliveira@email.com', 'Rua das Palmeiras, 120', 'São Paulo', 'SP', '01000000', '11987654321', GETDATE(), 0),
('98765432100', 'Mariana Santos', 'mariana.santos@email.com', 'Avenida Paulista, 1000', 'São Paulo', 'SP', '01310100', '11999887766', GETDATE(), 0),
('45678912355', 'Ricardo Lima', 'ricardo.lima@email.com', 'Rua XV de Novembro, 321', 'Curitiba', 'PR', '80020310', '41912345678', GETDATE(), 0),
('65432198733', 'Fernanda Souza', 'fernanda.souza@email.com', 'Avenida Brasil, 500', 'Rio de Janeiro', 'RJ', '22250040', '21976543210', GETDATE(), 0),
('78912345622', 'Gustavo Almeida', 'gustavo.almeida@email.com', 'Rua da Consolação, 777', 'Belo Horizonte', 'MG', '30120000', '31988776655', GETDATE(), 0),

-- Clientes com CNPJ (14 caracteres)
('12345678000190', 'Tech Solutions LTDA', 'contato@techsolutions.com.br', 'Rua das Inovações, 45', 'São Paulo', 'SP', '04567890', '1140028922', GETDATE(), 0),
('98765432000101', 'InovaSoft Tecnologia', 'suporte@inovasoft.com', 'Avenida do Progresso, 300', 'Porto Alegre', 'RS', '90220150', '5130304040', GETDATE(), 0),
('45678912000112', 'MegaTech Comércio', 'vendas@megatech.com.br', 'Rua dos Computadores, 89', 'Campinas', 'SP', '13040080', '19999991111', GETDATE(), 0),
('65432198000143', 'InfoBrasil Soluções', 'atendimento@infobrasil.com', 'Avenida Principal, 1234', 'Fortaleza', 'CE', '60060160', '8532325656', GETDATE(), 0),
('78123654000155', 'NetFast Provedores', 'contato@netfast.com.br', 'Rua da Internet, 99', 'Florianópolis', 'SC', '88015600', '4840032020', GETDATE(), 0);
