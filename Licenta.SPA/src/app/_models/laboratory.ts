export interface Laboratory {
    id: number;
    name: string;
    type: string;
    startDate: Date;
    endDate: Date;
    teacherId: number;
    teacherName?: string;
    subGroupId: number;
    subGroupName?: string;
    classId: number;
    className: string;
    semesterId: number;
}