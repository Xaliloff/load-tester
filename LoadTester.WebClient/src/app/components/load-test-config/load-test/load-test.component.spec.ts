import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LoadTestComponent } from './load-test.component';

describe('LoadTestComponent', () => {
  let component: LoadTestComponent;
  let fixture: ComponentFixture<LoadTestComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LoadTestComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LoadTestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
