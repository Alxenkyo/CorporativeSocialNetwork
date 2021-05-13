import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Profile } from 'src/model/profile';
import { User } from 'src/model/user';
import { UserService } from 'src/services/user.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {
  profile: Profile = new Profile();
  constructor(private router: Router, private _userService: UserService) { }

  ngOnInit(): void {
    this.GetProfile();
  }

  GetProfile(){
    var promise = this._userService.GetProfileData();
    promise.then(value=>{
      this.profile.id = value.id;
      this.profile.name = value.name;
      this.profile.usertypeId = value.userTypeId;
      this.profile.depId = value.departmentId
    })
  }
  
  Profile(){};
  Chats(){};
  Calendar(){};
  Logout(){};
}
