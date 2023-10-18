CREATE TABLE Perfil (
    Id int NOT NULL AUTO_INCREMENT,
    Nome varchar(255) NOT NULL,
    Descricao varchar(255),    
    DataCriacao DATETIME,
    Ativo TINYINT NULL DEFAULT 1, 
    Usuario_Id INT NULL  DEFAULT 1,
    PRIMARY KEY (Id)
);

CREATE TABLE CategoriaPermissao (
    Id int NOT NULL AUTO_INCREMENT,
    Nome varchar(255) NOT NULL,
    Descricao varchar(255),    
    Icon varchar(255)  NULL,
    Ordem int NULL,
    Usuario_Id INT NULL  DEFAULT 1,
    Ativo TINYINT NULL DEFAULT 1,
    PRIMARY KEY (Id)
);

CREATE TABLE Permissao (
    Id int NOT NULL AUTO_INCREMENT,
    Categoria_Id INT, 
    Nome varchar(255) NOT NULL,
    Descricao varchar(255),    
    Ajuda varchar(255),  
    Usuario_Id INT NULL  DEFAULT 1,
    Ativo TINYINT NULL DEFAULT 1,
    PRIMARY KEY (Id)
);

CREATE TABLE PermissaoPerfil (
    Id int NOT NULL AUTO_INCREMENT,
    Perfil_Id INT, 
    Permissao_Id INT,
    Ativo TINYINT NULL DEFAULT 1,
    Usuario_Id INT NULL DEFAULT 1,
    PRIMARY KEY (Id)
);


 CREATE TABLE Arquivo (
  Id INT NOT NULL AUTO_INCREMENT,
  Nome VARCHAR(500) NULL,
  Diretorio VARCHAR(500) NULL,
  SubDiretorio VARCHAR(500) NULL,
  Extensao VARCHAR(500) NULL,
  Entidade VARCHAR(500) NULL,
  IdEntidade INT NULL,
  Ativo TINYINT NULL DEFAULT 1,
  Usuario_Id INT NULL ,
  PRIMARY KEY (Id) 
 );

 
CREATE TABLE Pais (
    Id int NOT NULL AUTO_INCREMENT,
    Sigla VARCHAR(450),
    Nome VARCHAR(450), 
    Descricao VARCHAR(450), 
    Abreviacao VARCHAR(450), 
    Codigo INT,
    CodigoTelefone INT,
    Padrao TINYINT NULL,
    Usuario_Id INT NULL DEFAULT 1,
    Ativo TINYINT NULL DEFAULT 1,
    PRIMARY KEY (Id)
);

CREATE TABLE Regiao (
  Id 	INT 		NOT NULL AUTO_INCREMENT,
  Nome 	VARCHAR(50) NOT NULL,
  Usuario_Id INT NULL DEFAULT 1,
  Ativo TINYINT NULL DEFAULT 1,
  PRIMARY KEY (Id)
);

CREATE TABLE Estado (
    Id       INT          NOT NULL AUTO_INCREMENT,
    CodigoUf INT          NOT NULL,
    Nome     VARCHAR(50)  NOT NULL,
    Uf       VARCHAR(10)  NOT NULL,
    Regiao_Id   INT	NULL,
    Padrao TINYINT NULL,
    Pais_Id INT NULL,  
    Usuario_Id INT NULL DEFAULT 1,
    Ativo TINYINT NULL DEFAULT 1,
  PRIMARY KEY (Id),
  INDEX Pais_Id_FK_idx (Regiao_Id ASC),
  INDEX Regiao_Id_FK_idx (Pais_Id ASC),
  CONSTRAINT FK_Pais_Id
    FOREIGN KEY (Pais_Id)
    REFERENCES Pais (Id)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT FK_Regiao_Id
    FOREIGN KEY (Regiao_Id)
    REFERENCES Regiao (Id)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION
 );


