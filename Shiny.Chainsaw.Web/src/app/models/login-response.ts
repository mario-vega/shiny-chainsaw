export interface LoginResponse {
    accessToken: string;
    userdId: string;
    tokenType: string;
    expiresIn: number;
    refreshToken: number;
}