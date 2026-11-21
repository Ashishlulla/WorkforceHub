import { ShiftType } from "./shiftType.enum";

export interface Shift {

    shiftId: number;
    shiftType: ShiftType;
    startTime: string;
    endTime: string;
    graceperoidInMinutes: number;
    isOverNight: boolean;
    isActive: boolean;
    createdAt: string;
    updatedAt: string;

}
