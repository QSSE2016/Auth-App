import { Component, OnDestroy } from '@angular/core';
import { FormGroup,FormControl, Validators } from '@angular/forms';
import { MiddleManService } from '../services/middle-man.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-login-screen',
  templateUrl: './login-screen.component.html',
  styleUrls: ['./login-screen.component.css']
})
export class LoginScreenComponent implements OnDestroy {
    currAuthMethod: string = 'JWT'
    loginForm: FormGroup
    loginSub?: Subscription
    alreadySubmittedRequest = false // prevent multiple requests on log in

    constructor(private middleman: MiddleManService) {
      this.loginForm = new FormGroup({
         email: new FormControl('',[Validators.email,Validators.required]),
         password: new FormControl('',[Validators.required])
      })
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
          }
          this.alreadySubmittedRequest = false
        },
        error: (value) => {
          alert("The email you entered doesn't correspond to an account. Please enter another one.")
          this.alreadySubmittedRequest = false
        }
      })
    }


    changeAuthMethod() {
      this.currAuthMethod = this.oppositeOfCurrentAuth

      // Check if we can log in instantly with new method (later)
    }


    // UI text getters
    get oppositeOfCurrentAuth() {
      return this.currAuthMethod == 'Cookies' ? 'JWT' : 'Cookies'
    }

    ngOnDestroy(): void {
        this.loginSub?.unsubscribe()
    }
}
