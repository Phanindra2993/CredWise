import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { User } from '../_models/user.model';
import { LoginRequest, LoginResponse } from '../_models/login-request-response.model';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private registerUrl = 'https://localhost:7037/api/User/register';
  private loginUrl = 'https://localhost:7037/api/User/login';

  private currentUserSubject: BehaviorSubject<User | null>;
  public currentUser$: Observable<User | null>;
  private tokenSubject: BehaviorSubject<string | null>;
  public token$: Observable<string | null>;

  constructor(private http: HttpClient, private router: Router) {
    this.currentUserSubject = new BehaviorSubject<User | null>(null);
    this.tokenSubject = new BehaviorSubject<string | null>(null);
    this.currentUser$ = this.currentUserSubject.asObservable();
    this.token$ = this.tokenSubject.asObservable();
  }

  register(userData: any): Observable<any> {
    return this.http.post<any>(this.registerUrl, userData);
  }

  login(userData: LoginRequest): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(this.loginUrl, userData).pipe(
      tap(response => {
        if (response && response.success && response.data) {
          this.currentUserSubject.next(response.data.user);
          this.tokenSubject.next(response.data.token);
        }
      })
    );
  }

  logout() {
    this.currentUserSubject.next(null);
    this.tokenSubject.next(null);
    this.router.navigate(['/login']);
  }

  isLoggedIn(): boolean {
    return !!this.currentUserSubject.value;
  }

  public get currentUserValue(): User | null {
    return this.currentUserSubject.value;
  }

  gettoken(): string | null {
    return this.tokenSubject.value;
  }

  getUserId(): number {
    const currentUser = this.currentUserSubject.value;
    return currentUser ? currentUser.userId : 0;
  }
} 
