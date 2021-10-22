import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Profile } from 'src/model/profile';
import { User } from 'src/model/user';
import { UserService } from 'src/services/user.service';
import { AppComponent } from '../app.component';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {
  profile: any
  profileToUpload: any;
  constructor(private router: Router, private _userService: UserService) { }

  ngOnInit(): void {
    this.GetProfile();
  }

  GetProfile(){
    var promise = this._userService.GetProfileData();
    promise.then(value=>{
      this.profile=value;
      this.profileToUpload=this.profile
    })
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

  public onFileSelected(event: Event) {
    const target = event.target as HTMLInputElement;
    const files = target.files as FileList;
    this.ReadFile(files[0]).then(data=>{
        this.profileToUpload.imageData=data;
        //this.profileToUpload.imageData=this.profileToUpload.imageData.split(",").pop();
        this.UploadImage();
      })
      
    }

  UploadImage(){   
    var promise = this._userService.UpdateUser(this.profileToUpload);
    promise.then(value=>{this.profile.imageData=value.imageData;
      AppComponent.profile.imageData=value.imageData})    
  }
  DeleteImage(){  
    this.profileToUpload.imageData=null; 
    var promise = this._userService.UpdateUser(this.profileToUpload);
    promise.then(value=>{this.profile.imageData=value.imageData;
    AppComponent.profile.imageData=value.imageData})    
  }
}
