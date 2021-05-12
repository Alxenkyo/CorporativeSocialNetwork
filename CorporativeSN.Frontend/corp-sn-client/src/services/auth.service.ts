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
  Login(userName: string, password: string) : Promise<any>
  {
    var promise = new Promise((resolve, reject) => {
     this.http.post<any>(environment.apiUrl+'/login/token?username='+userName+'&password='+password,{},{observe: 'response'}).subscribe(
              (response) => {
                var res = response.body.access_token
                //console.log(res);
                var role = response.headers.get('X-User-Type');
                localStorage.setItem('Bearer', res);
                localStorage.setItem('UserRole', role ?? "kekw");
               resolve(true);},
              (error)=>{
                alert("Invalid username or password");
              });
    });
              return promise;
}

  IsUserLogin(): Promise<any>
  { 
    var promise = new Promise((resolve, reject) => {
      var key = localStorage.getItem('Bearer'); 
      const  headers = new  HttpHeaders().set('Authorization', 'Bearer ' + key);
      this.http.get(environment.apiUrl + "/check/", {headers}).subscribe(resp => { 
        resolve(resp);
      },
      (error: Response)=>{
        if(error.status == 401 || 405){  
          this.router.navigateByUrl('/login');
          reject("Not logged");
        }
      });
    });
    return promise;
}

Logout(){
  localStorage.removeItem('Bearer'); 
  localStorage.removeItem('UserType');
}

}