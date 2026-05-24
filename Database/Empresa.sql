CREATE TABLE Empresa
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    IdUsuario INT NOT NULL,
    Nome VARCHAR(200) NOT NULL,
    Email VARCHAR(200) NOT NULL,
    Cnpj CHAR(14) NOT NULL,
    Cep CHAR(8) NOT NULL,
    Numero VARCHAR(20) NOT NULL,

    CONSTRAINT FK_Empresa_Usuario
        FOREIGN KEY (IdUsuario)
        REFERENCES Usuario(Id),

    CONSTRAINT UQ_Empresa_Cnpj UNIQUE (Cnpj),
    CONSTRAINT UQ_Empresa_IdUsuario UNIQUE (IdUsuario)
);