import { Component, OnInit, ViewChild } from '@angular/core';
import { Ingredient } from './models/ingredient';
import { Pizza } from './models/pizza';
import { IngredientService } from './services/ingredient.service';
import { PizzaService } from './services/pizza.service';
import { MatDialog } from '@angular/material/dialog';
import { CartComponent } from './cart/cart.component';
import { OrderConfirmationComponent } from './order-confirmation/order-confirmation.component';
import { PizzaCustomizationComponent } from './customize-pizza/pizza-customization.component';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.sass'],
})
export class AppComponent  implements OnInit{
  pizzas: Pizza[] = [];
  ingredients: Ingredient[] = [];
  title = 'Pizza Store';
  constructor(
    private pizzaService: PizzaService,
    public dialog: MatDialog,
    private ingredientService: IngredientService) {
 
  }
  ngOnInit(): void {
    this.pizzaService.getPizzas().subscribe((pizzas: Pizza[]) => {
      this.pizzas = pizzas;
    });
    this.ingredientService.getIngredients().subscribe((ingredients: Ingredient[]) => {
      this.ingredients = ingredients;
    });
  }

  customize(item: Pizza): void {
    const pizzaCustomization = this.dialog.open(PizzaCustomizationComponent, {
      width: '25%',
      data: {
        pizza: item,
        ingredients: this.ingredients
      }
    });
  }
  openCart() {
    const pizzaCustomization = this.dialog.open(CartComponent, {
      width: '25%',
      data: {
        ingredients: this.ingredients,
        pizzas: this.pizzas
      },
    });

    pizzaCustomization.afterClosed().subscribe(result => {
      if (result.isEditRequest) {
        this.dialog.open(PizzaCustomizationComponent, {
          width: '25%',
          data: {
            pizza: this.pizzas.find(x => x.id === result.cartItem.pizzaIngredients.pizzaID),
            ingredients: this.ingredients,
            cartItem: result.cartItem
          }
        });
      }
      else if (result)
        this.dialog.open(OrderConfirmationComponent, {
          width: '25%',
          data: {
            orderId: result.orderId
          },
        });
    });
  }

}
