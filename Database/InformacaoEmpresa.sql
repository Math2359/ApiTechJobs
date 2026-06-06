CREATE TABLE InformacaoEmpresa (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    IdEmpresa INT NOT NULL,

    Descricao VARCHAR(MAX) NULL,
    Setor VARCHAR(250) NULL,
    Tecnologias VARCHAR(2000) NULL,
    LinkSite VARCHAR(500) NULL,
    CONSTRAINT FK_InformacaoEmpresa_Empresa
        FOREIGN KEY (IdEmpresa)
        REFERENCES Empresa(Id),

    CONSTRAINT UQ_InformacaoEmpresa_IdEmpresa
        UNIQUE (IdEmpresa)
);