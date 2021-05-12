import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from 'src/services/user.service';
import {User} from '../../model/user'

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.scss']
})
export class AdminComponent implements OnInit {

  constructor(private router: Router, private _userService: UserService) { }

  users: User[] = [];
  columnDefs = [
    { field: 'Id' },
    { field: 'Name' },
    { field: 'UserTypeId'},
    { field: 'DepartmentId'}
];
  ngOnInit(): void {
    this.GetUsers();
  }
  GetUsers()
  {this._userService.GetUsers().subscribe(data=>this.users=data);
    
  }
}
