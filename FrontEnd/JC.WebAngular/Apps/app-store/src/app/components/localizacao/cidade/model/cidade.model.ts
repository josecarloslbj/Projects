import { PagedInputDTO } from "src/app/shared/paginator/pagedInputDTO";
import { EstadoModel } from "../../estado/model/estado.model";

export class CidadeModel  extends PagedInputDTO{
    id!: number;
    nome!: string | undefined;
    descricao!: string | undefined;
    idEstado!: number | undefined;
    estado!: EstadoModel | undefined;
    padrao!: Boolean;
}
