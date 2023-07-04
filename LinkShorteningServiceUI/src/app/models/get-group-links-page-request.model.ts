import { PageRequest } from './page-request.model';

export class GetGroupLinksPageRequest extends PageRequest {
    groupId: number;

    constructor(page: number, size: number, groupId: number) {
        super(page, size);
        this.groupId = groupId;
    }
}