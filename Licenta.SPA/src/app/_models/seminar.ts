export interface Seminar {
    id: number;
    name: string;
    type: string;
    startDate: Date;
    endDate: Date;
    teacherId: number;
    teacherName?: string;
    groupId: number;
    groupName?: string;
    classId: number;
    className?: string;
    semesterId: number;
    courseId: number;
    courseName?: string;
}