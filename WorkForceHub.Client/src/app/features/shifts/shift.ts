import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Shift } from './models/shift.model';

@Injectable({
  providedIn: 'root',
})
export class ShiftService {
  http = inject(HttpClient);
  apiUrl: string = "https://localhost:7249/api/Shift";

  getShifts():Observable<Shift[]>{
    return this.http.get<Shift[]>(this.apiUrl);
  }
}
