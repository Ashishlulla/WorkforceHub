import { ShiftType } from "./shiftType.enum";

export interface UpdateShiftModel{
    shiftId: number;
    shiftType: ShiftType;
    startTime: string;
    endTime: string;
    gracePeriodInMinutes: number;
    isOvernight: boolean ;
    isActive: boolean;
}