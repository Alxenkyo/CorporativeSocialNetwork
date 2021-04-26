import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  password: string = "";
  username: string = "";
  constructor(private router: Router, private _authService: AuthService) { }

  ngOnInit() {
    
  }
onLogin(){var promise = this._authService.Login(this.username,this.password);
    if(promise)
    {
      console.log(localStorage.getItem("Bearer"));
      //console.log(localStorage.getItem("UserType"));
      this.router.navigateByUrl("chats")
    }
  }
}
