import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs/internal/BehaviorSubject';
import { CartItem } from '../models/cartItem';

@Injectable()
export class CartService {
    private cartItems: CartItem[] = [];
    private currentId: number = 1;
    private subscribeCartChange = new BehaviorSubject<CartItem[] | undefined>(undefined);
    currentCartItems = this.subscribeCartChange.asObservable();
    constructor() { }

    addUpdateCart(cartItem: CartItem) {
        if (cartItem) {
            if (cartItem.id > 0) {
                let item = this.cartItems.find(x => x.id === cartItem.id);
                if (item) {
                    this.cartItems = this.cartItems.filter(item => item.id !== cartItem.id);
                }
                this.cartItems.push(cartItem);
            } else {
                cartItem.id = this.currentId++;
                this.cartItems.push(cartItem);
            }

            this.subscribeCartChange.next(this.cartItems);
        }
    }

    clearCart() {
        this.cartItems = [];
        this.subscribeCartChange.next(this.cartItems);
    }

    removeItem(id: number) {
        this.cartItems = this.cartItems.filter(item => item.id !== id);
        this.subscribeCartChange.next(this.cartItems);
    }

}