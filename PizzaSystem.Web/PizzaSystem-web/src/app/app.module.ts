import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MenuComponent } from './menu/menu.component';
import { ItemComponent } from './menu/item/item.component';
import { PizzaService } from './services/pizza.service';
import { ImageService } from './services/image.service';
import { HttpClientModule } from '@angular/common/http';
import { ModalModule } from './_modal';
import { CustomizePizzaComponent } from './customize-pizza/customize-pizza.component';

@NgModule({
  declarations: [
    AppComponent,
    MenuComponent,
    ItemComponent,
    CustomizePizzaComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ModalModule
  ],
  providers: [PizzaService,ImageService],
  bootstrap: [AppComponent]
})
export class AppModule { }
