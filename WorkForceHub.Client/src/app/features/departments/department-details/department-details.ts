import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { DepartmentService } from '../department';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { PageTitleService } from '../../../core/services/PageTitleService';
import { DepartmentDetailsModel } from '../models/department-details.model';
import { DatePipe } from '@angular/common';

@Component({
  imports: [DatePipe, RouterLink],
  selector: 'app-department-details',
  styleUrl: './department-details.css',
  templateUrl: './department-details.html',
})
export class DepartmentDetails implements OnInit {

  department: DepartmentDetailsModel | null = null;


  constructor(private departmentService: DepartmentService, private activatedRoute: ActivatedRoute, private pageServiceTitle: PageTitleService, private cdr: ChangeDetectorRef, private router: Router){}

  ngOnInit(): void {
    this.getDepartmentDetails();

    this.pageServiceTitle.setPageTitle("Department Details", "Department overview and associated employees");
  }

  getDepartmentDetails(){
    const id = Number(this.activatedRoute.snapshot.paramMap.get("id"));

    this.departmentService.getDepartmentDetails(id).subscribe({
      next:(data)=>{
        this.department = data;
        this.cdr.detectChanges();
        console.log("Department Data: ", data)
      }, 
      error:(error)=>{
        console.error("error feth department details: ", error);
      }});
  }

  viewEmployeeDetails(id: number){
    this.router.navigate(['/employees',id]);
  }

  

}
