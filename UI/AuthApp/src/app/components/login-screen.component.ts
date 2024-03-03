import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormGroup,FormControl, Validators } from '@angular/forms';
import { MiddleManService } from '../services/middle-man.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-login-screen',
  templateUrl: './login-screen.component.html',
  styleUrls: ['./login-screen.component.css']
})
export class LoginScreenComponent implements OnDestroy,OnInit {
    currAuthMethod: string = 'JWT'
    loginForm: FormGroup
    loginSub?: Subscription
    cookieAuthSub?: Subscription
    jwtAuthSub?: Subscription
    alreadySubmittedRequest = false // prevent multiple requests on log in

    constructor(private middleman: MiddleManService) {
      this.loginForm = new FormGroup({
         email: new FormControl('',[Validators.email,Validators.required]),
         password: new FormControl('',[Validators.required])
      })
    }

    ngOnInit(): void {
      let storageConfig = localStorage.getItem("auth-method")
      this.currAuthMethod = storageConfig == null ? 'Cookie' : storageConfig
      this.authenticateViaCredentials()
    }


    login() {
      if(this.alreadySubmittedRequest)
        return

      if(this.loginForm.invalid) {
        alert("Please fill out all the fields and make sure the email field actually has an email.")
        return
      }

      this.alreadySubmittedRequest = true
      this.loginSub = this.middleman.loginCheck(this.loginForm.controls['email'].value,this.loginForm.controls['password'].value).subscribe({
        next: (value: any) => {
          if(value.loginResult == 0)
            alert("Incorrect Password.")
          else {
            alert("Logged in!")
            localStorage.setItem("jwtToken",value.jwtToken) // store the jwt token 
          }
          this.alreadySubmittedRequest = false
        },
        error: (value) => {
          console.log(value)
          alert("The email you entered doesn't correspond to an account. Please enter another one.")
          this.alreadySubmittedRequest = false
        }
      })
    }


    changeAuthMethod() {
      this.currAuthMethod = this.oppositeOfCurrentAuth
      this.authenticateViaCredentials()
      localStorage.setItem("auth-method",this.currAuthMethod)
    }

    authenticateViaCredentials() {
      if(this.currAuthMethod == 'JWT')
        this.jwtAuth()
      else
        this.cookieAuth()
    }

    cookieAuth() {
      this.cookieAuthSub = this.middleman.authWithCookie().subscribe({
        next: (value) => {
          alert("Signed in with cookie!")
        },
        error: (value) => {
           console.log("No cookie found (must have expired).")
        }
      })
    }

    jwtAuth() {
      this.jwtAuthSub = this.middleman.authWithJWT().subscribe({
        next: (value) => {
          alert("Signed in with JWT!")
        },
        error: (value) => {
           console.log("No JWT found.")
        }
      })
    }


    // UI text getters
    get oppositeOfCurrentAuth() {
      return this.currAuthMethod == 'Cookies' ? 'JWT' : 'Cookies'
    }

    ngOnDestroy(): void {
        this.loginSub?.unsubscribe()
        this.cookieAuthSub?.unsubscribe()
    }
}
