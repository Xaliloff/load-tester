import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LoadTestConfigsListComponent } from './load-test-configs-list.component';

describe('LoadTestConfigsListComponent', () => {
  let component: LoadTestConfigsListComponent;
  let fixture: ComponentFixture<LoadTestConfigsListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LoadTestConfigsListComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LoadTestConfigsListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
