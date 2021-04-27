import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/services/auth.service';
import {  HttpClient, HttpHeaders, HttpParams  } from '@angular/common/http';
import { AuthOptions } from 'src/model/authOptions';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  password: string = "";
  username: string = "";
  currentUser: AuthOptions = new AuthOptions;
  constructor(private router: Router, private _authService: AuthService) { }

  ngOnInit() {
    
  }
onLogin(){console.log(this.username + ",,,," + this.password)
  this._authService.Login(this.username,this.password).subscribe(/*data=> this.currentUser = data*/);
    if(localStorage.getItem("Bearer")!=null)
    {
      console.log(localStorage.getItem("Bearer"));
      //console.log(localStorage.getItem("UserType"));
      this.router.navigateByUrl("chats")
    }
    else this.router.navigateByUrl("login")
  }
}
