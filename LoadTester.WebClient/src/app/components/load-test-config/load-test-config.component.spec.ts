import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LoadTestConfigComponent } from './load-test-config.component';

describe('LoadTestConfigComponent', () => {
  let component: LoadTestConfigComponent;
  let fixture: ComponentFixture<LoadTestConfigComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LoadTestConfigComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LoadTestConfigComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
