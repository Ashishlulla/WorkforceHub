import { ChangeDetectorRef, Component, inject, OnInit, } from '@angular/core';
import { AbstractControl, FormBuilder, ReactiveFormsModule, ValidationErrors, ValidatorFn, Validators} from '@angular/forms';
import { RouterLink } from '@angular/router';
import { DepartmentService } from '../../departments/department';
import { ShiftService } from '../../shifts/shift';
import { Department } from '../../departments/models/department.model';
import { Shift } from '../../shifts/models/shift.model';
import { ShiftType } from '../../shifts/models/shiftType.enum';
import { EmployeeService } from '../employee';
import { CreateEmployeeModel } from '../models/create-employee.model';


const joiningDateValidators: ValidatorFn=(control: AbstractControl):ValidationErrors |null=>{

  const dateOfBirth = control.get('dateOfBirth')?.value;
  const dateOfJoining = control.get('dateOfJoining')?.value;

  if(dateOfBirth == null || dateOfJoining== null){

    return null;
  }

  return dateOfJoining>= dateOfBirth? null: {invalidJoiningDate: true}; 
};


@Component({
  imports: [ReactiveFormsModule, RouterLink],
  selector: 'app-create-employee',
  styleUrl: './create-employee.css',
  templateUrl: './create-employee.html',
})
export class CreateEmployee implements OnInit{

  ShiftType = ShiftType; 
  
  private fb = inject(FormBuilder).nonNullable;

   departments: Department[] = [];
   shifts: Shift[] = [];

   constructor(private employeeService: EmployeeService , private departmentSerice:DepartmentService, private shiftService: ShiftService, private cdr: ChangeDetectorRef){}

    employeeForm = this.fb.group({
      employeeCode: ['', Validators.required],

      firstName: ['', [Validators.required, Validators.maxLength(20)]],
      lastName: ['', [Validators.required, Validators.maxLength(20)]],
      email: ['', [Validators.required, Validators.email]],
      phone: ['', [Validators.required, Validators.pattern(/^[0-9]{10}$/)]],
      dateOfBirth: ['', [Validators.required, this.isPastDate]],
      gender: [0, [Validators.required, Validators.min(1)]],

      jobTitle: ['', Validators.required],
      departmentId: [0, [Validators.required, Validators.min(1)]],
      shiftId: [0, [Validators.required, Validators.min(1)]],

      dateOfJoining: ['', Validators.required],

      employmentType: [1, Validators.required],
      status: [1, Validators.required],

      address: ['', Validators.required],
      city: ['', Validators.required],
      state: ['', Validators.required],
     
      postalCode: ['', [Validators.required, Validators.pattern(/^[0-9]{6}$/)]],

    }, {validators: joiningDateValidators});

    ngOnInit(): void {
      this.getDepartments();
      this.getShifts();
    }

    getDepartments(){
      this.departmentSerice.geDepartments().subscribe({
        next:(data)=>{
          this.departments = data;
          this.cdr.detectChanges();
          console.log("Departments Values:", this.departments);
        }, 
        error:(error)=>{
          console.error("Error fetching departments:", error);
        }});
    }

    getShifts(){
      this.shiftService.getShifts().subscribe({
        next:(data)=>{
          this.shifts = data;
          this.cdr.detectChanges();
          console.log("Shifts Values:", this.shifts);
        }, 
        error:(error)=>{
          console.error("Error fetching shifts:", error);
        }});
    }

    

  isPastDate(control: any) {
      if (!control.value) {
        return null;
      }

      const selectedDate = new Date(control.value);
      const today = new Date();

      today.setHours(0, 0, 0, 0);

      return selectedDate < today
        ? null
        : { futureDate: true };
    }

    isJoiningDateValid():any {
      const dateOfBirth = this.employeeForm?.controls.dateOfBirth.value;
      const dateOfJoining = this.employeeForm?.controls.dateOfJoining.value;

      if (!dateOfBirth || !dateOfJoining) {
        return null;
      }

      return dateOfJoining >= dateOfBirth
        ? null
        : { invalidJoiningDate: true };
    }

    OnSubmit(){
      if(this.employeeForm.invalid){
       
        this.employeeForm.markAllAsTouched();
        return;
      }
      
      const employeeData: CreateEmployeeModel = this.employeeForm.getRawValue();

      console.log("Employee Data: ", this.employeeForm.value);

     this.employeeService.addEmployee(employeeData).subscribe({
          next: (addedEmployee) => {
            console.log('Employee added successfully:', addedEmployee);
          },
          error: (error) => {
            console.error('Error adding employee:', error);
          }
      });
    }
}
