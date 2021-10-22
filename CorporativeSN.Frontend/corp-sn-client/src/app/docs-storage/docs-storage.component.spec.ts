import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DocsStorageComponent } from './docs-storage.component';

describe('DocsStorageComponent', () => {
  let component: DocsStorageComponent;
  let fixture: ComponentFixture<DocsStorageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DocsStorageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DocsStorageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
