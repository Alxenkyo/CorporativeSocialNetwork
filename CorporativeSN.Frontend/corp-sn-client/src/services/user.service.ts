import { Injectable } from '@angular/core';
import {  HttpClient, HttpHeaders, HttpParams  } from '@angular/common/http';
import { Router } from '@angular/router'; 
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { User } from 'src/model/user';
import { map } from 'rxjs/operators';
import { userModel } from 'src/model/userModel';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient,private router: Router) { }

  GetUsers(): Observable<any>
  {
    return this.http.get<any>(environment.apiUrl+'/user',{headers: {'Accept': 'application/json', 'Authorization' : 'Bearer ' +
    localStorage.getItem('Bearer')} })
  }

  GetProfiles(): Observable<any>
  {
    return this.http.get<any>(environment.apiUrl+'/user/profiles',{headers: {'Accept': 'application/json', 'Authorization' : 'Bearer ' +
    localStorage.getItem('Bearer')} })
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

  DeleteUser(id: number): Promise<any>{
    var promise = new Promise((resolve, reject)=>{
        this.http.delete<any>(environment.apiUrl+'/user/'+id,{headers: {'Accept': 'application/json', 'Authorization' : 'Bearer ' +
        localStorage.getItem('Bearer')}}).subscribe(
          (response)=>{
            resolve(true);
          },
          (error)=>{
            alert("Can't delete user")
          }
        )
    })
    return promise
  }

  CreateUser(user: any): Promise<any>{
    var promise = new Promise((resolve, reject)=>{
      this.http.post<any>(environment.apiUrl+'/user',user,{headers: {'Accept': 'application/json', 'Authorization' : 'Bearer ' +
      localStorage.getItem('Bearer')}}).subscribe(
        (response)=>{
          resolve(true);
        },
        (error)=>{
          alert("Can't create user")
        }
      )
    })
    return promise
  }

  UpdateUser(user: any): Promise<any>{
    if(user.imageData==""){user.imageData=null}
    if(user.imageData!=null){user.imageData=user.imageData.split(",").pop();}
    var promise = new Promise((resolve, reject)=>{
      this.http.put<any>(environment.apiUrl+'/user', user,{headers: {'Accept': 'application/json', 'Authorization' : 'Bearer ' +
      localStorage.getItem('Bearer')}}).subscribe(
        (response)=>{
          resolve(response);
        },
        (error)=>{
          alert("Can't update user")
        }
      )
    })
  return promise
  }
}
