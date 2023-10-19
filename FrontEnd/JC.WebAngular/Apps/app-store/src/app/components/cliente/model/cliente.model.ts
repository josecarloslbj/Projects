import { PerfilEntidade } from "../../perfis/entidade/perfil-entidade";
import { ArquivoModel } from "../../produto/model/produto-model";

export class ClienteModel {
    id: number = 0;
    tipoPessoa: number = 1;
    nome!: string | undefined;
    razaoSocial!: string | undefined;
    cpj_cnpj!: string | undefined;
    webSite!: string | undefined;
    email!: string | undefined;
    observacao!: string | undefined;
    telefone!: string | undefined;
    celular!: string | undefined;
    idEndereco!: string | undefined;
    idArquivo!: number | null;
    idContato!: string | undefined;
    dataCadastro!: Date | undefined;
    ativo!:boolean;
    arquivo!: ArquivoModel| null;
    
    // cpf!: string | undefined;
    // login!: string | undefined;
    // senha!: string | undefined;
  
    // telefoneCelular!: string | undefined;
    // ativo!: string | undefined;
    // perfis: PerfilEntidade[] = [];
}