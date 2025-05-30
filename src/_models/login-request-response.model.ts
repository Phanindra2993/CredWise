import { User } from "./user.model";

export interface LoginRequest {
    email: string;
    password: string;
}

export interface LoginResponse {
    success: boolean;
    data: {
        token: string;
        user: User;
    };
    message: string;
    errors: any;
}
  