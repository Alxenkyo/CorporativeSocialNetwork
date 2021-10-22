import { ThrowStmt } from '@angular/compiler';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Chat } from 'src/model/chat';
import { User } from 'src/model/user';
import { ChatService } from 'src/services/chat.service';
import { UserService } from 'src/services/user.service';
@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss']
})
export class UsersComponent implements OnInit {
  userId: any;
  constructor(private router: Router, private _userService: UserService, private _chatService: ChatService) { }
  users: any[] = [];
  chats: any[] = [];
  createChat: Chat = new Chat();
  searchUsers: any[] = [];
  ngOnInit(): void {
    this.GetUsers();
    this.GetChats();
    var promise = this._userService.GetProfileData();
    promise.then(value=>{
      this.userId=value.id
    })
  }
  GetChats(){
    this._chatService.GetUserChats().subscribe(data=>{
      this.chats=data.items});
  }
  GetUsers()
  {this._userService.GetProfiles().subscribe(data=>{
    console.log(data)
    this.users=data.items
    this.AddToSearch();});
     
  }

  AddToSearch(){
    this.searchUsers=this.users;  
  }

  FilterValue(value: any){
    if(!value || value==""){
      this.AddToSearch();
    }
    else{
    this.searchUsers = this.users.filter(
      item=>item.firstName.concat(item.lastName).toLowerCase().includes(value.replace(/\s/g, "").toLowerCase()) || 
      item.lastName.concat(item.firstName).toLowerCase().includes(value.replace(/\s/g, "").toLowerCase())
    )
  }}
  
  Write(id: any){
      var chat = this.chats.find(c=>c.chatType=="personal" && c.members.find((x: { userId: any; })=>x.userId==this.userId)!=null
      && c.members.find((x: { userId: any; })=>x.userId==id)!=null)
      if(chat!=null){
        this.router.navigate(['/chats', {userId: this.userId,chatId: chat.id,chats: JSON.stringify(this.chats)}])
      }
      else {
        this.createChat.chatType="personal"
        this.createChat.creatorId=this.userId;
        this.createChat.name="personal";
        this.createChat.membersId.push(this.userId)
        this.createChat.membersId.push(id)
        this._chatService.CreateGroupChat(this.createChat).then(value=>{
          if(value!=null || value!=undefined){
            this.GetChats();
            var user = this.users.find(c=>c.id==id);
            value.members[1].userName=user.firstName + ' ' + user.lastName
            this.chats.push(value);
            this.router.navigate(['/chats', {userId: this.userId, chatId: value.id,chats: JSON.stringify(this.chats)}])}
        })
      }

  }
}
