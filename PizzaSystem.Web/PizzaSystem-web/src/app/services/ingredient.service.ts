import { Injectable } from '@angular/core';
import { HttpClient } from
    '@angular/common/http';
import { environment } from '../../environments/environment'
import { Ingredient } from '../models/ingredient';



@Injectable()
export class IngredientService {
    constructor(private http: HttpClient) {
    }

    public getIngredients() {
        const apiUrl: string = environment.apiBaseUrl + '/api/Ingredients';
        return this.http.get<Ingredient[]>(apiUrl);
    }
}