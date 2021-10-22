import { EventEmitter, Injectable } from '@angular/core';
import {  HttpClient, HttpHeaders, HttpParams  } from '@angular/common/http';
import { Router } from '@angular/router'; 
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import * as signalR from "@aspnet/signalr";
import { ChatComponent } from 'src/app/chat/chat.component';
import { Chat } from 'src/model/chat';

@Injectable({
  providedIn: 'root'
})
export class ChatService {
  messageReceived = new EventEmitter();
  private hubConnection!: signalR.HubConnection;
  constructor(private http: HttpClient,private router: Router) { }

  GetUserChats(): Observable<any>{
    return this.http.get<any>(environment.apiUrl + '/chat', {
      headers: {
        'Accept': 'application/json', 'Authorization': 'Bearer ' +
          localStorage.getItem('Bearer')
      }
    });

  }

  AddUserToChat(){}

  GetChat(chatId: number): Observable<any>{
    return this.http.get<any>(environment.apiUrl+'/chat/' + chatId,{headers: {'Accept': 'application/json', 'Authorization' : 'Bearer ' +
    localStorage.getItem('Bearer')} })
  }
  
  CreateChat(chat: Chat): Promise<any>{
    var promise = new Promise((resolve, reject)=>{
    this.http.post(environment.apiUrl+'/chat/',chat,{headers: {'Accept': 'application/json', 'Authorization' : 'Bearer ' +
    localStorage.getItem('Bearer')} }).subscribe(
      (response)=>{
        resolve(response)
      },
      (error)=>{
        alert("Can't create chat")
      }

    )}   
    )
    return promise
  }


  CreateGroupChat(chat: Chat): Promise<any>{
    var promise = new Promise((resolve, reject)=>{
    this.http.post(environment.apiUrl+'/chat/',chat,{headers: {'Accept': 'application/json', 'Authorization' : 'Bearer ' +
    localStorage.getItem('Bearer')} }).subscribe(
      (response)=>{
        resolve(response)
      },
      (error)=>{
        alert("Can't create chat")
      }

    )}   
    )
    return promise
  }

  UpdateChat(chat: any){
    var promise = new Promise((resolve, reject)=>{
      this.http.put(environment.apiUrl+'/chat/',chat,{headers: {'Accept': 'application/json', 'Authorization' : 'Bearer ' +
      localStorage.getItem('Bearer')} }).subscribe(
        (response)=>{
          resolve(response)
        },
        (error)=>{
          alert("Can't update chat")
        }
  
      )}   
      )
      return promise
  }
  DeleteChat(chat: any){
    var promise = new Promise((resolve, reject)=>{
      this.http.delete(environment.apiUrl+'/chat/'+ chat.id,{headers: {'Accept': 'application/json', 'Authorization' : 'Bearer ' +
      localStorage.getItem('Bearer')} }).subscribe(
        (response)=>{
          resolve(true)
        },
        (error)=>{
          alert("Can't delete chat")
        }
  
      )}   
      )
      return promise
  }
}
