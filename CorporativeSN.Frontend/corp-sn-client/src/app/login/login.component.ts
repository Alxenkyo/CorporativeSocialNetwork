import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/services/auth.service';
import {  HttpClient, HttpHeaders, HttpParams  } from '@angular/common/http';
import { AuthOptions } from 'src/model/authOptions';
import { AppComponent } from '../app.component';
import { UserService } from 'src/services/user.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  password: string = "";
  username: string = "";
  incorrect: boolean = false;
  currentUser: AuthOptions = new AuthOptions;
  constructor(private router: Router, private _authService: AuthService, private _userService: UserService) { }

ngOnInit() {}

onLogin(){
  if(this.password=="" || this.username==""){
    this.incorrect=true;
  }
  else{
  this.incorrect=false;
  var promise = this._authService.Login(this.username,this.password);
  promise.then(response=>{
    if(response){
      AppComponent.isLogged=true;
      var role = localStorage.getItem('UserRole') ?? "unknown";
      if(role == "admin")
      AppComponent.isAdmin=true;
      else AppComponent.isAdmin=false;
      this._userService.GetProfileData().then(value=>{
        AppComponent.profile=value})
      this.router.navigateByUrl("/chats")
      //AppComponent.Connect();
    }
  })}
}
}