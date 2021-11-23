import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA } from '@angular/material/dialog'
import { CartItem } from 'src/app/models/cartItem';
import { Ingredient } from 'src/app/models/ingredient';
import { Pizza } from 'src/app/models/pizza';
import { CartService } from 'src/app/services/cart.service';
import { ImageService } from 'src/app/services/image.service';
import { IngredientType } from '../pizza.constant';


@Component({
  selector: 'pizza-customization',
  templateUrl: './pizza-customization.component.html',
  styleUrls: ['./pizza-customization.component.sass']
})
export class PizzaCustomizationComponent implements OnInit {
  pizza: Pizza;
  toppingList: string[];

  finalPrice: number;
  ingredients: Ingredient[];
  pizzaForm: FormGroup;
  isEdit: boolean;

  sizes: Ingredient[];
  crusts: Ingredient[];
  sauces: Ingredient[];
  toppings: Ingredient[];
  optionals: Ingredient[];
  cartItem: CartItem;

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    public imageService: ImageService,
    private fb: FormBuilder,
    private cartService: CartService
  ) {    
    this.pizza = data.pizza;
    this.finalPrice = this.pizza.basePrice;
    this.ingredients = data.ingredients;
    this.cartItem = data.cartItem;
    this.isEdit = data.isEdit;
    this.createForm(this.cartItem);

  }
  ngOnInit(): void {

    this.sizes = this.getIngredients(IngredientType.PIZZA_SIZE);
    this.crusts = this.getIngredients(IngredientType.CRUST);
    this.sauces = this.getIngredients(IngredientType.SAUCE);
    this.toppings = this.getIngredients(IngredientType.TOPPING);
    this.optionals = this.getIngredients(IngredientType.OPTIONAL);

    this.pizzaForm.valueChanges.subscribe(() => {
      this.updatePrice();
    });
  }

  submit() {
    let customPizza = this.pizzaForm.value;
    const item = {      
      id: this.cartItem ? this.cartItem.id : 0,
      pizzaIngredients: {
        toppings: customPizza.toppings,
        optionals: customPizza.optionals,
        pizzaID: this.pizza.id,
        sauce: customPizza.sauce,
        crust: customPizza.crust,
        size: customPizza.size
      },
      quantity: customPizza.quantity
    };
    this.cartService.addUpdateCart(item);
  }


  updatePrice() {
    let price = this.pizza.basePrice;
    let customPizza = this.pizzaForm.value;
    price += this.getIngredientsPrice(customPizza.toppings);
    price += this.getIngredientsPrice(customPizza.optionals);
    price += this.getIngredientPrice(customPizza.size);
    price += this.getIngredientPrice(customPizza.sauce);
    price += this.getIngredientPrice(customPizza.crust);
    price = price * customPizza.quantity;
    this.finalPrice = price;
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

  createForm(cartItem: CartItem | undefined) {
    if (cartItem) {
      this.pizzaForm = this.fb.group({
        quantity: [cartItem.quantity, [Validators.required, Validators.pattern("^[0-9]*$"), Validators.min(1)]],
        sauce: [cartItem.pizzaIngredients.sauce, Validators.required],
        crust: [cartItem.pizzaIngredients.crust, Validators.required],
        toppings: [cartItem.pizzaIngredients.toppings],
        optionals: [cartItem.pizzaIngredients.optionals],
        size: [cartItem.pizzaIngredients.size, Validators.required],
      });
    } else
      this.pizzaForm = this.fb.group({
        quantity: ['1', [Validators.required, Validators.pattern("^[0-9]*$"), Validators.min(1)]],
        sauce: ['', Validators.required],
        crust: ['', Validators.required],
        toppings: [''],
        optionals: [''],
        size: ['', Validators.required],
      });
  }

}
