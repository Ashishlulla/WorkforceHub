export interface DepartmentDetailsModel {
    departmentId: number;
    departmentName: string;
    description: string;
    isActive: boolean;
    createdAt: string;
    updatedAt: string | null;
    employees: {
        employeeId: number;
        employeeCode: string;
        firstName: string;
        lastName: string;
        email: string;
        jobTitle: string;
        status: number;
    }[];
}