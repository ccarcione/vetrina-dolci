import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { DolceInVendita } from 'src/app/models/dolce-in-vendita';

@Component({
  selector: 'app-dialog-input-disponibilita',
  templateUrl: './dialog-input-disponibilita.component.html',
  styleUrls: ['./dialog-input-disponibilita.component.css']
})
export class DialogInputDisponibilitaComponent {

  constructor(
    public dialogRef: MatDialogRef<DialogInputDisponibilitaComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DolceInVendita,
  ) {}

  onNoClick(): void {
    this.dialogRef.close();
  }
}
