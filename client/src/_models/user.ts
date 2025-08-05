export type User = {
    id: string;
    username: string;
    email: string;
    token: string;
    imageUrl?: string;
}

export type LoginCreds = {
    email: string;
    password: string;
}

export type RegisterCreds = {
    email: string;
    username: string;
    password: string;
}