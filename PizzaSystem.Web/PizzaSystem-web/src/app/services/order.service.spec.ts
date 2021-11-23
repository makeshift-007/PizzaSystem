import { Shallow } from "shallow-render";
import { AppModule } from "../app.module";
import { HttpClient } from "@angular/common/http";
import { of } from "rxjs";
import { OrderService } from "./order.service";
import { Order } from "../models/order";


describe('Services: OrderService', () => {
    let shallow: Shallow<OrderService>;

    beforeEach(() => {
        shallow = new Shallow(OrderService, AppModule);
    });

    it('should submit order when submitOrder called', async () => {
        var order: Order = {
            pizzas: undefined,
            products: undefined
        }
        var orderReceived!: Order;
        var orderReceivedResponse!: Order;
        const { instance } = shallow
            .mock(HttpClient, {
                post: (url, body) => {
                    orderReceived = body;
                    return of(body);
                }
            }).createService();

        instance.submitOrder(order).subscribe((order) => orderReceivedResponse = order);

        expect(orderReceived).toEqual(order);
        expect(orderReceivedResponse).toEqual(order);
    });

});
