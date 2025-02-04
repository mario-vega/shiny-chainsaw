import { Component } from '@angular/core';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatNavList, MatListItem } from '@angular/material/list';
import { MatMenu, MatMenuItem, MatMenuTrigger } from '@angular/material/menu';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [MatToolbarModule, MatIconModule, MatNavList, MatListItem, MatMenu, MatMenuItem, MatMenuTrigger],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent {

}
