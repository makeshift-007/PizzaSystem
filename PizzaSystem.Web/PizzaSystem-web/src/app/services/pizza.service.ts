import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpParams } from
    '@angular/common/http';
import { environment } from '../../environments/environment'
import { Pizza } from '../models/pizza'



@Injectable()
export class PizzaService {
    constructor(private http: HttpClient) {
    }

    public getPizzas() {
        const apiUrl: string = environment.apiBaseUrl + '/api/Pizzas';
        return this.http.get<Pizza[]>(apiUrl);
    }
}