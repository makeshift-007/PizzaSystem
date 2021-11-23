import { Shallow } from "shallow-render";
import { AppModule } from "../app.module";
import { IngredientService } from "./ingredient.service";
import { HttpClient } from "@angular/common/http";
import { Ingredient } from "../models/ingredient";
import { of } from "rxjs";


describe('Services: IngredientService', () => {
    let shallow: Shallow<IngredientService>;

    beforeEach(() => {
        shallow = new Shallow(IngredientService, AppModule);
    });
    function getIngredients(): Ingredient[] {
        return [{
            id: "ID1",
            imageName: "imageName",
            name: "name",
            price: 1,
            type: 1
        },
        {
            id: "ID2",
            imageName: "imageNam2",
            name: "nam2",
            price: 11,
            type: 2
        }];
    }

    it('should get Ingredients when getIngredients called', async () => {        
        var ingredients = getIngredients();
        var ingredientsReceived!: Ingredient[];
        const { instance } = shallow
            .mock(HttpClient, {
                get: () => of(ingredients)
            }).createService();

        instance.getIngredients().subscribe((ingredients) => ingredientsReceived = ingredients);        
        expect(ingredientsReceived).toEqual(ingredients);
    });

});
