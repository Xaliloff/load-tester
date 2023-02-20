import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LoadTestConfigInfoComponent } from './load-test-config-info.component';

describe('LoadTestConfigInfoComponent', () => {
  let component: LoadTestConfigInfoComponent;
  let fixture: ComponentFixture<LoadTestConfigInfoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LoadTestConfigInfoComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LoadTestConfigInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
