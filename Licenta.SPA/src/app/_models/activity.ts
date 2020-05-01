export interface Activity {
    name: string;
    type: string;
    startDate: Date;
    endDate: Date;
    duration: number;
    teacher?: string;
    categoryName?: string;
    className: string;
    semesterId: number;
}