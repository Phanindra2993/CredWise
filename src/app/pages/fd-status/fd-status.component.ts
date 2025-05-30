import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FdService } from '../../../_services/fd.service';
import { FDStatus } from '../../../_models/fd.model';
import { AuthService } from '../../../_services/auth.service';

@Component({
  selector: 'app-fd-status',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './fd-status.component.html',
  styleUrls: ['./fd-status.component.scss']
})
export class FdStatusComponent implements OnInit {
  fdApplications: FDStatus[] = [];
  error: string = '';

  constructor(
    private fdService: FdService,
    private authService: AuthService
  ) {}

  ngOnInit() {
    this.loadFDStatus();
  }

  loadFDStatus() {
    const currentUser = this.authService.currentUserValue;
    if (!currentUser) {
      this.error = 'Please login to view your FD applications.';
      return;
    }

    this.fdService.getFdStatusByUserId(currentUser.userId).subscribe({
      next: (data: FDStatus[]) => {
        this.fdApplications = data;
        console.log('FD Applications:', this.fdApplications);
      },
      error: (error: any) => {
        console.error('Error loading FD status:', error);
        this.error = 'Failed to load FD applications. Please try again later.';
      }
    });
  }

  statusClass(status: string): string {
    switch (status?.toLowerCase()) {
      case 'active': return 'active';
      case 'matured': return 'matured';
      case 'pending': return 'pending';
      case 'closed': return 'closed';
      case 'rejected': return 'rejected';
      default: return '';
    }
  }

  statusText(status: string): string {
    switch (status?.toLowerCase()) {
      case 'active': return 'Approved';
      case 'matured': return 'Matured';
      case 'pending': return 'Processing';
      case 'closed': return 'Closed';
      case 'rejected': return 'Rejected';
      default: return status || 'Unknown';
    }
  }
}
