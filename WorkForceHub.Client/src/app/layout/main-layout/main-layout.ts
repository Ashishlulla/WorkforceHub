import { Component, signal } from '@angular/core';
import { RouterOutlet,  RouterLink, RouterLinkActive } from '@angular/router';


@Component({
  imports: [RouterOutlet,RouterLink, RouterLinkActive],
  selector: 'app-main-layout',
  styleUrl: './main-layout.css',
  templateUrl: './main-layout.html',
})
export class MainLayout {

  isSidebarCollapsed = signal(false);

  toggleSidebar(){
      this.isSidebarCollapsed.update(value => !value);
  }
}
