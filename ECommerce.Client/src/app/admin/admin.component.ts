import { Component } from '@angular/core';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrl: './admin.component.scss'
})
export class AdminComponent {
  
  isSidebarOpen = false;
  isDesktop = false;

  ngOnInit(): void {
    this.checkScreenSize();
    window.addEventListener('resize', this.checkScreenSize.bind(this));
  }

  checkScreenSize(): void {
    this.isDesktop = window.innerWidth >= 1024;
    if (this.isDesktop) {
      this.isSidebarOpen = true;
    } else {
      this.isSidebarOpen = false;
    }
  }

  toggleSidebar(): void {
    this.isSidebarOpen = !this.isSidebarOpen;
  }

  closeSidebar(): void {
    if (!this.isDesktop) {
      this.isSidebarOpen = false;
    }
  }
}
