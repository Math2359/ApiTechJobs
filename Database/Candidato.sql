CREATE TABLE Candidato
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    IdUsuario INT NOT NULL,
    IdEmpresa INT NULL,
    Nome VARCHAR(200) NOT NULL,
    Email VARCHAR(200) NOT NULL,
    Cpf CHAR(11) NOT NULL,

    CONSTRAINT FK_Candidato_Usuario
        FOREIGN KEY (IdUsuario)
        REFERENCES Usuario(Id),

    CONSTRAINT FK_Candidato_Empresa
        FOREIGN KEY (IdEmpresa)
        REFERENCES Empresa(Id),

    CONSTRAINT UQ_Candidato_Cpf UNIQUE (Cpf),
    CONSTRAINT UQ_Candidato_IdUsuario UNIQUE (IdUsuario)
);