import { CartService } from "./cart.service";
import { Shallow } from "shallow-render";
import { AppModule } from "../app.module";
import { CartItem } from "../models/cartItem";

describe('Services: CartService', () => {
    let shallow: Shallow<CartService>;

    beforeEach(() => {
        shallow = new Shallow(CartService, AppModule);
    });

    function getCartItems(): CartItem[] {
        return [{
            id: 1,
            quantity: 10,
            pizzaIngredients: {
                size: "1",
                sauce: "1",
                crust: "1",
                pizzaID: "1",
                toppings: ["1", "1"],
                optionals: ["1", "2"]
            }
        }];
    }

    it('should recieve new cart Item when addUpdateCart called', async () => {
        const { instance } = shallow.createService();
        var item = getCartItems()[0];
        let cartItemsReceived!: CartItem[];

        instance.currentCartItems.subscribe((cartItems: CartItem[] | undefined) => {
            if (cartItems)
                cartItemsReceived = cartItems;
        })
        instance.addUpdateCart(item);
        expect(cartItemsReceived?.length).toBeGreaterThan(0);
        expect(cartItemsReceived[0]).toEqual(item);

    });

    it('should clear cart Item when clearCart called', async () => {
        const { instance } = shallow.createService();
        var item = getCartItems()[0];
        let cartItemsReceived!: CartItem[];
        instance.addUpdateCart(item);        
        instance.currentCartItems.subscribe((cartItems: CartItem[] | undefined) => {
            if (cartItems)
                cartItemsReceived = cartItems;
        });
        instance.clearCart();
        expect(cartItemsReceived?.length).toEqual(0);
    });

    it('should remove cart Item when clearCart called', async () => {
        const { instance } = shallow.createService();
        var item = getCartItems()[0];
        let cartItemsReceived!: CartItem[];
        instance.addUpdateCart(item);        
        instance.currentCartItems.subscribe((cartItems: CartItem[] | undefined) => {
            if (cartItems)
                cartItemsReceived = cartItems;
        });
        instance.removeItem(item.id);
        expect(cartItemsReceived?.length).toEqual(0);
    });
});
