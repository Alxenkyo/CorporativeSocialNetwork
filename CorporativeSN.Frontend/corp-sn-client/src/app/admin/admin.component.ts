import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { userModel } from 'src/model/userModel';
import { DepartmentService } from 'src/services/department.service';
import { UserService } from 'src/services/user.service';
import {User} from '../../model/user'
import { AppComponent } from '../app.component';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.scss']
})
export class AdminComponent implements OnInit {

  constructor(private router: Router, private _userService: UserService, private _depService: DepartmentService) { }
  createUser: userModel = new userModel();
  editUser: userModel = new userModel();
  creatingUser: boolean = false;
  editingUser: boolean = false;
  users: any[] = [];
  departments: any[] = [];
  ngOnInit(): void {
    this.GetUsers();
    this.GetDepartments();
  }
  GetUsers()
  {this._userService.GetUsers().subscribe(data=>{
    console.log(data)
    this.users=data.items});
  }
  GetDepartments(){
    var promise = this._depService.GetDepartments()
      promise.then(data=>{this.departments=data.items})
    }

  OpenCreating(){
    this.creatingUser=true;
  }
  CloseCreating(){
    this.creatingUser=false;
    this.createUser=new userModel();
  }

  colDefs = [
    {  headerName: 'Id', field: 'id', sortable: true },
    {  headerName: 'Имя', field: 'firstName', sortable: true, filter: true },
    {  headerName: 'Фамилия', field: 'lastName', sortable: true, filter: true },
    {  headerName: 'Отдел', field: 'departmentName', sortable: true, filter: true },
    {  headerName: 'Роль', field: 'typeName', sortable: true, filter: true }
];

  OpenEditing(user: any){
    this.editingUser=true;
    this.editUser=user;
  }
  CloseEditing(){
    this.editingUser=false;
    this.editUser=new userModel();
  }

  CreateUser(){
    this.createUser.departmentId=Number(this.createUser.departmentId)
    var promise = this._userService.CreateUser(this.createUser)
      promise.then(data=>{
        if(data)
        alert("User created")
        this.CloseCreating();
        this.GetUsers();
      })
      this.router.navigateByUrl('admin')
    }

  EditUser(){
    this.editUser.departmentId=Number(this.editUser.departmentId)
    var promise = this._userService.UpdateUser(this.editUser)
    promise.then(data=>{
      if(data)
      alert("User updated")
      this.CloseEditing();
    })
    this.GetUsers();
    this.router.navigateByUrl('admin')
  }

  DeleteUser(id: number){
    var promise = this._userService.DeleteUser(id)
    promise.then(response=>{
      if(response){
        alert("User deleted")
        this.GetUsers();
      }
    })
    this.router.navigateByUrl('admin')
  }
}