CREATE TABLE Cidade (
  Id INT NOT NULL AUTO_INCREMENT,
  Nome VARCHAR(500) NULL,
  Codigo INT NULL,
  Estado_Id INT NULL,
  Padrao TINYINT NULL,
  Usuario_Id INT NULL DEFAULT 1,
  Ativo TINYINT NULL DEFAULT 1,
  PRIMARY KEY (Id),
  INDEX Estado_Id_FK_idx (Estado_Id ASC),
  CONSTRAINT FK_Estado_Id
    FOREIGN KEY (Estado_Id)
    REFERENCES Estado (Id)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION
 );

CREATE TABLE Bairro (
  Id INT NOT NULL AUTO_INCREMENT,
  Nome VARCHAR(500) NULL,
  ZonaGeografica VARCHAR(500) NULL,
  RegionalAdministrativa VARCHAR(500) NULL,
  Populacao INT NULL,
  AreaKm2 DECIMAL(18,2) NULL,
  Cidade_Id INT NULL,  
  Padrao TINYINT NULL,
  Usuario_Id INT NULL DEFAULT 1,
  Ativo TINYINT NULL DEFAULT 1,
  PRIMARY KEY (Id),
  INDEX Cidade_Id_FK_idx (Id ASC),
  CONSTRAINT FK_Cidade_Id
    FOREIGN KEY (Id)
    REFERENCES Cidade (Id)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION
 );

 CREATE TABLE Endereco (
  Id INT NOT NULL AUTO_INCREMENT,
  Logradouro VARCHAR(500) NULL,
  Numero VARCHAR(500) NULL,
  Complemento VARCHAR(500) NULL,
  Referencia  VARCHAR(5000) NULL,
  GeoLocalizacao  VARCHAR(1000) NULL,
  Bairro_Id INT NULL,  
  Padrao TINYINT NULL,
  Usuario_Id INT NULL ,
  Ativo TINYINT NULL DEFAULT 1,
  PRIMARY KEY (Id),
  INDEX Bairro_Id_FK_idx (Id ASC),
  CONSTRAINT FK_Bairro_Id
    FOREIGN KEY (Id)
    REFERENCES Bairro (Id)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION
 );

CREATE TABLE Usuario (
  Id int NOT NULL AUTO_INCREMENT,
  Nome VARCHAR(500) NULL,
  CPF VARCHAR(45) NULL,
  Email VARCHAR(450) NULL,
  Login VARCHAR(450) NULL,
  Senha VARCHAR(450) NULL,
  Telefone VARCHAR(450) NULL,
  Celular VARCHAR(450) NULL,
  UltimaSenhaGerada VARCHAR(450) NULL,
  DataCadastro DATETIME NULL,
  Ativo TINYINT NULL DEFAULT 1,
  Arquivo_Id INT NULL, 
  Endereco_Id INT NULL,  
   INDEX Usuario_Endereco_Id_FK_idx (Endereco_Id ASC),  
   CONSTRAINT FK_Usuario_Endereco_Id
    FOREIGN KEY (Endereco_Id)
    REFERENCES Endereco (Id),
  INDEX Usuario_Arquivo_Id_FK_idx (Arquivo_Id ASC),  
   CONSTRAINT FK_Usuario_Arquivo_Id
    FOREIGN KEY (Arquivo_Id)
    REFERENCES Arquivo (Id),
  PRIMARY KEY (Id)
);

CREATE TABLE PerfilUsuario (
    Id int NOT NULL AUTO_INCREMENT,
    Perfil_Id INT, 
    Usuario_Id INT,
    Ativo TINYINT NULL DEFAULT 1,
    PRIMARY KEY (Id)
);



CREATE TABLE ContatoPessoa (
  Id INT NOT NULL AUTO_INCREMENT,
  Nome VARCHAR(500) NULL,
  Telefone VARCHAR(500) NULL,
  Celular VARCHAR(500) NULL,
  Setor VARCHAR(500) NULL,
  Endereco_Id INT NULL,  
  Usuario_Id INT NULL,
  Ativo TINYINT NULL DEFAULT 1,
  PRIMARY KEY (Id),
  INDEX Endereco_Id_FK_idx (Id ASC),
  CONSTRAINT FK_Endereco_Id
    FOREIGN KEY (Id)
    REFERENCES Endereco (Id)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION
 );

