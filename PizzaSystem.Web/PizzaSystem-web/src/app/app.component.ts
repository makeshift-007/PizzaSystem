import { Component, ViewChild } from '@angular/core';
import { CustomizePizzaComponent } from './customize-pizza/customize-pizza.component';
import { Pizza } from './models/pizza';
import { PizzaService } from './services/pizza.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.sass'],
})
export class AppComponent {
  pizzas: Pizza[] = [];
  @ViewChild(CustomizePizzaComponent) customizePizza: CustomizePizzaComponent;
  title = 'Pizza Store';
  constructor(private pizzaService: PizzaService) {
    pizzaService.getPizzas().subscribe((pizzas: Pizza[]) => {
      this.pizzas = pizzas;
    });
  }

  customize(item: Pizza): void {
    this.customizePizza.openCustomization(item);
    //alert('customizing');
  }

}
