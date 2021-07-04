import { AttachmentDTO } from "./attachment";

export class MessageModel{
    text: any;
    dateCreated:any;
    chatId: any;
    CreatorName: any;
    messagesAttachments: AttachmentDTO[] = [];
}