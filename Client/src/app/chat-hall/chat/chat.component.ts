import { Component, OnDestroy, OnInit, TemplateRef } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Observable, from } from 'rxjs';
import { AccountService } from 'src/app/account/account.service';
import { PrivateChatComponent } from '../private-chat/private-chat.component';
import { RecorderService } from '../recorder.service';
import { IUser } from 'src/app/shared/models/user';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.scss']
})
export class ChatComponent implements OnInit, OnDestroy {
  userNames: string[] = [];
  onlineUsers: string[] = this.accountService.onlineUsers;
  currentUserName$!: Observable<string | null>;
  currentUser!:IUser|null ;
  startRecordingTimeout: any;
  timer: number = 60;
  isRecording: boolean = false;
  recordingTimeout: any;
  constructor(public accountService: AccountService, private modalService: NgbModal,
    private recorderService: RecorderService) {
  }
  ngOnDestroy(): void {
    this.accountService.stopChatConnection();

  }

  ngOnInit(): void {
    this.accountService.createChatConnection();
    this.getAllNames(); // Call the method when the component is initialized
    this.currentUserName$ = from(this.accountService.getCurrentUserName());
    this.currentUser = this.accountService.getCurrentUserValue();
  }

  getAllNames() {
    this.accountService.getAllUserNames().subscribe(
      (response: string[]) => {
        this.userNames = response;
      },
      (error: any) => {
        console.log(error);
      }
    );
  }
  sendMessage(content: string) {
    this.accountService.sendMessage(content);

  }

  openPrivateChat(toUser: string) {
    console.log("pchat is opened")
    const modalRef = this.modalService.open(PrivateChatComponent, {
      backdrop: 'static'
    });
    modalRef.componentInstance.toUser = toUser;

  }

  startRecording() {
    if (this.currentUser?.email !== "ahmedfathymohamed1998@gmail.com")
    {
      this.recorderService.startRecording();
    this.isRecording = true; // Set recording status
    this.recordingTimeout = setInterval(() => {
      if (this.timer > 0) {
        this.timer--;
      } else {
        this.stopRecording();
      }
    }, 1000); 

    } else
    {
      this.recorderService.startRecording();
    }
    
  }

  stopRecording() {
    if (this.currentUser?.email !== "ahmedfathymohamed1998@gmail.com")
    {
      if (this.recordingTimeout) {
        clearInterval(this.recordingTimeout);
      }
      this.isRecording = false; 
      this.timer = 60; 
      this.recorderService.stopRecording();

    } else
    {
      this.recorderService.stopRecording();
    }
  
  }

  handleImageInput(event: any) {
    const file: Blob = event.target.files[0];
    console.log(file);
    if (file) {
      // Get the content type (MIME type) of the file
      const contentType = file.type;
  
      // Now you can send the image with the correct content type
      this.accountService.sendImage(file, contentType)
        .then(() => console.log('Image sent'))
        .catch((error) => console.error('Error sending image:', error));
    }
  }

  compressImage(file: Blob, quality: number): Promise<Blob> {
    return new Promise((resolve, reject) => {
      const reader = new FileReader();
      reader.readAsDataURL(file);
      reader.onload = (event) => {
        const img = new Image();
        img.src = event.target?.result as string;
        img.onload = () => {
          const canvas = document.createElement('canvas');
          const ctx = canvas.getContext('2d');
  
          if (!ctx) {
            reject(new Error('Could not create canvas context'));
            return;
          }
  
          canvas.width = img.width;
          canvas.height = img.height;
  
          ctx.drawImage(img, 0, 0, img.width, img.height);
  
          canvas.toBlob(
            (blob) => {
              if (blob) {
                resolve(blob);
              } else {
                reject(new Error('Error creating compressed blob'));
              }
            },
            file.type,
            quality
          );
        };
      };
  
      reader.onerror = (error) => reject(error);
    });
  }

}