import { Injectable } from '@angular/core';
import { HttpClient } from
    '@angular/common/http';
import { environment } from '../../environments/environment'
import { Order } from '../models/order';
@Injectable()
export class OrderService {
    constructor(private http: HttpClient) {
    }

    public submitOrder(order: Order) {
        const apiUrl: string = environment.apiBaseUrl + '/api/Orders';
        return this.http.post<Order>(apiUrl, order);
    }
}