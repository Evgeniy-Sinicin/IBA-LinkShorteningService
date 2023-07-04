export class AddLinkRequest {
    url: string;
    groupId: number;
    name: string;

    constructor(url: string, name: string, groupId: number) {
        this.url = url;
        this.groupId = groupId;
        this.name = name;
    }
}