import { Injectable } from '@angular/core';
import {  HttpClient, HttpHeaders, HttpParams  } from '@angular/common/http';
import { Router } from '@angular/router'; 
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { User } from 'src/model/user';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient,private router: Router) { }

  GetUsers(): Observable<any>
  {
    return this.http.get<any>(environment.apiUrl+'/user',{headers: {'Accept': 'application/json', 'Authorization' : 'Bearer ' +
    localStorage.getItem('Bearer')} }).pipe(map(data=>{
      console.log(data.items)
      let userlist = data.items;
      return userlist.map(function(user:any){
        return {id: user.id, name: user.name, usertypeId: user.userTypeId, depId: user.departmentId}
      })
    }))    
  }

  GetProfileData(): Promise<any>{
    var promise = new Promise((resolve, reject)=>{
      this.http.get<any>(environment.apiUrl+'/user/profile',{headers: {'Accept': 'application/json', 'Authorization' : 'Bearer ' +
    localStorage.getItem('Bearer')} } ).subscribe(
      (response)=>{
        console.log(response)
        resolve(response);
      },
      (error)=>{
        alert("Can't get profile data");
      }
    )
    })
     return promise;
  }

  DeleteUser(){}

  CreateUser(){}

  GetUserById(){}
}
