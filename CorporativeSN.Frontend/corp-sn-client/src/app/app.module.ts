import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { ChatComponent, DeleteDialog } from './chat/chat.component';
import { AdminComponent } from './admin/admin.component';
import {FormsModule} from '@angular/forms';
import {HttpClientModule} from '@angular/common/http';
import { ProfileComponent } from './profile/profile.component'
import { AgGridModule } from 'ag-grid-angular';
import { DocsStorageComponent } from './docs-storage/docs-storage.component';
import { DatePipe } from '@angular/common';
import { UsersComponent } from './users/users.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {MatTabsModule} from '@angular/material/tabs';
import {MatDialogModule} from '@angular/material/dialog';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    ChatComponent,
    AdminComponent,
    ProfileComponent,
    DocsStorageComponent,
    UsersComponent,
    DeleteDialog
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    AgGridModule,
    BrowserAnimationsModule,
    MatTabsModule,
    MatDialogModule
  ],
  providers: [DatePipe],
  bootstrap: [AppComponent]
})
export class AppModule { }
