import { Injectable } from '@angular/core';
import {  HttpClient, HttpHeaders, HttpParams  } from '@angular/common/http';
import { Router } from '@angular/router'; 
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ChatService {

  constructor(private http: HttpClient,private router: Router) { }

  GetUserChats(): Observable<any>{
    return this.http.get<any>(environment.apiUrl+'/chat',{headers: {'Accept': 'application/json', 'Authorization' : 'Bearer ' +
    localStorage.getItem('Bearer')} })


  }

  AddUserToChat(){}


  
}
