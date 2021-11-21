import { CartItem } from "./cartItem";
import { Product } from "./product";

export interface Order {
    pizzas:CartItem[]|undefined,
    products:Product[]|undefined
}
  

  
  
  
  