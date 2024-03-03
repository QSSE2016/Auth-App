import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { jwtDecode } from 'jwt-decode';
import { Observable, throwError } from 'rxjs';

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

  authWithJWT() {
    let tokenFromStorage = localStorage.getItem("jwtToken")
    if(!this.tokenIsValid(tokenFromStorage)) {
      if(tokenFromStorage != null)
        localStorage.removeItem("jwtToken")

      return throwError("Token Invalid or doesn't exist")
    }

    var header = {
       headers: new HttpHeaders().set('Authorization',`Bearer ${tokenFromStorage}`),
       withCredentials: true
    }

    return this.http.get(this.url + "auth/jwt", header)
  }


  // Should probably go in a service of its own but whatever
  private tokenIsValid(token: string | null) : boolean {
    if(token == null)
      return false

    const decoded = jwtDecode(token) as {exp: number}
    return decoded.exp * 1000 > Date.now() // convert expiration to milliseconds
  }
}
