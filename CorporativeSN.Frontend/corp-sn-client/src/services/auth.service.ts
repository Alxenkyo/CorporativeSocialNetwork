import { Injectable } from '@angular/core';
import {  HttpClient, HttpHeaders, HttpParams  } from '@angular/common/http';
import { Router } from '@angular/router'; 
import { environment } from 'src/environments/environment';
import { map } from 'rxjs/operators';
import {AuthOptions} from '../model/authOptions'
import {Observable} from 'rxjs';
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
    return this.http.post(environment.apiUrl+'/login/token?username='+userName+'&password='+password,{} /*httpOptions*/ )
            .pipe(map(data => {
              console.log(data);
               //this.authData = data;
               //localStorage.setItem('Bearer', JSON.stringify(data.access_token));
               //localStorage.setItem('UserRole', JSON.stringify(data.username));
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
