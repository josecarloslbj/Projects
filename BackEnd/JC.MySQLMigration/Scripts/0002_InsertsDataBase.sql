


INSERT INTO `CategoriaPermissao` (`Nome`, `Descricao`, `Icon`, `Ordem`) VALUES ('ADMINISTRACAO', 'Administração', 'pi pi-lock', '1');
INSERT INTO `CategoriaPermissao` (`Nome`, `Descricao`, `Icon`, `Ordem`) VALUES ('VENDAS', 'Vendas', 'pi pi-dollar', '2');
INSERT INTO `CategoriaPermissao` (`Nome`, `Descricao`, `Icon`, `Ordem`) VALUES ('RELATORIOS', 'Relatórios', 'pi pi-chart-bar', '3');
INSERT INTO `CategoriaPermissao` (`Nome`, `Descricao`, `Icon`, `Ordem`) VALUES ('CADASTROS', 'Cadastros', 'pi pi-user', '4');
INSERT INTO `CategoriaPermissao` (`Nome`, `Descricao`, `Icon`, `Ordem`) VALUES ('PRODUTOS', 'Produtos', 'pi pi-gift', '5');

INSERT INTO `Permissao` (`Categoria_Id`, `Nome`, `Descricao`, `Ajuda`) VALUES (
(SELECT id FROM CategoriaPermissao where Nome = 'ADMINISTRACAO' LIMIT 1), 
'ADMINISTRACAO_GERENCIAR_PERFIL', 
'Gerenciar Perfil',
'Permite Criar,Atualizar,Remover perfil');

INSERT INTO `Permissao` (`Categoria_Id`, `Nome`, `Descricao`, `Ajuda`) VALUES (
(SELECT id FROM CategoriaPermissao where Nome = 'ADMINISTRACAO' LIMIT 1), 
'ADMINISTRACAO_GERENCIAR_USUARIO', 
'Gerenciar Usuários',
'Permite Criar,Atualizar,Remover Usuário');


INSERT INTO `Permissao` (`Categoria_Id`, `Nome`, `Descricao`, `Ajuda`) VALUES (
(SELECT id FROM CategoriaPermissao where Nome = 'VENDAS' LIMIT 1), 
'VENDAS_GERENCIAR_VENDAS', 
'Gerenciar Vendas',
'Permite Criar,Atualizar,Remover vendas');

INSERT INTO `Permissao` (`Categoria_Id`, `Nome`, `Descricao`, `Ajuda`) VALUES (
(SELECT id FROM CategoriaPermissao where Nome = 'RELATORIOS' LIMIT 1), 
'RELATORIOS_GERENCIAR_RELATORIOS', 
'Gerenciar Relatórios',
'Permite Criar,Atualizar,Remover Relatórios');


INSERT INTO `Permissao` (`Categoria_Id`, `Nome`, `Descricao`, `Ajuda`) VALUES (
(SELECT id FROM CategoriaPermissao where Nome = 'RELATORIOS' LIMIT 1), 
'RELATORIOS_GERENCIAR_RELATORIOS_VENDA', 
'Visualizar Relatórios de vendas',
'Permite visualizar o relatório de vendas');


INSERT INTO `Permissao` (`Categoria_Id`, `Nome`, `Descricao`, `Ajuda`) VALUES (
(SELECT id FROM CategoriaPermissao where Nome = 'CADASTROS' LIMIT 1), 
'CADASTROS_GERENCIAR_FABRICANTES', 
'Gerenciar Fabricantes',
'Permite Criar,Atualizar,Remover Fabricantes');

INSERT INTO `Permissao` (`Categoria_Id`, `Nome`, `Descricao`, `Ajuda`) VALUES (
(SELECT id FROM CategoriaPermissao where Nome = 'CADASTROS' LIMIT 1), 
'CADASTROS_GERENCIAR_GRUPOPRODUTO', 
'Gerenciar Grupo de Produtos',
'Permite Criar,Atualizar,Remover Grupo de Produtos');

INSERT INTO `Permissao` (`Categoria_Id`, `Nome`, `Descricao`, `Ajuda`) VALUES (
(SELECT id FROM CategoriaPermissao where Nome = 'CADASTROS' LIMIT 1), 
'CADASTROS_GERENCIAR_FORNECEDORES', 
'Gerenciar Fornecedores',
'Permite Criar,Atualizar,Remover Fornecedores');

INSERT INTO `Permissao` (`Categoria_Id`, `Nome`, `Descricao`, `Ajuda`) VALUES (
(SELECT id FROM CategoriaPermissao where Nome = 'CADASTROS' LIMIT 1), 
'CADASTROS_GERENCIAR_CLIENTES', 
'Gerenciar Clientes',
'Permite Criar,Atualizar,Remover Clientes');


INSERT INTO `Permissao` (`Categoria_Id`, `Nome`, `Descricao`, `Ajuda`) VALUES (
(SELECT id FROM CategoriaPermissao where Nome = 'PRODUTOS' LIMIT 1), 
'PRODUTOS_PESQUISAR_PRODUTOS', 
'Pesquisa de Produtos',
'Permite pesquisar produtos');

INSERT INTO `Permissao` (`Categoria_Id`, `Nome`, `Descricao`, `Ajuda`) VALUES (
(SELECT id FROM CategoriaPermissao where Nome = 'PRODUTOS' LIMIT 1), 
'PRODUTOS_CADASTRAR_PRODUTOS', 
'Cadastro de Produtos',
'Permite cadastrar produtos');


INSERT INTO `Permissao` (`Categoria_Id`, `Nome`, `Descricao`, `Ajuda`) VALUES (
(SELECT id FROM CategoriaPermissao where Nome = 'PRODUTOS' LIMIT 1), 
'PRODUTOS_EDITAR_PRODUTOS', 
'Editar Cadastro de Produtos',
'Permite editar cadastrar produtos');


INSERT INTO `Permissao` (`Categoria_Id`, `Nome`, `Descricao`, `Ajuda`) VALUES (
(SELECT id FROM CategoriaPermissao where Nome = 'PRODUTOS' LIMIT 1), 
'PRODUTOS_EXCLUIR_PRODUTOS', 
'Exlcuir Cadastro de Produtos',
'Permite Excluir cadastrar produtos');

INSERT INTO `GrupoProduto` (`Nome`, `Descricao`, `Ativo`) VALUES ('Diversos', 'Grupo para produtos diversos', '1');
INSERT INTO `Fabricante` (`Nome`, `Descricao`, `Ativo`) VALUES ('Diversos', 'Fabricande genérico', '1');
INSERT INTO `Fornecedor` (`Nome`, `Observacao`) VALUES ('Diversos', 'Fabricante genérico');


