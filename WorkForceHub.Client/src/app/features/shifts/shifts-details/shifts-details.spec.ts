import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ShiftsDetails } from './shifts-details';

describe('ShiftsDetails', () => {
  let component: ShiftsDetails;
  let fixture: ComponentFixture<ShiftsDetails>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ShiftsDetails],
    }).compileComponents();

    fixture = TestBed.createComponent(ShiftsDetails);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
