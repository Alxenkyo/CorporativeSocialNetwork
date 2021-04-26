import { Injectable } from '@angular/core';
import {  HttpClient, HttpHeaders, HttpParams  } from '@angular/common/http';
import { Router } from '@angular/router'; 
import { environment } from 'src/environments/environment';
import { map } from 'rxjs/operators';
@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http: HttpClient,private router: Router) { }
  Login(userName: string, password: string)
  {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type':  'application/json',
      }),
      withCredentionals: true,
      
    };
    
    return this.http.post<any>(`${environment.apiUrl}/auth`, { userName, password })
            .pipe(map(token => {
                localStorage.setItem('Bearer', JSON.stringify(token));
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
