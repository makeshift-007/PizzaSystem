import { Shallow } from "shallow-render";
import { AppModule } from "../app.module";
import { BrowserAnimationsModule, NoopAnimationsModule } from '@angular/platform-browser/animations';
import { MenuComponent } from "./menu.component";
import { Pizza } from "../models/pizza";
import { ItemComponent } from "./item/item.component";


describe('Component: MenuComponent', () => {
    let shallow: Shallow<MenuComponent>;

    beforeEach(() => {
        shallow = new Shallow(MenuComponent, AppModule)
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
        const { find } = await shallow.render(`<menu></menu>`);
        expect(find("div.menu-items")).toHaveFound(1);
    });

    it('should create items', async () => {
        let pizzas: Pizza[] = getMockPizzaData();
        const { findComponent } = await shallow.
            render(`<menu [pizzas]="pizzas"></menu>`, {
                bind: {
                    pizzas: pizzas,
                }
            });
        expect(findComponent(ItemComponent)).toHaveFound(pizzas.length);

    });

    it('should emit on customization', async () => {
        let pizzas: Pizza[] = getMockPizzaData();
        let isEmitCalled: boolean = false;
        const { instance } = await shallow.
            render(`<menu [pizzas]="pizzas" (onCustomizeClick)="emit()"></menu>`, {
                bind: {
                    pizzas: pizzas,
                    emit: () => {
                        isEmitCalled = true;
                    }
                }
            });

        instance.customize(null);
        expect(isEmitCalled).toBeTruthy();

    });
});
