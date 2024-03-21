
import { Inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, BehaviorSubject } from 'rxjs';
import { tap } from 'rxjs/operators';
import { DOCUMENT } from '@angular/common';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private readonly apiUrl = 'http://localhost:5160/auth'; // Replace with your actual API URL
  private loggedIn = new BehaviorSubject<boolean>(this.tokenAvailable());

  constructor(private http: HttpClient) {
    
  }

  private tokenAvailable(): boolean {
    return !!localStorage.getItem('token');
  }

  getToken(): string | null {
    return localStorage.getItem('token');
  }

  get isLoggedIn(): Observable<boolean> {
    return this.loggedIn.asObservable();
  }

  login(email: string, password: string): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/login`, { email, password }).pipe(
      tap(response => {
        if (response && response.token) {
          localStorage.setItem('token', response.token);
          this.loggedIn.next(true);
        }
      })
    );
  }

  register(userInfo: any): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/register`, userInfo);
  }

  logout(): void {
    localStorage.removeItem('token');
    this.loggedIn.next(false);
  }
}
