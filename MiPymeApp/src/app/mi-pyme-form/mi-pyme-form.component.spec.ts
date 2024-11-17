import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MiPymeFormComponent } from './mi-pyme-form.component';

describe('MiPymeFormComponent', () => {
  let component: MiPymeFormComponent;
  let fixture: ComponentFixture<MiPymeFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MiPymeFormComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MiPymeFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
