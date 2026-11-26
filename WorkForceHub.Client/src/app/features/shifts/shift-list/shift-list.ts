import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { ShiftService } from '../shift';
import { Router } from '@angular/router';
import { ShiftModel } from '../models/shift.model';
import { PageTitleService } from '../../../core/services/PageTitleService';
import { CommonModule } from '@angular/common';
import { ShiftType } from '../models/shiftType.enum';
import { FormsModule } from '@angular/forms';

@Component({
  imports: [CommonModule, FormsModule],
  selector: 'app-shift-list',
  styleUrl: './shift-list.css',
  templateUrl: './shift-list.html',
})
export class ShiftList implements OnInit {

  shifts: ShiftModel[]=[];


  searchText: string ='';
  errorMessage: string = '';
  successMessage: string = '';

  constructor(private shiftService: ShiftService, private cdr: ChangeDetectorRef, private pageTitleService: PageTitleService , private router: Router){}

  ngOnInit(): void {

    this.pageTitleService.setPageTitle("Shifts", "manage and view shifts");
    this.getShifts();
  }

  getShifts(): void{

    this.shiftService.getShifts().subscribe({
      next:(data)=>{
        this.shifts = data;
        this.cdr.detectChanges();
        console.log("Shift Values: ", data);
      }, 
      error:(error)=>{
        console.error("error fetching shifts:", error);
      }});
  }

  getShiftName(shiftType: ShiftType): string{

    return ShiftType[shiftType];
  }

  get FilteredShifts(): ShiftModel[]{

    const search = this.searchText.toLowerCase().trim();

    return this.shifts.filter(shift=>{

      const shiftName = ShiftType[shift.shiftType].toLowerCase();

      return shiftName.includes(search) || 
      shift.startTime.includes(search) ||
      shift.endTime.includes(search);
    });
  }

  navigateToShiftDetails(id: number){
    return this.router.navigate(['/shifts', id]);
  }

  createShift(){
    return this.router.navigate(['/shifts/create-shift']);
  }

  updsteShift(id: number){
    return this.router.navigate(['/shifts/edit', id]);
  }

  deleteShift(id:number): void{
    this.shiftService.deleteShift(id).subscribe({
      next:()=>{
        console.log("shift deleyed successfully...");
        this.cdr.detectChanges();
        this.router.navigate(["/shifts"]);
      }, 
      error:(error)=>{
        console.error("error deleting shift: ", error)
      }});
  }
}
