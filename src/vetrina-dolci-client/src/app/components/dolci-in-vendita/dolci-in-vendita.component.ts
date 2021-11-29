import { Component, OnInit } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Dolce } from 'src/app/models/dolce';
import { DolceInVendita } from 'src/app/models/dolce-in-vendita';
import { Pagination } from 'src/app/models/pagination';
import { PageInfo } from 'src/app/models/pagination-result';
import { HelperService } from 'src/app/services/helper.service';
import { VetrinaDolciWebapiService } from 'src/app/services/vetrina-dolci-webapi.service';

@Component({
  selector: 'app-dolci-in-vendita',
  templateUrl: './dolci-in-vendita.component.html',
  styleUrls: ['./dolci-in-vendita.component.css']
})
export class DolciInVenditaComponent implements OnInit {
  pagination: Pagination = new Pagination();
  paginationSub = new BehaviorSubject(this.pagination);
  data: DolceInVendita[];

  constructor(private vetrinaDolciWebapiService: VetrinaDolciWebapiService,
    private helperService: HelperService) { }

  ngOnInit(): void {
    this.paginationSub.subscribe(async p => {
      let result = await this.vetrinaDolciWebapiService.getAllDolciInVendita(p);
      this.data = result.data;
      this.pagination.pageInfo = result.pagination;
      // update image
      this.data.forEach(f => {
        this.helperService.getImageFromPixabay(f.dolce.nome).subscribe((s: any) => {
          if (s.total == '0') {
            // load default image
            f.img = 'assets/image-not-found.png';
          } else {
            f.img = s.hits[0]?.webformatURL;
          }
        })
      });
      console.log(result);
    });
  }

  itemsLoaded() {
    console.log('itemsloaded');
  }

  showNext() {
    this.pagination.pageIndex++;
    this.paginationSub.next(this.pagination);
  }
  
  showPrev() {
    this.pagination.pageIndex--;
    this.paginationSub.next(this.pagination);
  }
}
