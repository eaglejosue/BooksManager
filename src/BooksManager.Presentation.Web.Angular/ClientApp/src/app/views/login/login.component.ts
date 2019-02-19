import { Component, Inject, ElementRef, ViewChild } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Router } from "@angular/router";

import { SignInModel } from "src/app/models/signIn.model";

@Component({
  selector: "app-login",
  templateUrl: "./login.component.html"
})
export class LoginComponent {

  baseUrl: string;
  signInModel: SignInModel;


  @ViewChild("userNameInput") userNameInput: ElementRef<HTMLInputElement>;
  @ViewChild("passwordInput") passwordInput: ElementRef<HTMLInputElement>;

  constructor(
    private readonly http: HttpClient,
    private readonly router: Router,
    @Inject("BASE_URL") baseUrl: string) {
    this.http = http;
    this.baseUrl = baseUrl;
  }

  onSubmit() {

    if (this.userNameInput.nativeElement.value === "") {
      this.userNameInput.nativeElement.focus();
      return;
    }

    if (this.passwordInput.nativeElement.value === "") {
      this.passwordInput.nativeElement.focus();
      return;
    }

    this.signInModel = new SignInModel();
    this.signInModel.login = this.userNameInput.nativeElement.value;
    this.signInModel.password = this.passwordInput.nativeElement.value;

    console.log(`Login: '${this.signInModel.login}'`);
    console.log(`Password: '${this.signInModel.password}'`);

    const url = this.baseUrl + "api/Authentication/SignIn/";
    const body = JSON.stringify(this.signInModel);
    console.log(`Dados: ${body}`);
    const options = {headers: new HttpHeaders({'Content-Type': "application/json"})};

    this.http.post(url, body, options)
      .subscribe(result => {
        console.log(`Login: '${this.signInModel.login}' | Password: '${this.signInModel.password}'`);
        this.router.navigate(["home"]);
      }, error => {
        console.log(`Login: '${this.signInModel.login}' | Password: '${this.signInModel.password}'`);
        console.error(error);
      });
  }
}
