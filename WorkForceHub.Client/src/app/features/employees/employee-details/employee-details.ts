import { ChangeDetectorRef, Component, inject, OnInit } from '@angular/core';
import { EmployeeService } from '../employee';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { EmployeesDetailsModel } from '../models/employees-details.model';
import { DatePipe } from '@angular/common';
import { EmploymentStatus } from '../models/employmentStatus.enum';
import { PageTitleService } from '../../../core/services/PageTitleService';

@Component({
  imports: [DatePipe, RouterLink],
  selector: 'app-employee-details',
  styleUrl: './employee-details.css',
  templateUrl: './employee-details.html',
})
export class EmployeeDetails implements OnInit {

  employee: EmployeesDetailsModel |null = null;
  employmentStatus = EmploymentStatus;
  pageTitleService = inject(PageTitleService);

  constructor(private employeeService: EmployeeService, private activatedRoute:ActivatedRoute, private cdr: ChangeDetectorRef, private router: Router){}

  ngOnInit(): void {
    this.getEmployeesDetails();

    this.pageTitleService.setPageTitle("Employee Details", "View complete employee information");
  }
  
  getEmployeesDetails(){

    const id = Number(this.activatedRoute.snapshot.paramMap.get("id"));

    this.employeeService.getEmployeesDetails(id).subscribe({
      next:(data)=>{
        this.employee = data;
        console.log("Employee details:", this.employee);
        this.cdr.detectChanges();
      }, 
      error:(error)=>{
      console.error("error fetching employee:", error);
      }
    });
  }
  
  getEmploymentStatus(status: number): string{

    return this.employmentStatus[status] ?? 'unknown';
  }

  getEmployeeForUpdate(id: number): void {
  console.log('Navigating to edit employee:', id);

  this.router.navigate(['/employees', id, 'edit']);
}

}

