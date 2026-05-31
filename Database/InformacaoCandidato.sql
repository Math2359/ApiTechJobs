CREATE TABLE InformacaoCandidato (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    IdCandidato INT NOT NULL,
    Descricao VARCHAR(MAX) NULL,
    Habilidades VARCHAR(2000) NULL,
    EmailPessoal VARCHAR(250) NULL,
    EmailCorporativo VARCHAR(250) NULL,
    Telefone VARCHAR(20) NULL,
    Linkedin VARCHAR(500) NULL,
    Github VARCHAR(500) NULL,

    CONSTRAINT FK_InformacaoCandidato_Candidato
        FOREIGN KEY (IdCandidato)
        REFERENCES Candidato(Id),

    CONSTRAINT UQ_InformacaoCandidato_IdCandidato
        UNIQUE (IdCandidato)
);