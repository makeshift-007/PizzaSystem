import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ImageService } from 'src/app/services/image.service';

@Component({
  selector: 'item',
  templateUrl: './item.component.html',
  styleUrls: ['./item.component.sass']
})
export class ItemComponent implements OnInit {
  @Input() name: string = '';
  @Input() price: number = 0;
  @Input() imageName: string = '';
  @Input() description: string = '';
  @Input() item: any;
  @Output() onCustomizeClick = new EventEmitter<any>();
  constructor(public imageService: ImageService
  ) { }

  ngOnInit(): void {

  }
  customize(): void {
    this.onCustomizeClick.emit(this.item);
  }
}
