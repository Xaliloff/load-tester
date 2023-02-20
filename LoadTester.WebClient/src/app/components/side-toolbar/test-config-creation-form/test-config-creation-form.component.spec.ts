import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TestConfigCreationFormComponent } from './test-config-creation-form.component';

describe('TestConfigCreationFormComponent', () => {
  let component: TestConfigCreationFormComponent;
  let fixture: ComponentFixture<TestConfigCreationFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TestConfigCreationFormComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TestConfigCreationFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
