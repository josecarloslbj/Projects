import { PagedInputDTO } from "src/app/shared/paginator/pagedInputDTO";
import { PerfilEntidade } from "../../perfis/entidade/perfil-entidade";

export class FornecedorModel extends PagedInputDTO {
    id: number = 0;
    tipoPessoa: number = 2;
    nome!: string | undefined;
    razaoSocial!: string | undefined;
    cpF_CNPJ!: string | undefined;
    inscricaoEstadual!: string | undefined;
    inscricaoMunicipal!: string | undefined;
    webSite!: string | undefined;
    email!: string | undefined;
    observacao!: string | undefined;
    telefone!: string | undefined;
    celular!: string | undefined;
    idEndereco!: string | undefined;
    idContato!: string | undefined;
    dataCriacao!: Date | undefined;
}