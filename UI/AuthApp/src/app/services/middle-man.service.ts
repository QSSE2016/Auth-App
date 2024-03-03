import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class MiddleManService {

  port = 7023
  url = `https://localhost:${this.port}/api/`
  constructor(private http: HttpClient) { }

  loginCheck(email: string,password: string) {
    return this.http.post(this.url + "login", {
      email: email,
      password: password
    },{withCredentials: true})
  }

  authWithCookie() {
    return this.http.get(this.url + "auth/cookies",{withCredentials: true})
  }
}
