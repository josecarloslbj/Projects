import { PerfilEntidade } from "../../perfis/entidade/perfil-entidade";
import { ArquivoModel } from "../../produto/model/produto-model";

export class UsuarioModel {
    id: number = 0;
    nome!: string | undefined;
    observacao!: string | undefined;
    cpf!: string | undefined;
    login!: string | undefined;
    senha!: string | undefined;
    email!: string | undefined;
    telefoneCelular!: string | undefined;
    ativo!: string | undefined;
    dataCriacao!: Date | undefined;
    perfis: PerfilEntidade[] = [];
    idArquivo!: number;
    arquivo!: ArquivoModel;
}