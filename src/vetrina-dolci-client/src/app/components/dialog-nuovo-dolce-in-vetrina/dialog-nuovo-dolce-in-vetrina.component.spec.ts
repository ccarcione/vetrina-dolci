import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DialogNuovoDolceInVetrinaComponent } from './dialog-nuovo-dolce-in-vetrina.component';

describe('DialogNuovoDolceInVetrinaComponent', () => {
  let component: DialogNuovoDolceInVetrinaComponent;
  let fixture: ComponentFixture<DialogNuovoDolceInVetrinaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DialogNuovoDolceInVetrinaComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DialogNuovoDolceInVetrinaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
