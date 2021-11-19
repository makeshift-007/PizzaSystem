import { Injectable } from '@angular/core';
import { HttpClient } from
    '@angular/common/http';
import { environment } from '../../environments/environment'



@Injectable()
export class ImageService {

    public getImageUrl(imageName: string): string {
        return environment.apiBaseUrl + '/images/' + imageName;        
    }
}