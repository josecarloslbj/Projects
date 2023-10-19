export class UsuarioEntity {

    username!: string | undefined;
    password!: string | undefined;
}

export class User {
    id!: number;
    username!: string | undefined;
    password!: string | undefined;
    firstName!: string | undefined;
    lastName!: string | undefined;
    jwtToken?: string | undefined;
}