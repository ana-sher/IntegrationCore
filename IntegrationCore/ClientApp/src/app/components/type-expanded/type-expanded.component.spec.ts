import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TypeExpandedComponent } from './type-expanded.component';

describe('TypeExpandedComponent', () => {
  let component: TypeExpandedComponent;
  let fixture: ComponentFixture<TypeExpandedComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TypeExpandedComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TypeExpandedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
