import { Injectable } from '@angular/core';
import {  HttpClient, HttpHeaders, HttpParams  } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class DepartmentService {

  constructor(private http: HttpClient) {
   }

   GetDepartments(): Promise<any>{
    var promise = new Promise((resolve, reject)=>{
      this.http.get<any>(environment.apiUrl+'/department',{headers: {'Accept': 'application/json', 'Authorization' : 'Bearer ' +
      localStorage.getItem('Bearer')} } ).subscribe(
        (response)=>{
          resolve(response)
        },
        (error)=>{
          alert("Can't get departments")
        }
      )
    })
    return promise
   }
}
