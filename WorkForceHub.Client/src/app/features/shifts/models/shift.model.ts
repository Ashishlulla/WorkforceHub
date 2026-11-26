import { ShiftType } from "./shiftType.enum";

export interface ShiftModel {
    shiftId: number;
    shiftType: ShiftType;
    startTime: string;
    endTime: string;
    gracePeriodInMinutes: number;
    isOvernight: boolean;
    isActive: boolean;
    createdAt: string;
    updatedAt: string | null;
}
