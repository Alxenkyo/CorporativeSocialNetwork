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
    /*const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type':  'application/json',
        'Access-Control-Allow-Origin' : '*'
      }),
      withCredentionals: true,
      
    };*/
    var promise = new Promise((resolve, reject) => {
     this.http.post<any>(environment.apiUrl+'/login/token?username='+userName+'&password='+password,{} /*httpOptions*/ ).subscribe(
              (response) => {
              console.log(JSON.stringify(response.body));
               localStorage.setItem('Bearer', JSON.stringify(response.body));
               //localStorage.setItem('UserRole', JSON.stringify(data.username));
               resolve(true);},
              (error)=>{
                alert("Invalid username or password");
              });
    });
              return promise;
}
  Logout(){
      localStorage.removeItem('Bearer'); 
      localStorage.removeItem('UserType');
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
}