CREATE TABLE Fornecedor (
  Id INT NOT NULL AUTO_INCREMENT,
  Nome VARCHAR(500) NULL,
  RazaoSocial VARCHAR(500) NULL,
  CPF_CNPJ VARCHAR(500) NULL,
  TipoPessoa  INT NULL,
  InscricaoEstadual VARCHAR(500) NULL,
  InscricaoMunicipal VARCHAR(500) NULL,
  WebSite VARCHAR(500) NULL,
  Email VARCHAR(500) NULL,
  Observacao VARCHAR(5000) NULL,
  Telefone VARCHAR(50) NULL,
  Celular VARCHAR(50) NULL,
  Endereco_Id INT NULL,  
  Contato_Id INT NULL,  
  Usuario_Id INT NULL DEFAULT 1,
  Ativo TINYINT NULL DEFAULT 1,
  PRIMARY KEY (Id),
  INDEX Fornecedor_Endereco_Id_FK_idx (Endereco_Id ASC),  
  INDEX Fornecedor_Contato_Id_FK_idx (Contato_Id ASC), 
  CONSTRAINT FK_Fornecedor_Endereco_Id
    FOREIGN KEY (Endereco_Id)
    REFERENCES Endereco (Id),  
  CONSTRAINT FK_Fornecedor_Contato_Id
    FOREIGN KEY (Contato_Id)
    REFERENCES ContatoPessoa (Id)
 );

CREATE TABLE Fabricante (
  Id INT NOT NULL AUTO_INCREMENT,
  Nome VARCHAR(500) NULL,
  Descricao VARCHAR(500) NULL,
  Usuario_Id INT NULL DEFAULT 1,
  Ativo TINYINT NULL DEFAULT 1,
  PRIMARY KEY (Id) 
 );

CREATE TABLE GrupoProduto (
  Id INT NOT NULL AUTO_INCREMENT,
  Nome VARCHAR(500) NULL,
  Descricao VARCHAR(500) NULL,
  Usuario_Id INT DEFAULT 1,
  Ativo TINYINT NULL DEFAULT 1,
  PRIMARY KEY (Id) 
 );

CREATE TABLE Produto (
  Id INT NOT NULL AUTO_INCREMENT,
  Codigo VARCHAR(500) NULL,
  Nome VARCHAR(500) NULL,
  Descricao VARCHAR(500) NULL,
  Tipo VARCHAR(500) NULL,
  DataCadastro DATETIME,
  DataAlteracao DATETIME NULL,
  Preco DECIMAL(18,2),
  ValorCusto DECIMAL(18,2) NULL,
  PercentIPI DECIMAL(18,2) NULL,
  PercentLucro DECIMAL(18,2) NULL,
  Qtde  INT(11) NULL DEFAULT 1,
  QtdeMin  INT(11) NULL DEFAULT 2,
  Localizacao VARCHAR(500) NULL,
  UnidadeMedida VARCHAR(500) NULL,
  Ativo TINYINT NULL DEFAULT 1,
  Grupo_Id INT NULL,
  Fabricante_Id INT NULL,
  Fornecedor_Id INT,
  Arquivo_Id INT NULL,
  Usuario_Id INT NULL DEFAULT 1,
  PRIMARY KEY (Id),
  INDEX Grupo_Id_FK_idx (Grupo_Id ASC),
  INDEX Fabricante_Id_FK_idx (Fabricante_Id ASC),
  INDEX Fornecedor_Id_FK_idx (Fornecedor_Id ASC),
  CONSTRAINT FK_Grupo_Id
    FOREIGN KEY (Grupo_Id)
    REFERENCES GrupoProduto (Id)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT FK_Fabricante_Id
    FOREIGN KEY (Fabricante_Id)
    REFERENCES Fabricante (Id)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT FK_Fornecedor_Id
    FOREIGN KEY (Fornecedor_Id)
    REFERENCES Fornecedor (Id)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION
 );

 CREATE TABLE EstoqueProduto (
  Id INT NOT NULL AUTO_INCREMENT,
  Produto_Id INT,
  QtdeAtual INT NOT NULL DEFAULT 1,
  QtdeUltimaReposicao INT  NULL,
  DataReposicao DATETIME,
  Usuario_Id INT NULL DEFAULT 1,
  Observacao VARCHAR(500) NULL,
  Ativo TINYINT NULL DEFAULT 1,
  PRIMARY KEY (Id), 
  INDEX Usuario_Id_FK_idx (Usuario_Id ASC),
  INDEX Produto_Id_FK_idx (Produto_Id ASC),
  CONSTRAINT Usuario_Id_FK_idx
    FOREIGN KEY (Usuario_Id)
    REFERENCES Usuario (Id)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT Produto_Id_FK_idx
    FOREIGN KEY (Produto_Id)
    REFERENCES Produto (Id)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION 
 );

