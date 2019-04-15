USE dapper_types

INSERT INTO [country]
([name]) 
VALUES 
('United States'),
('Brazil')

INSERT INTO [state]
([name],[id_country])
VALUES
('Minas Gerais', 2),
('Acre', 2),
('Georgia', 1),
('District of Columbia', 1)

INSERT INTO [address]
([street],[number],[city],[id_state], [zip])
VALUES
('Rua Antonio de Albuquerque', '330', 'Belo Horizonte', 1, 30112010),
('St SW', '300', 'Washington', 2, 20546)

INSERT INTO [account]
([amount])
VALUES
(1000000),
(2000000)

INSERT INTO [customer]
([number],[name],[id_account],[id_address])
VALUES
(007, 'James Bond', 1, 1),
(001, 'Buzz Aldrin', 2, 2)
