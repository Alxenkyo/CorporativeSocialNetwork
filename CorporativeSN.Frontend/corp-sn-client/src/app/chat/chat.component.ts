import { Component, OnInit, ChangeDetectorRef, Inject } from '@angular/core';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';
import { MessageModel } from 'src/model/message';
import { ChatService } from 'src/services/chat.service';
import { MessageService } from 'src/services/message.service';
import { DatePipe } from '@angular/common';
import { AppComponent } from '../app.component';
import { UserService } from 'src/services/user.service';
import { Chat } from 'src/model/chat';
import { AttachmentDTO } from 'src/model/attachment';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.scss'],
})
export class ChatComponent implements OnInit {

  static addedMessage: any;
  get addedMessage(){
    return ChatComponent.addedMessage
  }
  attach: any = null;
  showAttach: boolean=false;
  chats: any[] = []
  newChat: Chat = new Chat();
  editChat: Chat = new Chat();
  allUsers: any[] = []
  usersToAdd: number[] = []
  currentChat: any;
  userId: number = 0;
  selectedChatId: number = 0;
  editingChat: boolean = false;
  message: MessageModel = new MessageModel();
  chatIsOpen: boolean = false;
  creatingChat: boolean = false;
  constructor(private router: Router, public _chatServise: ChatService, private _msgService: MessageService,
    private datepipe: DatePipe, private ref: ChangeDetectorRef,
    private _userService: UserService, private route: ActivatedRoute, public dialog: MatDialog) { }

  ngOnInit(): void {
    //this.subscribeToEvents();
    var promise = this._userService.GetProfileData();
    promise.then(value=>{
      this.userId=value.id
    })
    this.GetUserChats();
    this.addListener();
    this.addListenerNewChat();
    var promise = this._userService.GetProfileData();
    promise.then(value=>{
      this.userId=value.id
    })
    this.route.paramMap.subscribe((params: ParamMap) => {
      //console.log(JSON.parse(params.get('chats')!))
      //this.GetUserChats();
      if(params.get('chats')!=null || params.get('chats')!=undefined){
      this.chats=JSON.parse(params.get('chats')!);
      this.userId=parseInt(params.get('userId')!)
      this.chats.forEach(item=>{if(item.chatType=="personal"){
        item.name = item.members.find((c: { userId: number; })=>c.userId!=this.userId).userName
      }
    return item}
   
      )
      this.router.navigateByUrl('/chats')
      this.OpenChat(this.chats.find(c=>c.id==params.get('chatId')))
    }
      })
    
  }

  public onFileSelected(event: Event) {
    const target = event.target as HTMLInputElement;
    const files = target.files as FileList;
    this.attach=files[0];
    this.showAttach=true;
      var att = new AttachmentDTO;
      att.name=this.attach.name;
      this.ReadFile(this.attach).then(data=>{
        att.binaryData=data;
        att.binaryData=att.binaryData.split(",").pop();
      })
      this.message.messagesAttachments.push(att)
    }

  GetUserChats(){
    this._chatServise.GetUserChats().subscribe(data=>{
      this.chats=data.items;
    this.chats.forEach(item=>{if(item.chatType=="personal"){
      item.name = item.members.find((c: { userId: number; })=>c.userId!=this.userId).userName
    }
  return item}
 
    )});
  }

  deleteAttach(){
    this.showAttach=false;
    this.attach=null;
    this.message.messagesAttachments=[];
  }

  OpenChat(chat: any){
    this.currentChat=chat;
    this.chatIsOpen=true;
    this.currentChat.messages.reverse()
  }

  TransformDate(date: any){
    return this.datepipe.transform(date, "MMM d, hh:mm a")
     
  }

  CloseChat(){
    this.chatIsOpen=false;
    this.currentChat.messages.reverse()
    this.currentChat=null;
    
  }

    ReadFile(file: any): Promise<any>{
    return new Promise((resolve, reject)=>{
      const reader = new FileReader();
      reader.readAsDataURL(file);
      reader.onload = () => {
        resolve(reader.result);
    };
    reader.onerror = error => reject(error)
    })    
  }

