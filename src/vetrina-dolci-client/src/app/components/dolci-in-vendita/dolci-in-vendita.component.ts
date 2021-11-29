import { Component, OnInit } from '@angular/core';
import { Dolce } from 'src/app/models/dolce';
import { DolceInVendita } from 'src/app/models/dolce-in-vendita';
import { Pagination } from 'src/app/models/pagination';
import { HelperService } from 'src/app/services/helper.service';
import { VetrinaDolciWebapiService } from 'src/app/services/vetrina-dolci-webapi.service';

@Component({
  selector: 'app-dolci-in-vendita',
  templateUrl: './dolci-in-vendita.component.html',
  styleUrls: ['./dolci-in-vendita.component.css']
})
export class DolciInVenditaComponent implements OnInit {
  pagination: Pagination = new Pagination();
  data: DolceInVendita[];

  constructor(private vetrinaDolciWebapiService: VetrinaDolciWebapiService,
    private helperService: HelperService) { }

  ngOnInit(): void {
    this.pagination.pageIndex = 1;
    this.vetrinaDolciWebapiService.getAllDolciInVendita(this.pagination).subscribe(x =>
      {
        this.data = x.data;
      });
  }

  itemsLoaded() {
    console.log('itemsloaded');
  }
}
