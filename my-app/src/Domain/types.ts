export enum responseStatus {
    ok = 200,
    notFound = 404,
    error = 500,
    unauthorized = 401,
    invalidInput = 400,
    conflict = 409,
    forbidden = 403,
    timeout = 408,
    cancelled = 499,
    alreadyExists = 409
}

export interface apiResponse<T> {
    statusCode: number;
    status: responseStatus;
    message: string;
    Message: string;
    data: T | null;
    Data?: T | null;  // C Вебсокета
    isSuccess: boolean;
    IsSuccess: boolean;
    errorCode?: string;
    details?: string;
}

export interface userDTO {
    idOfUser?: string;
    email?: string;
    userName?: string;
    name?: string;
    surname?: string;
    patronymic?: string;
    password?: string;
    avatar?: string;
    token?: string;
    balance?: number;
}
