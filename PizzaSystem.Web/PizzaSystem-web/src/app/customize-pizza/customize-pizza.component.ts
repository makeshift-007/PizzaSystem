import { Component, Input } from '@angular/core';
import { Pizza } from '../models/pizza';
import { ModalService } from '../_modal';

@Component({
  selector: 'customize-pizza',
  templateUrl: './customize-pizza.component.html',
  styleUrls: ['./customize-pizza.component.sass']
})
export class CustomizePizzaComponent {
  customizePizza: string = "customize-pizza";
  pizza: Pizza;
  constructor(private modalService: ModalService) { }

  openCustomization(item: Pizza) {
    this.pizza = item;
    this.modalService.open(this.customizePizza);
  }

  closeCustomization() {
    this.modalService.close(this.customizePizza);
  }
}
