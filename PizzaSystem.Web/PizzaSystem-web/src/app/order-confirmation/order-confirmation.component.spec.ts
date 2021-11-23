import { Shallow } from "shallow-render";
import { AppModule } from "../app.module";
import { BrowserAnimationsModule, NoopAnimationsModule } from '@angular/platform-browser/animations';
import { Pizza } from "../models/pizza";
import { OrderConfirmationComponent } from "./order-confirmation.component";
import { MAT_DIALOG_DATA } from "@angular/material/dialog";


describe('Component: OrderConfirmationComponent', () => {
    let shallow: Shallow<OrderConfirmationComponent>;

    beforeEach(() => {
        shallow = new Shallow(OrderConfirmationComponent, AppModule)
            .replaceModule(BrowserAnimationsModule, NoopAnimationsModule)
    });

    function getMockPizzaData(): Pizza[] {
        return [{
            basePrice: 0,
            crust: '',
            description: '',
            id: '',
            imageName: '',
            isVegPizza: true,
            name: '',
            sauce: '',
            size: ''
        }];
    }

    it('should create the component', async () => {
        const { find } = await shallow.
            mock(MAT_DIALOG_DATA, {
                orderId: "TEST1"
            }).render(`<order-confirmation></order-confirmation>`);
        expect(find("h2")[0].nativeElement.textContent.trim()).toEqual("Order Confirmed!!!");
        expect(find("h2")[1].nativeElement.textContent.trim()).toEqual("Order ID:TEST1");
    });

});
