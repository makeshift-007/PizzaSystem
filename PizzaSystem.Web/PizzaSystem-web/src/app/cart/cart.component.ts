import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog'
import { CartItem } from 'src/app/models/cartItem';
import { Ingredient } from 'src/app/models/ingredient';
import { Pizza } from 'src/app/models/pizza';
import { CartService } from 'src/app/services/cart.service';
import { ImageService } from 'src/app/services/image.service';
import { OrderService } from '../services/order.service';
import { MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { IngredientType } from '../pizza.constant';

@Component({
  selector: 'cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.sass']
})
export class CartComponent implements OnInit {
  ingredients: Ingredient[];
  pizzas: Pizza[];
  finalPrice: number = 0;
  sizes: Ingredient[];
  crusts: Ingredient[];
  sauces: Ingredient[];
  toppings: Ingredient[];
  optionals: Ingredient[];
  cartItems: CartItem[] | undefined = [];
  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    public imageService: ImageService,
    public orderService: OrderService,
    private cartService: CartService,
    private dialogRef: MatDialogRef<CartComponent>,
    private snackBar: MatSnackBar
  ) {
    this.ingredients = data.ingredients;
    this.pizzas = data.pizzas;
  }
  ngOnInit(): void {
    this.sizes = this.getIngredients(IngredientType.PIZZA_SIZE);
    this.crusts = this.getIngredients(IngredientType.CRUST);
    this.sauces = this.getIngredients(IngredientType.SAUCE);
    this.toppings = this.getIngredients(IngredientType.TOPPING);
    this.optionals = this.getIngredients(IngredientType.OPTIONAL);    
    this.cartService.currentCartItems.subscribe(cartItems => {      
      this.cartItems = cartItems
      this.updatePrice();
    });
  }

  updatePrice() {
    if (this.cartItems) {
      this.finalPrice = 0;
      this.cartItems.forEach((cartItem) => {
        let price = this.getPizzaPrice(cartItem.pizzaIngredients.pizzaID);
        price += this.getIngredientsPrice(cartItem.pizzaIngredients.toppings);
        price += this.getIngredientsPrice(cartItem.pizzaIngredients.optionals);
        price += this.getIngredientPrice(cartItem.pizzaIngredients.size);
        price += this.getIngredientPrice(cartItem.pizzaIngredients.sauce);
        price += this.getIngredientPrice(cartItem.pizzaIngredients.crust);
        price = price * cartItem.quantity;
        this.finalPrice += price;
      });
    }
  }


  submitOrder() {
    this.orderService.submitOrder({
      products: undefined,
      pizzas: this.cartItems
    }).subscribe(
      data => {
        this.cartService.clearCart();
        this.dialogRef.close(data);
      },
      error => {
        this.snackBar.open("Somthing went wrong!!!");
      });
  }

  editItem(cartItem: CartItem) {
    this.dialogRef.close({
      cartItem: cartItem,
      isEditRequest: true
    });
  }

  removeItem(cartItem: CartItem) {
    this.cartService.removeItem(cartItem.id);
  }

  getPizzaPrice(id: string) {
    let pizza: Pizza | undefined;
    if (id) {
      pizza = this.pizzas.find(x => x.id === id);
      if (pizza != undefined) {
        return pizza.basePrice;
      }
    }
    return 0;
  }

  getPizza(id: string) {
    return this.pizzas.find(x => x.id === id);
  }
  getPizzaImageName(id: string) {
    let pizza: Pizza | undefined;
    if (id) {
      pizza = this.pizzas.find(x => x.id === id);
      if (pizza != undefined) {
        return pizza.imageName;
      }
    }
    return "";
  }

  getIngredientsPrice(ids: string[]) {
    let price: number = 0;
    if (ids) {
      ids.forEach((id: string) => {
        price += this.getIngredientPrice(id);
      });
    }
    return price;
  }

  getIngredientPrice(id: string) {
    let ingredient: Ingredient | undefined;
    if (id) {
      ingredient = this.getIngredient(id)
      if (ingredient != undefined) {
        return ingredient.price;
      }
    }
    return 0;
  }


  getIngredients(ingredientType: number) {
    return this.ingredients.filter(s => s.type === ingredientType);
  }

  getIngredient(id: string) {
    return this.ingredients.find(x => x.id === id);
  }
}
