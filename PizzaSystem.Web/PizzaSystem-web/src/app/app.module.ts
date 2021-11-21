import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MenuComponent } from './menu/menu.component';
import { ItemComponent } from './menu/item/item.component';
import { PizzaService } from './services/pizza.service';
import { ImageService } from './services/image.service';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatDialogModule } from '@angular/material/dialog';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { IngredientService } from './services/ingredient.service';
import { CartService } from './services/cart.service';
import { CartComponent } from './cart/cart.component';
import { OrderService } from './services/order.service';
import {MatSnackBarModule} from '@angular/material/snack-bar';
import { OrderConfirmationComponent } from './order-confirmation/order-confirmation.component';
import { PizzaCustomizationComponent } from './customize-pizza/pizza-customization.component';

@NgModule({
  declarations: [
    AppComponent,
    MenuComponent,
    ItemComponent,    
    PizzaCustomizationComponent,
    CartComponent,
    OrderConfirmationComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,    
    BrowserAnimationsModule,
    MatButtonModule,
    MatCardModule,
    MatToolbarModule,
    MatSnackBarModule,
    MatIconModule,
    MatInputModule,
    MatDialogModule,
    MatSelectModule,
    FormsModule,
    ReactiveFormsModule,
    MatCheckboxModule
  ],
  providers: [PizzaService, ImageService, IngredientService, CartService, OrderService],
  bootstrap: [AppComponent]
})
export class AppModule { }
