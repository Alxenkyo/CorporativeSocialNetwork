import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'corp-sn-client';
  isUserLogin: boolean = false;
  constructor(private router: Router) {}
  ngOnInit(){
    this.isLogin();
}
isLogin(){
  var value = localStorage.getItem('Bearer')
    if(value!=null){ 
      this.router.navigate(['/chat']);
      this.isUserLogin = true;
    }else{ 
      this.router.navigate(['/login'])
      this.isUserLogin = false;
    }
}
logout() {
  this.router.navigate(['/login']);
}
}
