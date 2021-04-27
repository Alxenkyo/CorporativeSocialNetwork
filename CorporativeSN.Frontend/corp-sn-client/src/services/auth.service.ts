import { Injectable } from '@angular/core';
import {  HttpClient, HttpHeaders, HttpParams  } from '@angular/common/http';
import { Router } from '@angular/router'; 
import { environment } from 'src/environments/environment';
import { map } from 'rxjs/operators';
import {AuthOptions} from '../model/authOptions'
@Injectable({
  providedIn: 'root'
})
export class AuthService {
  authData: AuthOptions = new AuthOptions;
  constructor(private http: HttpClient,private router: Router) { }
  Login(userName: string, password: string)
  {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type':  'application/json',
        'Access-Control-Allow-Origin' : '*'
      }),
      withCredentionals: true,
      
    };
    
    return this.http.post<any>(environment.apiUrl+'/login/token/',{userName, password}, httpOptions )
            .pipe(map(token => {
               this.authData = token;
               localStorage.setItem('Bearer', JSON.stringify(this.authData.access_token));
               localStorage.setItem('UserRole', JSON.stringify(this.authData.username));
               console.log(localStorage.getItem('Bearer'));
                console.log(localStorage.getItem('UserRole'));
                return this.authData;
            }));
}
  Logout(){
      localStorage.removeItem('Bearer'); 
      localStorage.removeItem('UserType');
  }
  IsUserLogin(){
   
    var key = localStorage.getItem('Bearer'); 
    if(key!=null){
      return true
    }
    else return false
}
}
