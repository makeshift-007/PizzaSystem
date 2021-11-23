import { Shallow } from "shallow-render";
import { BrowserAnimationsModule, NoopAnimationsModule } from '@angular/platform-browser/animations';
import { AppModule } from "src/app/app.module";
import { ItemComponent } from "./item.component";
import { ImageService } from "src/app/services/image.service";


describe('Component: ItemComponent', () => {
    let shallow: Shallow<ItemComponent>;

    beforeEach(() => {
        shallow = new Shallow(ItemComponent, AppModule)
            .replaceModule(BrowserAnimationsModule, NoopAnimationsModule)
    });   

    it('should create the component with props', async () => {

        let name: string = "name";
        let price: number = 100;
        let imageName: string = "image";
        let item: string = "name";
        let description: string = "description";

        const { find } = await shallow.
            mock(ImageService, { getImageUrl: (imageName) => imageName }).
            render(`<item [name]="name"  [price]="price" [imageName]="imageName" [item]="item" [description]="description"></item>`, {
                bind: {
                    name: name,
                    price: price,
                    imageName: imageName,
                    item: item,
                    description: description
                }
            });

        expect(find("div.item-container")).toHaveFound(1);
        expect(find("p.text-content").nativeElement.textContent.trim()).toEqual(description);
    });
});
