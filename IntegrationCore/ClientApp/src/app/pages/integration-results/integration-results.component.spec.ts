import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { IntegrationResultsComponent } from './integration-results.component';

describe('IntegrationResultsComponent', () => {
  let component: IntegrationResultsComponent;
  let fixture: ComponentFixture<IntegrationResultsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ IntegrationResultsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(IntegrationResultsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
