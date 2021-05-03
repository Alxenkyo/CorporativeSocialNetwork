import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'corp-sn-client';
  isUserLogin: boolean = false;
  static isLogged: boolean = false;
  get isLogged(){
    return AppComponent.isLogged;
}
  constructor(private router: Router, private _authService: AuthService) {}
  ngOnInit(){
    this.isLogin();
    this.isUserLogin=AppComponent.isLogged;
}
isLogin(){
  var promise = this._authService.IsUserLogin(); 
      promise.then(value => {
        if(value){ 
          this.router.navigate(['/chats']);
          AppComponent.isLogged=true;
        }else{ 
          this.router.navigate(['/login']);
          AppComponent.isLogged=false;
        }
      });  
}
Logout() {
  this.router.navigate(['/login']);
  this.isUserLogin = false;
  AppComponent.isLogged=false;
}
Profile(){};
  Chats(){};
  Calendar(){};
}
