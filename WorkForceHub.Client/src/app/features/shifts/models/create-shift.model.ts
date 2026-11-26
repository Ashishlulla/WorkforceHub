import { ShiftType } from "./shiftType.enum";

export interface CreateShiftModel{

    shiftType: ShiftType;
    startTime: string;
    endTime: string;
    gracePeriodInMinutes: number;
    isOvernight: boolean;
    isActive: boolean;
}