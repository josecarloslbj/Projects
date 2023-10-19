import { PagedInputDTO } from "src/app/shared/paginator/pagedInputDTO";
import { CidadeModel } from "../../cidade/model/cidade.model";


export class PerfilEntidade {
    id!: number;
    nome!: string | undefined;
    descricao!: string | undefined;
    permissoes: PermissaoEntidade[] = [];
    dataCriacao!: Date | undefined;
    padrao!: Boolean;
}

export class PermissaoEntidade {

    constructor() { }

    id!: number;
    key!: string | undefined;
    categoriaIcon!: string | undefined;
    value!: any[];
    selecionado!: boolean | undefined;
    nome!: string | undefined;
    descricao!: string | undefined;
    // categoria!: CategoriaPermissaoEntidade;
    categoriaName!: string | undefined;
}

export class CategoriaPermissaoEntidade {
    id!: number;
    nome!: string | undefined;
    descricao!: string | undefined;
    icon!: string | undefined;
    ordem!: number | undefined;
    permissoes!: PermissaoEntidade[];
}

export class BairroModel extends PagedInputDTO {
    id!: number;
    nome!: string | undefined;
    descricao!: string | undefined;
    idCidade!: number | undefined;
    cidade!: CidadeModel | undefined;
    padrao: Boolean = false;
    ativo: Boolean = true;
}
