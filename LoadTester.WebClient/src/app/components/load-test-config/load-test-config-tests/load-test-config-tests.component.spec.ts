import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LoadTestConfigTestsComponent } from './load-test-config-tests.component';

describe('LoadTestConfigTestsComponent', () => {
  let component: LoadTestConfigTestsComponent;
  let fixture: ComponentFixture<LoadTestConfigTestsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LoadTestConfigTestsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LoadTestConfigTestsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
