import { Component, Inject, ViewChild } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { lastValueFrom, map, merge, startWith, switchMap } from 'rxjs';
import { Dolce } from 'src/app/models/dolce';
import { DolceInVendita } from 'src/app/models/dolce-in-vendita';
import { Pagination } from 'src/app/models/pagination';
import { VetrinaDolciWebapiService } from 'src/app/services/vetrina-dolci-webapi.service';

@Component({
  selector: 'app-dialog-nuovo-dolce-in-vetrina',
  templateUrl: './dialog-nuovo-dolce-in-vetrina.component.html',
  styleUrls: ['./dialog-nuovo-dolce-in-vetrina.component.css']
})
export class DialogNuovoDolceInVetrinaComponent  {
  displayedColumns: string[] = ['id', 'nome'];
  dataSource: MatTableDataSource<Dolce> = new MatTableDataSource<Dolce>();
  resultsLength = 0;

  @ViewChild(MatPaginator) paginator: MatPaginator;

  constructor(
    private vetrinaDolciWebapiService: VetrinaDolciWebapiService,
    public dialogRef: MatDialogRef<DialogNuovoDolceInVetrinaComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DolceInVendita,
  ) {}

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;

    merge(this.paginator.page)
      .pipe(
        startWith({}),
        switchMap(() => {
          let p = new Pagination();
          p.pageIndex = this.paginator.pageIndex;
          p.pageSize = this.paginator.pageSize;
          p.selectItemsPerPage = this.paginator.pageSizeOptions;
          return lastValueFrom(this.vetrinaDolciWebapiService.getAllDolci(p));
        }),
        map(data => {
          // Flip flag to show that loading has finished.
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
      .subscribe(data => (this.dataSource.data = data));
  }

  selectRow(row: Dolce) {
    this.data.dolceId = row.id;
    this.data.dolce = row;
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }
}
