import { Photo } from './photo';

export interface User {
    id: number;
    userName: string;
    firstName: string;
    lastName: string;
    age: number;
    year: number;
    specialization?: string;
    group?: string;
    subGroup?: string;
    created: Date;
    lastActive: Date;
    photoUrl: string;
    photos?: Photo[];
    roles?: string[];
}
