export interface CartItem {
    id:number;
    quantity: number,
    pizzaIngredients: {
        pizzaID: string,
        toppings: string[],
        optionals: string[],
        sauce: string
        crust: string,
        size: string
    }
}
