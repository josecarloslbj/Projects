export class PaisModel {
    id: number = 0;
    nome!: string | undefined;
    sigla!: string | undefined;
    descricao!: string | undefined;
    abreviacao!: string | undefined;
    codigo!: number | undefined;
    codigoTelefone!: number | undefined;
    padrao: Boolean = false;
    ativo: boolean = true;
}
