import { AfterViewInit, Component, Inject, TemplateRef, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { lastValueFrom, map, merge, startWith, switchMap } from 'rxjs';
import { DolceInVendita } from 'src/app/models/dolce-in-vendita';
import { Pagination } from 'src/app/models/pagination';
import { VetrinaDolciWebapiService } from 'src/app/services/vetrina-dolci-webapi.service';
import { DialogInputDisponibilitaComponent } from '../dialog-input-disponibilita/dialog-input-disponibilita.component';

@Component({
  selector: 'app-backoffice',
  templateUrl: './backoffice.component.html',
  styleUrls: ['./backoffice.component.css']
})
export class BackofficeComponent implements AfterViewInit {
  displayedColumns: string[] = ['id', 'nome', 'disponibilita', 'prezzo', 'note', 'modifica', 'elimina'];
  data: DolceInVendita[] = [];

  resultsLength = 0;
  isLoadingResults = true;

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild('modifica-quantita', { static: true }) templateDialog: TemplateRef<any>;

  constructor(private vetrinaDolciWebapiService: VetrinaDolciWebapiService,
    public dialog: MatDialog) {}

  ngAfterViewInit() {
    merge(this.paginator.page)
      .pipe(
        startWith({}),
        switchMap(() => {
          this.isLoadingResults = true;
          let p = new Pagination();
          p.pageIndex = this.paginator.pageIndex;
          p.pageSize = this.paginator.pageSize;
          p.selectItemsPerPage = this.paginator.pageSizeOptions;
          return this.vetrinaDolciWebapiService.getAllDolciInVendita(p);
        }),
        map(data => {
          // Flip flag to show that loading has finished.
          this.isLoadingResults = false;

          if (data === null) {
            return [];
          }

          // Only refresh the result length if there is new data. In case of rate
          // limit errors, we do not want to reset the paginator to zero, as that
          // would prevent users from re-triggering requests.
          this.resultsLength = data.pagination.totalCount;
          return data.data;
        }),
      )
      .subscribe(data => (this.data = data));
  }

  nuovo () {
    
  }
  
  async elimina (row: DolceInVendita) {
    await lastValueFrom(this.vetrinaDolciWebapiService.deleteDolceInVendita(row.id));
    location.reload();
  }

  modifica(row: DolceInVendita): void {
    const dialogRef = this.dialog.open(DialogInputDisponibilitaComponent, {
      width: '250px',
      data: row,
    });

    dialogRef.afterClosed().subscribe(async(result: DolceInVendita) => {
      await lastValueFrom(this.vetrinaDolciWebapiService.putDolceInVendita(result));
      location.reload();
    });
  }
}