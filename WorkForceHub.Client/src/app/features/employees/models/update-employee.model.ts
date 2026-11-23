export interface UpdateEmployeeModel{

    employeeId: number;
    employeeCode: string;

    firstName: string;
    lastName: string;
    email: string;
    phone: string;
    dateOfBirth: string;
    gender: number;

    jobTitle: string;
    departmentId: number;
    shiftId: number;

    dateOfJoining: string;

    employmentType: number;
    status: number;

    address: string;
    city: string;
    state: string;
    postalCode: string;
    
}