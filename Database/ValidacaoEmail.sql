CREATE TABLE ValidacaoEmail
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    IdUsuario INT NOT NULL,
    TokenHash VARCHAR(200) NOT NULL,
    DataExpiracao DATETIME NOT NULL,
    DataCriacao DATETIME NOT NULL,
    DataValidacao DATETIME NULL,

    CONSTRAINT FK_ValidacaoEmail_Usuario
        FOREIGN KEY (IdUsuario)
        REFERENCES Usuario(Id)
);
