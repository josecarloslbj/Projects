import { PagedInputDTO } from "src/app/shared/paginator/pagedInputDTO";
import { PaisModel } from "../../pais/model/pais-model";

export class EstadoModel extends PagedInputDTO {
    id!: number;
    nome!: string | undefined;
    codigoUf!: string | undefined;
    idPais!: number;
    pais!: PaisModel;
    padrao!: Boolean;
}

