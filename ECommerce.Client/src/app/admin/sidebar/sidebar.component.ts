import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Modules, Navigations, SideBarNavigation } from '../../models/sidebar-navigation.model';
import { PermissionService } from '../../shared/services/permission/permission.service';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrl: './sidebar.component.scss'
})
export class SidebarComponent {
  @Input() isSidebarOpen = false;
  @Input() isDesktop = false;
  ModuleList = Modules;
  @Output() closeSidebar = new EventEmitter<void>();
  constructor(private permissionService: PermissionService) {

  }
  get Modules(): string[] {
    return Modules
      .filter(module => this.getNavigations(module.name).length > 0) // Only modules with enabled navigations
      .map(module => module.name);
  }

  getNavigations(module: string): SideBarNavigation[] {
    return Navigations
        .filter(nav => nav.module === module) // Filter navigations by module
        .map(nav => new SideBarNavigation(
            nav.name,
            nav.description,
            this.permissionService.hasPermissions(nav.requiredPermission || []), // Ensure it handles undefined permissions
            nav.icon,
            nav.name,
            nav.route
        ))
        .filter(nav => nav.enabled); // Return only enabled items
  }
  onClose(): void {
    if (!this.isDesktop) {
      this.closeSidebar.emit();
    }
  }
}
