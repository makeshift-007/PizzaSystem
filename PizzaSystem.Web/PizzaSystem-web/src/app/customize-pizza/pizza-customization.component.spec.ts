import { Shallow } from "shallow-render";
import { AppModule } from "../app.module";
import { BrowserAnimationsModule, NoopAnimationsModule } from '@angular/platform-browser/animations';
import { Pizza } from "../models/pizza";
import { PizzaCustomizationComponent } from "./pizza-customization.component";
import { ImageService } from "../services/image.service";
import { CartService } from "../services/cart.service";
import { MAT_DIALOG_DATA } from "@angular/material/dialog";
import { Ingredient } from "../models/ingredient";
import { CartItem } from "../models/cartItem";



describe('Component: PizzaCustomizationComponent', () => {
    let shallow: Shallow<PizzaCustomizationComponent>;

    beforeEach(() => {
        shallow = new Shallow(PizzaCustomizationComponent, AppModule)
            .replaceModule(BrowserAnimationsModule, NoopAnimationsModule)
    });


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

    it('should create the component', async () => {
        let pizza = getMockPizza();
        const { find } = await shallow.
            mock(ImageService, { getImageUrl: (imageName) => imageName }).
            mock(CartService, { addUpdateCart: (item) => { } }).
            mock(MAT_DIALOG_DATA, {
                pizza: pizza,
                isEdit: false,
                ingredients: getIngredients()
            }).
            render('<pizza-customization></pizza-customization>');

        expect(find("h2.center")[0].nativeElement.textContent.trim()).toEqual(pizza.name);
    });

    it('should call submit in order Service', async () => {
        let pizza = getMockPizza();
        let cartItem: CartItem = {
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
        }
        let submitCalled: boolean = false;
        let cartItemReceived!: CartItem;
        const { find, instance } = await shallow.
            mock(ImageService, { getImageUrl: (imageName) => imageName }).
            mock(CartService, {
                addUpdateCart: (item) => {
                    submitCalled = true;
                    cartItemReceived = item;
                }
            }).
            mock(MAT_DIALOG_DATA, {
                pizza: pizza,
                isEdit: false,
                ingredients: getIngredients(),
                cartItem: cartItem
            }).
            render('<pizza-customization></pizza-customization>');
        find('button')[1].triggerEventHandler('click', undefined);
        expect(submitCalled).toBeTruthy();
        expect(cartItemReceived).not.toBeNull();
        expect(cartItem).toEqual(cartItemReceived);
    });

});
