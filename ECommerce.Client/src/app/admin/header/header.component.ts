import { Component, EventEmitter, Input, Output } from '@angular/core';
import { LoginService } from '../../login/login.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent {

  @Input() isSidebarOpen = false;
  @Input() isDesktop = false;
  @Output() toggleSidebar = new EventEmitter<void>();

  constructor(
    private loginService: LoginService,
    private router: Router
  ) {

  }

  onToggle(): void {
    this.toggleSidebar.emit();
  }

  logout(): void {
    this.loginService.logout();
    this.router.navigate(['/'])
    // Implement actual logout logic here (e.g., remove token, redirect)
  }
}
