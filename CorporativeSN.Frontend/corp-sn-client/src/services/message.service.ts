import { Injectable } from '@angular/core';
import {  HttpClient, HttpHeaders, HttpParams  } from '@angular/common/http';
import { Router } from '@angular/router'; 
import { environment } from 'src/environments/environment';
import * as CryptoJS from 'crypto-js';

@Injectable({
  providedIn: 'root'
})
export class MessageService {

  constructor(private http: HttpClient,private router: Router) { }

  SendMessage(message: any): Promise<any>{
    //message.text = CryptoJS.AES.encrypt(message.text, )
    //const formData = new FormData();
    //formData.append('message', message);
    var promise = new Promise((resolve, reject)=> 
    this.http.post(environment.apiUrl+'/message',message,{headers: {'Accept': 'application/json', 'Authorization' : 'Bearer ' +
    localStorage.getItem('Bearer')} }).subscribe(
      (response)=>{
      resolve(true)},
      (error)=>{
        alert("Can't send message")
        reject("Can't send message")
      }
    )
    )
      return promise
  }

  
}
