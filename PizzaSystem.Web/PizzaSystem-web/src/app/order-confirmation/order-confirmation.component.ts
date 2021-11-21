import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog'

@Component({
  selector: 'order-confirmation',
  templateUrl: './order-confirmation.component.html',
  styleUrls: ['./order-confirmation.component.sass']
})
export class OrderConfirmationComponent {
  orderId:string;
  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {
    this.orderId = data.orderId;
   }
}
