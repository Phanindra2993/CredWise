import { Component, EventEmitter, Output } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-success-modal',
  standalone: true,
  imports: [CommonModule],
  template: `
    <div class="modal-overlay" (click)="onClose()">
      <div class="modal-content" (click)="$event.stopPropagation()">
        <div class="success-icon">✓</div>
        <h2>Application Submitted Successfully!</h2>
        <p>Thank you for choosing CredWise. Our team will review your application and get back to you within 24-48 hours.</p>
        <!-- <p>Application Reference: {{applicationId}}</p> -->
        <button class="ok-button" (click)="onOk()">OK</button>
      </div>
    </div>
  `,
  styles: [`
    .modal-overlay {
      position: fixed;
      top: 0;
      left: 0;
      width: 100%;
      height: 100%;
      background: rgba(0, 0, 0, 0.5);
      display: flex;
      justify-content: center;
      align-items: center;
      z-index: 1000;
    }

    .modal-content {
      background: white;
      padding: 2rem;
      border-radius: 8px;
      text-align: center;
      max-width: 500px;
      width: 90%;
      box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
    }

    .success-icon {
      width: 60px;
      height: 60px;
      background: #4CAF50;
      color: white;
      border-radius: 50%;
      display: flex;
      align-items: center;
      justify-content: center;
      font-size: 30px;
      margin: 0 auto 1rem;
    }

    h2 {
      color: #333;
      margin-bottom: 1rem;
    }

    p {
      color: #666;
      margin-bottom: 1rem;
      line-height: 1.5;
    }

    .ok-button {
      background: #1890ff;
      color: white;
      border: none;
      padding: 0.75rem 2rem;
      border-radius: 4px;
      font-size: 1rem;
      cursor: pointer;
      transition: background 0.3s;
    }

    .ok-button:hover {
      background: #40a9ff;
    }
  `]
})
export class SuccessModalComponent {
  @Output() close = new EventEmitter<void>();
  @Output() ok = new EventEmitter<void>();
  // applicationId: string = Math.random().toString(36).substring(2, 10).toUpperCase();

  onClose() {
    this.close.emit();
  }

  onOk() {
    this.ok.emit();
  }
} 