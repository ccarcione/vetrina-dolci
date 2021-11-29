import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DialogInputDisponibilitaComponent } from './dialog-input-disponibilita.component';

describe('DialogInputDisponibilitaComponent', () => {
  let component: DialogInputDisponibilitaComponent;
  let fixture: ComponentFixture<DialogInputDisponibilitaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DialogInputDisponibilitaComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DialogInputDisponibilitaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
