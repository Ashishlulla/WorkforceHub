import { Component, inject, OnInit } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { DepartmentService } from '../department';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { CreateDepartmentModel } from '../models/create-department.model';
import { PageTitleService } from '../../../core/services/PageTitleService';
import { UpdateDepartmentModel } from '../models/update-department.model';

@Component({
  imports: [ReactiveFormsModule, RouterLink],
  selector: 'app-create-department',
  styleUrl: './create-department.css',
  templateUrl: './create-department.html',
})
export class CreateDepartment implements OnInit {

  private fb = inject(FormBuilder).nonNullable;
  isEditMode: boolean = false;
  departmentId: number = 0;


  pageService = inject(PageTitleService)
  constructor(private departmentService: DepartmentService, private router: Router, private activatedRoute: ActivatedRoute){}


  departmentForm = this.fb.group({
    departmentName: ['', [Validators.required, Validators.minLength(2)]],
    description: ['', Validators.required],
    isActive: [true,[Validators.required]]
  });


  ngOnInit(): void {
    this.pageService.setPageTitle("Create Department","Create your organization's department");

    this.activatedRoute.paramMap.subscribe(params=>{
      const id = Number(params.get('id'));

      if (id) {
        this.departmentId = id;
        this.isEditMode = true;

        console.log('Department ID:', this.departmentId);

        this.pageService.setPageTitle("Edit Department", "update department information");

        this.getDepartForUpdate();
        
      }
    });
  }

  OnSubmit(){
    
    if(this.departmentForm.invalid){
      this.departmentForm.markAllAsTouched();
      return;
    }

    if (this.isEditMode == true) {
        
       const  updateDepartmentData: UpdateDepartmentModel = {
        departmentId: this.departmentId,
        ...this.departmentForm.getRawValue()
       };

       this.departmentService.updateDepartment(this.departmentId, updateDepartmentData).subscribe({
        next:(UpdatedDepartment)=>{
          
          console.log("department updated successfully.",UpdatedDepartment);
          this.router.navigate(['/departments']);},

        error:(error)=>{console.error("error updating department: ", error)}});

    }else{

      const  departmentData: CreateDepartmentModel = this.departmentForm.getRawValue();

      this.departmentService.createDepartment(departmentData).subscribe({
        next:(addedDeparment)=>{
          console.log("Department added successfully", addedDeparment);
          this.router.navigate(['/departments']);
        }, 
        error:(error)=>{
          console.error("error adding department", error);
        }});
    } 
  }

  getDepartForUpdate(): void{

    this.departmentService.getDepartmentForUpdate(this.departmentId).subscribe({
      next:(data)=>{
       this.departmentForm.patchValue(data);
      }, 
      error:(error)=>{
        console.error("error fetching department:", error);
      }});
  }
}
