export class PerfilEntidade {
    id!: number;
    nome!: string | undefined;
    descricao!: string | undefined;
    permissoes: PermissaoEntidade[] = [];
    dataCriacao!: Date | undefined;
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
