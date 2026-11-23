import { Component, inject, signal } from '@angular/core';
import { RouterOutlet,  RouterLink, RouterLinkActive } from '@angular/router';
import { PageTitleService } from '../../core/services/PageTitleService';


@Component({
  imports: [RouterOutlet,RouterLink, RouterLinkActive],
  selector: 'app-main-layout',
  styleUrl: './main-layout.css',
  templateUrl: './main-layout.html',
})
export class MainLayout {

  isSidebarCollapsed = signal(false);
  
  pageService = inject(PageTitleService);
  toggleSidebar(){
      this.isSidebarCollapsed.update(value => !value);
  }
}
