import { Component, inject, OnInit } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { DepartmentService } from '../department';
import { Router, RouterLink } from '@angular/router';
import { CreateDepartmentModel } from '../models/create-department.model';
import { PageTitleService } from '../../../core/services/PageTitleService';

@Component({
  imports: [ReactiveFormsModule, RouterLink],
  selector: 'app-create-department',
  styleUrl: './create-department.css',
  templateUrl: './create-department.html',
})
export class CreateDepartment implements OnInit {

  private fb = inject(FormBuilder).nonNullable;
  

  pageService = inject(PageTitleService)
  constructor(private departmentService: DepartmentService, private router: Router){}


  departmentForm = this.fb.group({
    departmentName: ['', [Validators.required, Validators.minLength(2)]],
    description: ['', Validators.required]
  });


  ngOnInit(): void {
    this.pageService.setPageTitle("Create Department","Create your organization's department")
  }

  OnSubmit(){
    
    if(this.departmentForm.invalid){
      this.departmentForm.markAllAsTouched();
      return;
    }

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
