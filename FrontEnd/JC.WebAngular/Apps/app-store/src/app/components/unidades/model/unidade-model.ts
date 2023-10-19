import { PagedInputDTO } from "src/app/shared/paginator/pagedInputDTO";
import { BairroModel } from "../../localizacao/bairro/model/bairro.model";

export class UnidadeModel extends PagedInputDTO {
    id: number = 0;
    nome: string | undefined;
    cpfCnpj!: string | undefined;
    descricao!: string | undefined;
    email!: string | undefined;
    isUnidadePadrao: boolean | undefined;
    ativo!: boolean | false;
    tipoEmail: string | undefined;
    dataCriacao!: Date | undefined;
    dataUltimaAlteracao!: Date | undefined;

    cep?: string;
    logradouro?: string;
    numero?: string;
    complemento?: string;
    idBairro!: number | undefined;
    bairro!: BairroModel | undefined;
}