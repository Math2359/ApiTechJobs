CREATE TABLE NotificacaoUsuario
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    IdUsuario INT NOT NULL,
    Titulo VARCHAR(200) NOT NULL,
    Mensagem VARCHAR(2000) NOT NULL,
    DataCadastro DATETIME NOT NULL DEFAULT GETDATE(),
    Lida BIT NOT NULL DEFAULT 0,

    [PropsAdicionais] VARCHAR(100) NULL, 
    CONSTRAINT FK_NotificacaoUsuario_Usuario
        FOREIGN KEY (IdUsuario)
        REFERENCES Usuario(Id)
);
