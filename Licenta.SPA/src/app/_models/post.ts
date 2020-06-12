import { Comment } from './comment';
export class Post {
    id: number;
    content: string;
    createdAt: Date;
    userId?: number;
    userName: string;
    mainPhotoUrl: string;
    comments?: Comment[];
    showComments: boolean = false;
}