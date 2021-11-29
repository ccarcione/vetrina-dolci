import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DolciInVenditaComponent } from './dolci-in-vendita.component';

describe('DolciInVenditaComponent', () => {
  let component: DolciInVenditaComponent;
  let fixture: ComponentFixture<DolciInVenditaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DolciInVenditaComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DolciInVenditaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
