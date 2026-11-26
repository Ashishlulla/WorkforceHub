import { ChangeDetectorRef, Component, inject, OnInit } from '@angular/core';
import { ShiftService } from '../shift';
import { Router } from '@angular/router';
import { AbstractControl, FormBuilder, ReactiveFormsModule, ValidatorFn, Validators } from '@angular/forms';
import { PageTitleService } from '../../../core/services/PageTitleService';
import { ShiftModel } from '../models/shift.model';
import { CreateShiftModel } from '../models/create-shift.model';
import { ShiftType } from '../models/shiftType.enum';
const ValidateShiftDuration: ValidatorFn = (control: AbstractControl) => {

  const  startTime = control.get('startTime')?.value;
  const endTime = control.get('endTime')?.value;
  const isOvernight = control.get('isOvernight')?.value;

  if(!startTime || !endTime){

    return null;
  }


  if (startTime === endTime) {
    return {invalidShiftDuration: true};
  }

  if (isOvernight) {
    return startTime > endTime ? null : {invalidShiftDuration: true};
  }
  
  return endTime > startTime ? null: {invalidShiftDuration: true};

}

@Component({
  imports: [ReactiveFormsModule],
  selector: 'app-create-shift',
  styleUrl: './create-shift.css',
  templateUrl: './create-shift.html',
})
export class CreateShift implements OnInit {

  private fb = inject(FormBuilder);

  constructor( private shiftService: ShiftService, private pageTitleService: PageTitleService , private router: Router, private cdr: ChangeDetectorRef ){}

  shiftForm = this.fb.group({

    shiftType: this.fb.control<ShiftType | null>(null, Validators.required),
    startTime: ['', [Validators.required]],
    endTime: ['', [Validators.required]],
    gracePeriodInMinutes: [0, [Validators.required, Validators.min(0)]],
    isOvernight:[false],
    isActive: [true],
   
  },
  {
    validators: ValidateShiftDuration
  }


);

  ngOnInit(): void {
     
    this.pageTitleService.setPageTitle("Create Shift", "add new shift to you organization");
  
  }

  onSubmit(){
    
    if(this.shiftForm.invalid){
      this.shiftForm.markAllAsTouched();
      return;
    }

    const shiftData = this.shiftForm.getRawValue() as CreateShiftModel;

    this.shiftService.createShift(shiftData).subscribe({
      next: (addedShift)=>{
        console.log("Shift Added successfully: ", addedShift);
        this.router.navigate(['shifts']);
      }, 

      error:(error)=>{
        console.error("error adding shift: ", error);
      }});
  }
}
