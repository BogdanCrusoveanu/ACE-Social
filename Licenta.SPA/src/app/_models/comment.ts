export class Comment {
    id: number;
    content: string;
    postId?: number;
    userId?: number;
    createdAt: Date;
    userName: string;
    mainPhotoUrl: string;
}