  SendMessage(){
    this.message.chatId=this.currentChat.id
    
    /*const reader = new FileReader();
      reader.readAsDataURL(this.attach);
      reader.onload = () => {
        att.binaryData=reader.result
        att.binaryData=att.binaryData.split(",").pop();
        console.log(att.binaryData)
    };*/
    console.log(this.message)
    var promise = this._msgService.SendMessage(this.message)
    promise.then(value=>{
      console.log("message sended")
      //this.currentChat.messages.push(ChatComponent.addedMessage)
      //console.log(ChatComponent.addedMessage)
      this.message.text=""
      this.deleteAttach();
    },
    error=>{
      alert("Message can't sended")
    }
    )
  }

  Attach(){

  }
  public addListener = () => {
    AppComponent.hubConnection.on('Notify', (data) => {
      console.log(data);
      
      if(this.currentChat != null && this.currentChat.id==data.chatId){
        this.currentChat.messages.unshift(data)
      }
      else{this.chats.find(c=>c.id==data.chatId).messages.push(data)
        this.chats.forEach(item=>{if(item.chatType=="personal"){
          item.name = item.members.find((c: { userId: number; })=>c.userId!=this.userId).userName
        }})}
      this.ref.detectChanges();
      
    });
  }
  
  public addListenerNewChat = () => {
    AppComponent.hubConnection.on('NotifyNewChat', (data) => {
      this.GetUserChats;
      this.chats.push(data);this.chats.forEach(item=>{if(item.chatType=="personal"){
        item.name = item.members.find((c: { userId: number; })=>c.userId!=this.userId).userName
      }})
      this.ref.detectChanges();
    });
  }

OpenCreating(){
this.creatingChat=true;
this._userService.GetProfiles().subscribe(data=>{
  this.allUsers = data.items
  const index = this.allUsers.findIndex(c=>c.id==this.userId);
  if (index > -1) {
    this.allUsers.splice(index, 1);
    }
})
  this.usersToAdd.push(this.userId)
}

onCheckChange(id: number, isChecked: boolean){
  if(isChecked==true){
    this.usersToAdd.push(id)
  }
else{
  const index = this.usersToAdd.indexOf(id, 0);
  if (index > -1) {
    this.usersToAdd.splice(index, 1);
    }
  }
}

CreateChat(){
  this.newChat.creatorId=this.userId;
  this.newChat.membersId=this.usersToAdd;
  var promise = this._chatServise.CreateGroupChat(this.newChat);
  promise.then(value=>{
    if(value){
      alert("Chat was created succesfully")
    this.CloseCreating();
    }
  }
    )
}

CloseCreating(){
  this.creatingChat=false;
  this.allUsers=[];
}

CloseEditing(){
  this.editingChat=false;
  this.editChat=new Chat();
}
EditChat(){
 this.editingChat=true;
 this.editChat=this.currentChat;
}

UpdateChat(){
  this._chatServise.UpdateChat(this.editChat).then((value)=>{this.currentChat=value})
  this.CloseEditing();
}

DeleteChat(){
  const dialogRef = this.dialog.open(DeleteDialog, {
    width: '200px'
  });

  dialogRef.afterClosed().subscribe(result => {
    if(result){this._chatServise.DeleteChat(this.currentChat).then((value)=>{console.log("deleted");
    this.GetUserChats();})
    this.currentChat=new Chat();
    this.chatIsOpen=false;}
  });
}

Download(file: any){
  let byteChar = atob(file.binaryData);
    let byteArray = new Array(byteChar.length);
    for(let i = 0; i < byteChar.length; i++){
      byteArray[i] = byteChar.charCodeAt(i);
    }
    let uIntArray = new Uint8Array(byteArray);
  var blob = new Blob([uIntArray], {type: "application/octet-stream"} )
  var downloadURL = window.URL.createObjectURL(blob);
  var link = document.createElement('a');
  link.href = downloadURL;
  link.download = file.name;
  link.click();
}

}
@Component({
  selector: 'delete-dialog',
  templateUrl: 'delete-dialog.html',
})
export class DeleteDialog {

  constructor(
    public dialogRef: MatDialogRef<DeleteDialog>,
    @Inject(MAT_DIALOG_DATA) public data: any) {}

}