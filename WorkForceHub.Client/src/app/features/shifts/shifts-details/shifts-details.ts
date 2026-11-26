import { ChangeDetectorRef, Component, NgModule, OnInit } from '@angular/core';
import { ShiftService } from '../shift';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { ShiftDetailsModel } from '../models/shift-details.model';
import { PageTitleService } from '../../../core/services/PageTitleService';
import { ShiftType } from '../models/shiftType.enum';
import { CommonModule, DatePipe } from '@angular/common';


@Component({
  imports: [ DatePipe, CommonModule, RouterLink],
  selector: 'app-shifts-details',
  styleUrl: './shifts-details.css',
  templateUrl: './shifts-details.html',
})
export class ShiftsDetails implements OnInit{

  shift: ShiftDetailsModel | null = null;
  constructor(private shiftService: ShiftService, private acticvatedRoute: ActivatedRoute, private pageTitleService: PageTitleService, private cdr: ChangeDetectorRef){}

  ngOnInit(): void {
    
    this.pageTitleService.setPageTitle("Shift Details", "View complete shift information and schedule");

    this.getShiftDetails();
  }

  getShiftDetails() {

    const id = Number(this.acticvatedRoute.snapshot.paramMap.get('id'));

    this.shiftService.getShiftDetails(id).subscribe({
    next:(data)=>{
      this.shift = data;
      this.cdr.detectChanges();
      console.log("Shift details", data);
    }, 
    error:(error)=>{console.error("error fetching shift: ", error);}})
  }

  getShiftName(shiftType: ShiftType):string{
    return ShiftType[shiftType];
  }
}
