import configUrl from '../../assets/config/config.json'
import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { LoginRequest } from "../models/login-request";
import { map, Observable } from "rxjs";
import { LoginResponse } from "../models/login-response";
import { CookieOptions } from 'express';
import { Router } from '@angular/router';

@Injectable({ providedIn: "root" })
export class AuthService {

    private url = configUrl.ApiUrl.url + "/Auth";
    constructor(private httpClient: HttpClient, private router: Router) { }

    login(credentials: LoginRequest): Observable<LoginResponse> {
        return this.httpClient.post<LoginResponse>(this.url, credentials)
            .pipe(
                map(response => {
                    localStorage.setItem('accessToken', response.accessToken);
                    document.cookie = `refreshToken=${response.refreshToken};`;
                    return response;
                }));
    }

    logout(): void {
        localStorage.removeItem('accessToken');
        this.router.navigate(['login']);
    }

    isLoggedIn(): boolean {
        return localStorage.getItem('accessToken') !== null;
    }

    refreshToken(): Observable<LoginResponse> {
        const refreshToken = this.getRefreshTokenFromCookie();
        return this.httpClient.post<LoginResponse>(this.url, { refreshToken })
            .pipe(
                map(response => {
                    localStorage.setItem('accessToken', response.accessToken);
                    document.cookie = `refreshToken=${response.refreshToken};`;
                    return response;
                })
            );
    }

    private getRefreshTokenFromCookie(): string | null {
        const cookieString: string = document.cookie;
        const cookieArray: string[] = cookieString.split('; ');

        for (const cookie of cookieArray) {
            const [name, value] = cookie.split('=');
            if (name == 'refreshToken') {
                return value;
            }
        }
        return null;
    }
}