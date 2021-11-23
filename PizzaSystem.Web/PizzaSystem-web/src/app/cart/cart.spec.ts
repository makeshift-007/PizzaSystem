import { CartComponent } from './cart.component';
import { Shallow } from "shallow-render";
import { AppModule } from "../app.module";
import { BrowserAnimationsModule, NoopAnimationsModule } from '@angular/platform-browser/animations';
import { MAT_DIALOG_DATA, MatDialogRef, MatDialogModule } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Ingredient } from '../models/ingredient';
import { Pizza } from '../models/pizza';
import { CartService } from '../services/cart.service';
import { CartItem } from '../models/cartItem';
import { of } from 'rxjs';
import { ImageService } from '../services/image.service';
import { OrderService } from '../services/order.service';
import { Order } from '../models/order';



describe('Component: CartComponent', () => {
    let shallow: Shallow<CartComponent>;


    function getIngredients(): Ingredient[] {
        return [{
            id: "ID1",
            imageName: "imageName",
            name: "name",
            price: 1,
            type: 1
        },
        {
            id: "ID2",
            imageName: "imageNam2",
            name: "nam2",
            price: 11,
            type: 2
        }];
    }
    function getMockPizza(): Pizza {
        return {
            basePrice: 100,
            crust: '1',
            description: 'desc',
            id: '1',
            imageName: '1',
            isVegPizza: true,
            name: 'name',
            sauce: '1',
            size: '1'
        };
    }
    function getMockPizzas(): Pizza[] {
        return [getMockPizza()];
    }

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


    beforeEach(() => {
        shallow = new Shallow(CartComponent, AppModule)
            .replaceModule(BrowserAnimationsModule, NoopAnimationsModule)
            .replaceModule(MatDialogModule, MatDialogModule)
            .mock(MatDialogRef, {
            })
            .mock(MatSnackBar, {
            }).mock(CartService, {
                currentCartItems: {
                    subscribe: () => {

                    }
                }
            });
    });

    it('should create the component', async () => {
        const { find } = await shallow.
            mock(MAT_DIALOG_DATA, {
                pizzas: getMockPizzas(),
                ingredients: getIngredients()
            }).
            render('<cart></cart>');

        expect(find("h2.center")[0].nativeElement.textContent.trim()).toEqual("Cart")
    });

    it('should call cart remove item on remove click', async () => {

        let cartItems = getCartItems();
        let removedItemId: number = 0;
        const { find } = await shallow.
            mock(MAT_DIALOG_DATA, {
                pizzas: getMockPizzas(),
                ingredients: getIngredients()
            })
            .mock(ImageService, { getImageUrl: (imageName) => imageName })
            .mock(CartService, {
                currentCartItems: of(cartItems),
                removeItem: (id) => {
                    removedItemId = id;
                }
            }).
            render('<cart></cart>');

        find('button.remove-item')[0].triggerEventHandler('click', undefined);
        expect(removedItemId).toEqual(cartItems[0].id);
    });

    it('should call editItem callback on edit click', async () => {

        let cartItems = getCartItems();
        let cartItemReceived!: CartItem;
        let isEditRequest: boolean = false;
        const { find } = await shallow.
            mock(MAT_DIALOG_DATA, {
                pizzas: getMockPizzas(),
                ingredients: getIngredients()
            })
            .mock(ImageService, { getImageUrl: (imageName) => imageName })
            .mock(MatDialogRef, {
                close: (data: any) => {
                    cartItemReceived = data.cartItem;
                    isEditRequest = data.isEditRequest;
                }
            })
            .mock(CartService, {
                currentCartItems: of(cartItems),
                addUpdateCart: (item) => {
                    cartItemReceived = item;
                }
            }).
            render('<cart></cart>');

        find('button.edit-item')[0].triggerEventHandler('click', undefined);
        expect(cartItemReceived).toEqual(cartItems[0]);
        expect(isEditRequest).toBeTruthy();
    });

    it('should submit order on submit click', async () => {

        let cartItems = getCartItems();
        let isOrderCalled: boolean = false;


        const { find } = await shallow.
            mock(MAT_DIALOG_DATA, {
                pizzas: getMockPizzas(),
                ingredients: getIngredients()
            })
            .mock(ImageService, { getImageUrl: (imageName) => imageName })
            .mock(CartService, {
                currentCartItems: of(cartItems),
                clearCart: () => { }
            })
            .mock(OrderService, {
                submitOrder: (order: Order) => {
                    isOrderCalled = true;
                    return of(order);
                },
            }).
            render('<cart></cart>');

        find('button.submit-order')[0].triggerEventHandler('click', undefined);
        expect(isOrderCalled).toBeTruthy();
    });
});
