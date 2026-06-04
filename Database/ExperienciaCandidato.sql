CREATE TABLE ExperienciaCandidato(
    IdCandidato INT NOT NULL,
    TipoExperiencia INT NOT NULL,
    Instituicao VARCHAR(250) NOT NULL,
    Descricao VARCHAR(250) NOT NULL,
    DataInicio DATETIME NOT NULL,
    DataFim DATETIME NULL,

    CONSTRAINT FK_ExperienciaCandidato_Candidato
        FOREIGN KEY (IdCandidato)
        REFERENCES Candidato(Id)
);