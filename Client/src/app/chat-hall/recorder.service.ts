// recorder.service.ts
import { Injectable } from '@angular/core';
import { AccountService } from '../account/account.service';

@Injectable({
  providedIn: 'root',
})
export class RecorderService {
  private mediaRecorder!: MediaRecorder;
  private audioChunks: Blob[] = [];

  constructor(private accountService:AccountService) {}
  
  startRecording() {
    navigator.mediaDevices.getUserMedia({ audio: true })
      .then((stream) => {
        this.mediaRecorder = new MediaRecorder(stream);
        this.mediaRecorder.ondataavailable = (event) => {
          if (event.data.size > 0) {
            this.audioChunks.push(event.data);
          }
        };
        this.mediaRecorder.onstop = async () => {
          const audioBlob = new Blob(this.audioChunks, { type: 'audio/wav' });

          await this.accountService.sendAudio(audioBlob);
        };

        this.mediaRecorder.start();
      })
      .catch((error) => {
        console.error('Error accessing microphone:', error);
      });
  }

  stopRecording() {
    if (this.mediaRecorder.state !== 'inactive') {
      this.mediaRecorder.stop();
    }
  }

}