CREATE TABLE HistoricoEstoqueProduto (
  Id INT NOT NULL AUTO_INCREMENT,
  EstoqueProduto_Id INT,
  QtdeAtual INT NOT NULL DEFAULT 1,
  DataReposicao DATETIME,
  Usuario_Id INT NULL DEFAULT 1,
  Observacao VARCHAR(500) NULL,
  Ativo TINYINT NULL DEFAULT 1,
  PRIMARY KEY (Id), 
  INDEX EstoqueProduto_Id_FK_idx (EstoqueProduto_Id ASC),
  CONSTRAINT EstoqueProduto_Id_FK_idx
    FOREIGN KEY (EstoqueProduto_Id)
    REFERENCES EstoqueProduto (Id)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION  
 );

 CREATE TABLE Cliente (
  Id int NOT NULL AUTO_INCREMENT,
  Nome VARCHAR(500) NULL,
  TipoPessoa INT NULL ,
  CPF_CNPJ VARCHAR(45) NULL,
  Email VARCHAR(450) NULL,
  Login VARCHAR(450) NULL,
  Senha VARCHAR(450) NULL,
  UltimaSenhaGerada VARCHAR(450) NULL,
  DataCadastro DATETIME NULL DEFAULT NOW(),
  DataAlteracao DATETIME NULL,  
  Observacao VARCHAR(5000) NULL,
  Usuario_Id INT NULL DEFAULT 1,
  Arquivo_Id INT NULL ,
  Contato_Id INT NULL, 
  Endereco_Id INT NULL,  
  Ativo TINYINT NULL DEFAULT 1,
  INDEX Cliente_Endereco_Id_FK_idx (Endereco_Id ASC),  
   CONSTRAINT FK_Cliente_Endereco_Id
    FOREIGN KEY (Endereco_Id)
    REFERENCES Endereco (Id),
  INDEX Cliente_Arquivo_Id_FK_idx (Arquivo_Id ASC),  
   CONSTRAINT FK_Cliente_Arquivo_Id
    FOREIGN KEY (Arquivo_Id)
    REFERENCES Arquivo (Id),
 INDEX Cliente_Usuario_Id_FK_idx (Usuario_Id ASC),  
   CONSTRAINT FK_Cliente_Usuario_Id
    FOREIGN KEY (Usuario_Id)
    REFERENCES Usuario (Id),

  PRIMARY KEY (Id)
);

 CREATE TABLE HistoricoPedido (
  Id int NOT NULL AUTO_INCREMENT,
  Pedido_Id INT,
  Usuario_Id INT,
  DataCadastro DATETIME NULL DEFAULT NOW(),
  Situacao VARCHAR(450) NULL,
  Descricao VARCHAR(2550) NULL,
  Ativo TINYINT NULL DEFAULT 1,
  PRIMARY KEY (Id)
);



 CREATE TABLE Pedido (
  Id INT NOT NULL AUTO_INCREMENT,
  Codigo VARCHAR(500) NULL,
  Descricao VARCHAR(2500) NULL,
  FormaPagamento VARCHAR(500) NULL,
  SituacaoAtual VARCHAR(500) NULL,
  DataCadastro DATETIME DEFAULT NOW(),
  DataAlteracao DATETIME NULL,
  ValorPedido DECIMAL(18,2),
  ValorDesconto DECIMAL(18,2) NULL,
  ValorAcrescimo DECIMAL(18,2) NULL,
  ValorTotal DECIMAL(18,2),  
  Usuario_Id INT,
  Cliente_Id INT NULL,
  HistoricoAtual_Id INT NULL,
  PRIMARY KEY (Id),  
  INDEX Cliente_Id_FK_idx (Cliente_Id ASC),
  INDEX Usuario_Id_FK_idx (Usuario_Id ASC),
  INDEX HistoricoAtual_Id_FK_idx (HistoricoAtual_Id ASC),  
  CONSTRAINT FK_Cliente_Id
    FOREIGN KEY (Cliente_Id)
    REFERENCES Cliente (Id)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT FK_Usuario_Id
    FOREIGN KEY (Usuario_Id)
    REFERENCES Usuario (Id)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT FK_HistoricoAtual_Id
    FOREIGN KEY (HistoricoAtual_Id)
    REFERENCES HistoricoPedido (Id)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION
 );


 
 CREATE TABLE ItemPedido (
  Id INT NOT NULL AUTO_INCREMENT,
  Descricao VARCHAR(2500) NULL,
  Quantidade VARCHAR(500) NULL,
  SituacaoAtual VARCHAR(500) NULL,
  ValorUnitario DECIMAL(18,2),
  ValorDesconto DECIMAL(18,2) NULL,
  ValorAcrescimo DECIMAL(18,2) NULL,
  ValorTotal DECIMAL(18,2),  
  Produto_Id INT,  
  Pedido_Id INT,
  PRIMARY KEY (Id),  
  INDEX Produto_Id_FK_idx (Produto_Id ASC),
  INDEX Pedido_Id_FK_idx (Pedido_Id ASC),  
  CONSTRAINT FK_Produto_Id
    FOREIGN KEY (Produto_Id)
    REFERENCES Produto (Id)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT FK_Pedido_Id
    FOREIGN KEY (Pedido_Id)
    REFERENCES Pedido (Id)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION  
 );



 CREATE TABLE Caixa (
    Id int NOT NULL AUTO_INCREMENT,
    DataAbertura DATETIME NULL,
    DataFechamento DATETIME NULL,
    UsuarioAbertura_Id INT,  
    UsuarioFechamento_Id INT NULL,      
    UsuarioConferenciaAbertura_Id INT NULL,  
    UsuarioConferenciaFechamento_Id INT NULL,  
    Situacao VARCHAR(50) NULL,
    ValorInicial DECIMAL(18,2),
    ValorFinal DECIMAL(18,2) NULL,
    ObservacaoAbertura  VARCHAR(2000) NULL,
    ObservacaoFechamento  VARCHAR(2000) NULL,
    PRIMARY KEY (Id)
);

 CREATE TABLE HistoricoCaixa (
    Id int NOT NULL AUTO_INCREMENT,
    DataAtual DATETIME NULL,
    Motivo  VARCHAR(2000) NULL,
    Usuario_Id INT,   
    PRIMARY KEY (Id)
);

CREATE TABLE Unidade(
	Id int NOT NULL AUTO_INCREMENT,
	Nome varchar(255) NOT NULL,
    TipoUnidade int NOT NULL,
    CpfCnpj varchar(14) NOT NULL,
	Descricao varchar(2000) NOT NULL,
	IsUnidadePadrao TINYINT NOT NULL,
	Ativo TINYINT NOT NULL DEFAULT 1,
	Email varchar(255) NULL,
	TipoEmail varchar(255) NULL,	
	CamposComplementares varchar(5000) NULL,
	DataCriacao DATETIME NULL,
	DataUltimaAlteracao DATETIME NULL,
    Cep varchar(14) NOT NULL,
    Logradouro VARCHAR(500) NULL,
    Numero VARCHAR(500) NULL,
    Complemento VARCHAR(500) NULL,    
    Bairro_Id INT NULL,
    PRIMARY KEY (Id)
);