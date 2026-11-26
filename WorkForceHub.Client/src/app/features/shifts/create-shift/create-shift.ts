import { ChangeDetectorRef, Component, inject, OnInit } from '@angular/core';
import { ShiftService } from '../shift';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { AbstractControl, FormBuilder, ReactiveFormsModule, ValidatorFn, Validators } from '@angular/forms';
import { PageTitleService } from '../../../core/services/PageTitleService';
import { ShiftModel } from '../models/shift.model';
import { CreateShiftModel } from '../models/create-shift.model';
import { ShiftType } from '../models/shiftType.enum';
import { CommonModule } from '@angular/common';
import { UpdateShiftModel } from '../models/update-shift.model';
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
  imports: [ReactiveFormsModule,CommonModule, RouterLink],
  selector: 'app-create-shift',
  styleUrl: './create-shift.css',
  templateUrl: './create-shift.html',
})
export class CreateShift implements OnInit {

  private fb = inject(FormBuilder);
  private shiftId: number | null= null;
  private isEditMode: boolean = false;

  constructor( private shiftService: ShiftService, private pageTitleService: PageTitleService , private router: Router, private cdr: ChangeDetectorRef, private activatedRoute: ActivatedRoute ){}

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

     this.activatedRoute.paramMap.subscribe(params=>{
      const id = Number(params.get('id'));

      if (id) {
        this.shiftId = id;
        this.isEditMode = true;

        this.pageTitleService.setPageTitle("Edit Shift", "pdate shift information");

        this.grtShiftForUpdate()
      }
     });
  }

  grtShiftForUpdate(): void{

    this.shiftService.getShiftForUpdate(this.shiftId).subscribe({
      next:(data)=>{
        this.shiftForm.patchValue(data);
      }, 
      error:(error)=>{
        console.error("error fetching shoft for update: ", error);
      }});
  }

  onSubmit(){
    
    if(this.shiftForm.invalid){
      this.shiftForm.markAllAsTouched();
      return;
    }

    if(this.isEditMode){

      const shiftData: UpdateShiftModel = 
      {
        shiftId: this.shiftId!,
        shiftType: this.shiftForm.controls.shiftType.value!,
        startTime: this.shiftForm.controls.startTime.value!,
        endTime: this.shiftForm.controls.endTime.value!,
        gracePeriodInMinutes:this.shiftForm.controls.gracePeriodInMinutes.value!,
        isActive: this.shiftForm.controls.isActive.value ?? true,
        isOvernight: this.shiftForm.controls.isOvernight.value ?? false,
      };

      this.shiftService.updateShift(this.shiftId!, shiftData).subscribe(
        {next:(updatedShift)=>{
          console.log("Shift updatd successfully: ", updatedShift);
          this.router.navigate(['/shifts']);
        }, 
        error: (error)=>{
          console.error("error updating shift: ", error);
        }});

    }else{

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
}
