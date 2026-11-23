import { ChangeDetectorRef, Component, inject, OnInit } from '@angular/core';
import { DepartmentService } from '../department';
import { Department } from '../models/department.model';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { PageTitleService } from '../../../core/services/PageTitleService';

@Component({
  imports: [FormsModule, RouterLink],
  selector: 'app-department-list',
  styleUrl: './department-list.css',
  templateUrl: './department-list.html',
})
export class DepartmentList implements OnInit {

  departments: Department[] = [];
  searchText: string = '';
  pageTitleService = inject(PageTitleService);


  constructor(private departmentService: DepartmentService, private cdr: ChangeDetectorRef){}

  ngOnInit(): void {
    this.getDepartments();
    
    this.pageTitleService.setPageTitle("Departments", "Manage your organization's departments");
  }
  

  get FilteredDepartment() : Department[]{
    const search = this.searchText.toLowerCase().trim();

    return this.departments.filter(department=>
      department.departmentName.toLowerCase().includes(search) || department.description.toLowerCase().includes(search)
    );

  }



  getDepartments(): void{

    this.departmentService.getDepartments().subscribe({
      next:(data)=>{
        this.departments = data;
        this.cdr.detectChanges();
        console.log("Department Value: ", this.departments);
      }, 
      error:(error)=>{console.error("error fetching Departments: ", error)}});
  
    }
}
