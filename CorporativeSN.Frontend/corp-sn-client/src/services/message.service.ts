import { Injectable } from '@angular/core';
import {  HttpClient, HttpHeaders, HttpParams  } from '@angular/common/http';
import { Router } from '@angular/router'; 
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class MessageService {

  constructor(private http: HttpClient,private router: Router) { }

  SendMessage(){}

  GetChatMessages(){}

  
}
