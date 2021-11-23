import { Shallow } from "shallow-render";
import { AppModule } from "../app.module";
import { HttpClient } from "@angular/common/http";
import { of } from "rxjs";
import { OrderService } from "./order.service";
import { Order } from "../models/order";
import { PizzaService } from "./pizza.service";
import { Pizza } from "../models/pizza";


describe('Services: PizzaService', () => {
    let shallow: Shallow<PizzaService>;

    beforeEach(() => {
        shallow = new Shallow(PizzaService, AppModule);
    });
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

    it('should get pizzas when getPizzas called', async () => {
        let pizzas: Pizza[] = getMockPizzas();
        let pizzasReceived!: Pizza[];
        const { instance } = shallow
            .mock(HttpClient, {
                get: () => of(pizzas)
            }).createService();

        instance.getPizzas().subscribe((pizzas) => pizzasReceived = pizzas);

        expect(pizzas).toEqual(pizzasReceived);        
    });

});
