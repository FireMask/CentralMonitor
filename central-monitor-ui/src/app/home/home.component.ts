import { Component } from '@angular/core';
import { SalesDashboardComponent } from '../sales-dashboard/sales-dashboard.component';

import { MatToolbar } from '@angular/material/toolbar';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { RouterLink } from '@angular/router';
import { NgForOf } from '@angular/common';

@Component({
  selector: 'app-home',
  imports: [
    MatToolbar,
    MatToolbarModule,
    MatSidenavModule,
    MatIconModule,
    MatListModule,
    MatButtonModule,
    MatCardModule,
    RouterLink,
    NgForOf,
    SalesDashboardComponent
  ],
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent {
  isSidebarOpen = true;

  toggleSidebar(): void {
    this.isSidebarOpen = !this.isSidebarOpen;
  }

  navLinks = [
    { label: 'Dashboard', icon: 'dashboard', route: '/' },
    { label: 'Reports', icon: 'bar_chart', route: '/' },
    { label: 'Settings', icon: 'settings', route: '/' },
  ];
}
