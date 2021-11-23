import { Shallow } from "shallow-render";
import { AppModule } from "../app.module";
import { ImageService } from "./image.service";
import { environment } from "src/environments/environment";

describe('Services: ImageService', () => {
    let shallow: Shallow<ImageService>;

    beforeEach(() => {
        shallow = new Shallow(ImageService, AppModule);
    });
    it('should recieve new cart Item when addUpdateCart called', async () => {
        const { instance } = shallow.createService();
        var result = instance.getImageUrl("imgName");
        expect(result).toEqual(environment.apiBaseUrl + '/images/imgName');
    });

});
