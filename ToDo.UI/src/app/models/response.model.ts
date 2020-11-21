export class MResponse<T> {
    Meta: Meta;
    Entities: any[T];
    Entity?: T;
}

export class Meta {
    IsSuccess: boolean;
    Message: string;
    MessageDetail: string;
}