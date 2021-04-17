import { Component } from '@angular/core';
import { Router } from '@angular/router';
//import {UserService} from './Services/user.service'
//import {LoginService} from './Services/login.service'
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  //currentUser: User;
    static isAdmin: boolean = false;
    //private _userService: UserService; 
    constructor(
        private router: Router,
       
    ) {
        
    }
    ngOnInit(){
        this.isLogin();
    }
    isLogin(){
        var value = localStorage.getItem('Bearer')
          if(value!=null){ 
            this.router.navigate(['/chats'])
          }else{ 
            this.router.navigate(['/login'])
          }
      }
    logout() {
        this.router.navigate(['/login']);
    }
}
