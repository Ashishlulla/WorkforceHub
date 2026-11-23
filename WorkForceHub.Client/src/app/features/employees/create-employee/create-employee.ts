import { ChangeDetectorRef, Component, inject, OnInit, } from '@angular/core';
import { AbstractControl, FormBuilder, ReactiveFormsModule, ValidationErrors, ValidatorFn, Validators} from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { DepartmentService } from '../../departments/department';
import { ShiftService } from '../../shifts/shift';
import { Department } from '../../departments/models/department.model';
import { Shift } from '../../shifts/models/shift.model';
import { ShiftType } from '../../shifts/models/shiftType.enum';
import { EmployeeService } from '../employee';
import { CreateEmployeeModel } from '../models/create-employee.model';
import { UpdateEmployeeModel } from '../models/update-employee.model';
import { PageTitleService } from '../../../core/services/PageTitleService';


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
   pageTitleService = inject(PageTitleService);

   departments: Department[] = [];
   shifts: Shift[] = [];
   employeeId: number | null = null;
   isEditMode: boolean = false;

   constructor(private employeeService: EmployeeService , private departmentSerice:DepartmentService, private shiftService: ShiftService, private cdr: ChangeDetectorRef, private route: Router, private activatedRoute: ActivatedRoute){}

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

      this.pageTitleService.setPageTitle("Create Employee", "Add a new employee to your organization");

      this.activatedRoute.paramMap.subscribe(params=>{

        const id= Number(params.get('id'));

        if (id) {
          this.employeeId = id;
          this.isEditMode = true;

          console.log("Edit Employee ID:", id);

          this.pageTitleService.setPageTitle("Edit Employee", "Update employees information");

          this.getEmployeForUpdate();
        }
      });

    }

    getDepartments(){
      this.departmentSerice.getDepartments().subscribe({
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

    getEmployeForUpdate(): void{

      this.employeeService.getEmployeeForUpdate(this.employeeId).subscribe({
        next:(employee)=>{
          this.employeeForm.patchValue(employee);
        }, 
        error:(error)=>{console.error("error fetching employee")}});
    }

    updateEmployee(id: number, employee:UpdateEmployeeModel): void{

      this.employeeService.updateEmployee(id, employee)
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
      
      if(this.isEditMode ){
        const employeeData: UpdateEmployeeModel = {
          employeeId:this.employeeId!, 
          ...this.employeeForm.getRawValue()
        };  

        this.employeeService.updateEmployee(this.employeeId, employeeData).subscribe({
          next:(updatedEmployee)=>{
            console.log("Employee updated successfully:", updatedEmployee);
            this.isEditMode = false;
           this.route.navigate(['/employees']);
          }, 
          error:(error)=>{console.error("error updating employee:", error)}});

          

      }else{

        const employeeData: CreateEmployeeModel = this.employeeForm.getRawValue();

        console.log("Employee Data: ", this.employeeForm.value);

        this.employeeService.addEmployee(employeeData).subscribe({
              next: (addedEmployee) => {
                console.log('Employee added successfully:', addedEmployee);
                this.route.navigate(['/employees']);
              },
              error: (error) => {
                console.error('Error adding employee:', error);
              }
          });
        }
    }

    deleteEmployee(): void{
      this.employeeService.deleteEmployee(this.employeeId).subscribe({
        next:()=>{
          console.log("Employee deleted successfully.");
          this.route.navigate(['/employees']);
        }, 
        error:(error)=>{console.log("error deleting employee", error)}});
    }
  
}
