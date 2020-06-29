import { Photo } from './photo';
import { Like } from './like';

export interface User {
    id: number;
    userName: string;
    firstName: string;
    lastName: string;
    fullName?: string;
    groupName?: string; 
    isFriend?: number;
    age: number;
    year: number;
    interests: string;
    description: string;
    specialization?: string;
    distance?: number;
    group?: string;
    subGroup?: string;
    created: Date;
    lastActive: Date;
    photoUrl: string;
    photos?: Photo[];
    roles?: string[];
    friends?: Like[];
}
