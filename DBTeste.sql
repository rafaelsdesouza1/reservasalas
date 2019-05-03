--Crie um banco com o nome ReservaSalasBD
-- E crie as três tabelas abaixo.

CREATE TABLE Locais (
    Id int IDENTITY(1,1) PRIMARY KEY,
    Nome varchar(255) NOT NULL
);

CREATE TABLE Salas (
    Id int IDENTITY(1,1) PRIMARY KEY,
    Nome varchar(255) NOT NULL,
	LocalId int
);

ALTER TABLE Salas  WITH CHECK ADD  CONSTRAINT FK_Salas_Locais FOREIGN KEY(LocalId)
REFERENCES Locais (Id)
GO

ALTER TABLE Salas CHECK CONSTRAINT FK_Salas_Locais
GO

CREATE TABLE Reservas (
    Id int IDENTITY(1,1) PRIMARY KEY,
    Nome varchar(255) NOT NULL,
	SalaId int,
	DtHrIni datetime,
	DtHrFim datetime,
	Responsavel varchar(255) NOT NULL,
	Cafe bit DEFAULT 0 NOT NULL,
	QtdePessoas int DEFAULT 0 NULL
);

ALTER TABLE Reservas  WITH CHECK ADD  CONSTRAINT FK_Reservas_Salas FOREIGN KEY(SalaId)
REFERENCES Salas (Id)
GO

ALTER TABLE Reservas CHECK CONSTRAINT FK_Reservas_Salas
GO