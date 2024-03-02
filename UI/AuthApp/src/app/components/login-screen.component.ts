import { Component } from '@angular/core';
import { FormGroup,FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-login-screen',
  templateUrl: './login-screen.component.html',
  styleUrls: ['./login-screen.component.css']
})
export class LoginScreenComponent {
    currAuthMethod: string = 'JWT'
    loginForm: FormGroup

    constructor() {
      this.loginForm = new FormGroup({
         email: new FormControl('',[Validators.email,Validators.required]),
         password: new FormControl('',[Validators.required])
      })
    }


    login() {
      if(this.loginForm.invalid) {
        alert("Please fill out all the fields and make sure the email field actually has an email.")
        return
      }

      alert("Submitting...")
    }


    changeAuthMethod() {
      this.currAuthMethod = this.oppositeOfCurrentAuth

      // Check if we can log in instantly with new method (later)
    }


    // UI text getters
    get oppositeOfCurrentAuth() {
      return this.currAuthMethod == 'Cookies' ? 'JWT' : 'Cookies'
    }
}
