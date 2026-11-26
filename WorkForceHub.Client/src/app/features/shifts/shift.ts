import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ShiftModel } from './models/shift.model';
import { ShiftDetailsModel } from './models/shift-details.model';
import { CreateShiftModel } from './models/create-shift.model';
import { UpdateShiftModel } from './models/update-shift.model';

@Injectable({
  providedIn: 'root',
})
export class ShiftService {
  private http = inject(HttpClient);
  private apiUrl: string = "https://localhost:7249/api/Shift";

  getShifts():Observable<ShiftModel[]>{
    return this.http.get<ShiftModel[]>(this.apiUrl);
  }

  getShiftDetails(id:number):Observable<ShiftDetailsModel>{
    return this.http.get<ShiftDetailsModel>(`${this.apiUrl}/details/${id}`);
  }

  getShiftForUpdate(id:number |null): Observable<UpdateShiftModel >{
    return this.http.get<UpdateShiftModel>(`${this.apiUrl}/edit/${id}`);
  }

  createShift(shift: CreateShiftModel): Observable<ShiftModel>{
    return this.http.post<ShiftModel>(this.apiUrl, shift);
  }

  updateShift(id: number, shift: UpdateShiftModel):Observable<ShiftModel>{
    return this.http.put<ShiftModel>(`${this.apiUrl}/edit/${id}`,shift);
  }

  deleteShift(id: number): Observable<void>{
    return this.http.delete<void>(`${this.apiUrl}/delete/${id}`);
  }
}
