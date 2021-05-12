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

  GetUsers(): Observable<User[]>
  {
    return this.http.get<any>(environment.apiUrl+'/user',{headers: {'Accept': 'application/json', 'Authorization' : 'Bearer ' +
    localStorage.getItem('Bearer')} }).pipe(map(data=>{
      let userlist = data;
      return userlist.map(function(user:any){
        return {id: user.id, name: user.name, usertypeid: user.usertypeid, depId: user.departmentid}
      })
    }))
  }
}
