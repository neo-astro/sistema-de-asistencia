import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AsistenciaFormAddComponent } from './asistencia-form-add.component';

describe('AsistenciaFormAddComponent', () => {
  let component: AsistenciaFormAddComponent;
  let fixture: ComponentFixture<AsistenciaFormAddComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AsistenciaFormAddComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AsistenciaFormAddComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
