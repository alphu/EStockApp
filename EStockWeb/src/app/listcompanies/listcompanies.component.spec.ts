import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListcompaniesComponent } from './listcompanies.component';

describe('ListcompaniesComponent', () => {
  let component: ListcompaniesComponent;
  let fixture: ComponentFixture<ListcompaniesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ListcompaniesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ListcompaniesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
