import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ChatService } from 'src/services/chat.service';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.scss']
})
export class ChatComponent implements OnInit {

  chats: any[] = []
  constructor(private router: Router, private _chatServise: ChatService) { }

  ngOnInit(): void {
    this.GetUserChats();
  }

  GetUserChats(){
    this._chatServise.GetUserChats().subscribe(data=>{
      console.log(data)
      this.chats=data
    })
  }
}
