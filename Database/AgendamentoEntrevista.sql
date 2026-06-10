CREATE TABLE AgendamentoEntrevista
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    IdAplicacao INT NOT NULL,
    Data DATE NOT NULL,
    Hora TIME NOT NULL,
    Local VARCHAR(300) NOT NULL,
    Observacao VARCHAR(1000) NULL,

    CONSTRAINT FK_AgendamentoEntrevista_CandidatoVaga
        FOREIGN KEY (IdAplicacao)
        REFERENCES CandidatoVaga(Id),

    CONSTRAINT UQ_AgendamentoEntrevista_IdAplicacao UNIQUE (IdAplicacao)
);
