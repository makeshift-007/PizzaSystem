import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Pizza } from '../models/pizza';

@Component({
  selector: 'menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.sass']
})
export class MenuComponent {  
  @Input() pizzas: Pizza[] = [];
  @Output() onCustomizeClick = new EventEmitter<any>();
  customize(item:any): void {
    this.onCustomizeClick.emit(item);
  }
}
