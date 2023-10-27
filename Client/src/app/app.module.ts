import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavBarComponent } from './core/nav-bar/nav-bar.component';
import { AccountModule } from './account/account.module';
import { HomeComponent } from './home/home/home.component';
import { FooterComponent } from './core/footer/footer/footer.component';
import { HttpClientModule } from '@angular/common/http';
import { ChatComponent } from './chat-hall/chat/chat.component';
import { ChatInputComponent } from './chat-hall/chat-input/chat-input.component';
import { FormsModule } from '@angular/forms';
import { MessagesComponent } from './chat-hall/messages/messages.component';
import { PrivateChatComponent } from './chat-hall/private-chat/private-chat.component';
import { DashBoardComponent } from './core/dash-board/dash-board.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatIconModule } from '@angular/material/icon';
import { UsersDataComponent } from './core/dash-board/users-data/users-data.component';
import { AboutUsComponent } from './core/dash-board/about-us/about-us.component';
import { AdminComponent } from './core/dash-board/admin/admin.component';
@NgModule({
  declarations: [
    AppComponent,
    NavBarComponent,
    HomeComponent,
    FooterComponent,
    ChatComponent,
    ChatInputComponent,
    MessagesComponent,
    PrivateChatComponent,
    DashBoardComponent,
    UsersDataComponent,
    AboutUsComponent,
    AdminComponent
  ],
  imports: [
    AccountModule,
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    BrowserAnimationsModule,
    MatIconModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
