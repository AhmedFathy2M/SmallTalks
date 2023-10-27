import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { AccountService } from 'src/app/account/account.service';
import { interval, Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

@Component({
  selector: 'app-private-chat',
  templateUrl: './private-chat.component.html',
  styleUrls: ['./private-chat.component.scss']
})
export class PrivateChatComponent implements OnInit, OnDestroy {
  @Input() toUser = '';
  private lastMessageSent = 0;
  private destroy$: Subject<void> = new Subject();
  private noResponseThreshold = 60000;
  private inactivityWarningThreshold = this.noResponseThreshold / 2;
  private inactivityWarningShown = false;
  private currentToUser = ''; // Track the current toUser name
  private inactivityTimer: any; // Store the inactivity timer
  public chatWarningMessage = true;

  constructor(public activeModal: NgbActiveModal, public accountService: AccountService) {}

  ngOnDestroy(): void {
    this.accountService.closePrivateChatMessage(this.toUser);
    this.destroy$.next();
    this.destroy$.complete();
  }

  ngOnInit(): void {
    interval(1000)
      .pipe(takeUntil(this.destroy$))
      .subscribe(() => {
        const currentTime = Date.now();
        if (this.lastMessageSent !== 0 && currentTime - this.lastMessageSent >= this.noResponseThreshold) {
          this.activeModal.dismiss('Chat closed due to no response');
        } else if (this.lastMessageSent !== 0 && currentTime - this.lastMessageSent >= this.inactivityWarningThreshold) {
          if (!this.inactivityWarningShown) {
            this.showInactivityWarningPopup();
            this.inactivityWarningShown = true;
          }
        }
      });
  }

  sendMessage(content: any) {
    this.lastMessageSent = Date.now();

    // Check if the toUser name has changed
    if (this.toUser !== this.currentToUser) {
      this.currentToUser = this.toUser; // Update the current toUser
      this.inactivityWarningShown = false; // Reset the inactivity warning
    }

    // Clear and restart the inactivity timer
    clearTimeout(this.inactivityTimer);
    this.inactivityTimer = setTimeout(() => {
      // Inactivity timeout for the current user
      this.activeModal.dismiss('Chat closed due to no response');
    }, this.noResponseThreshold);

    this.accountService.sendPrivateMessage(this.toUser, content);
    this.chatWarningMessage = true;
  }

  showInactivityWarningPopup() {
    this.chatWarningMessage = false;
  }





  
}
