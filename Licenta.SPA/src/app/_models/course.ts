export interface Course {
    id: number;
    name: string;
    type: string;
    startDate: Date;
    endDate: Date;
    teacherId: number;
    teacherName?: string;
    specializationId: number;
    specializationName?: string;
    classId: number;
    className: string;
    semesterId: number;
}