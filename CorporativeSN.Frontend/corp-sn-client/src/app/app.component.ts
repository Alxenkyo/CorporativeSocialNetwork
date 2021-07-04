import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import * as signalR from '@aspnet/signalr';
import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';
import { AuthService } from 'src/services/auth.service';
import { UserService } from 'src/services/user.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'corp-sn-client';
  static isAdmin: boolean = false;
  static isLogged: boolean = false;
  static hubConnection: signalR.HubConnection;
  static userId: number;
  static profile: any;
  get profile(){
    return AppComponent.profile;
  }
  get userId(){
    return AppComponent.userId;
  }
  static connectionId: string = "";
  get connectionId(){
    return AppComponent.connectionId;
  }
  get isLogged(){
    return AppComponent.isLogged;
}
get isAdmin(){
  return AppComponent.isAdmin;
}
get hubConnection(){
  return AppComponent.hubConnection;
}
  constructor(private router: Router, private _authService: AuthService, private _userService: UserService) {}
  ngOnInit(){
    AppComponent.Connect();
    this.isLogin();
  }
isLogin(){
  var promise = this._authService.IsUserLogin(this.connectionId); 
      promise.then(value => { 
          var role = localStorage.getItem('UserRole') ?? "unknown";
          if(role == "admin"){
          AppComponent.isAdmin=true;}
          AppComponent.isLogged=true;
          this._userService.GetProfileData().then(value=>{
            AppComponent.profile=value})
          this.router.navigateByUrl("/chats")
          
        },
         error=>{
          this.router.navigate(['/login']);
          AppComponent.isLogged=false;
         }
      )
}

static Connect(){
  this.startConnection();
  
}

  static startConnection = () => {
  AppComponent.hubConnection = new signalR.HubConnectionBuilder()
                          .withUrl('http://localhost:34502/chart')
                          .build();
  AppComponent.hubConnection
    .start()
    .then(() => console.log('Connection started'))
    .then(()=> AppComponent.hubConnection.invoke("getConnectionId").then(data=>{AppComponent.connectionId=data}))
    .catch(err => console.log('Error while starting connection: ' + err))
}
 
Logout() {
  this.router.navigate(['/login']);
  AppComponent.isLogged=false;
  localStorage.removeItem('Bearer');
  AppComponent.profile=null;
}

Profile(){
  this.router.navigateByUrl("/profile")
};

Chats(){
  this.router.navigateByUrl("/chats")
};

Calendar(){
  this.router.navigateByUrl("/calendar")
};

AdminPage(){
  this.router.navigateByUrl("/admin")
}
UsersPage(){
  this.router.navigateByUrl("users")
}
Documents(){
  this.router.navigateByUrl('docs')
}

NavigateToHome(){
  this.router.navigateByUrl('/chats')
}

NavigateToProfile(){
  this.router.navigateByUrl('/profile')
}
}
