import { PagedInputDTO } from "src/app/shared/paginator/pagedInputDTO";
import { PerfilEntidade } from "../../perfis/entidade/perfil-entidade";

export class GrupoProdutoModel extends PagedInputDTO {
    id: number = 0;
    nome: string | undefined;
    descricao!: string | undefined;    
}