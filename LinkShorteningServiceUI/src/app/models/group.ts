export class Group {
    id: number;
    name: string;
    redirectCount: number;
    linksCount: number;

    constructor(id: number, name: string, redirectCount: number, linksCount: number) {
        this.id = id;
        this.name = name;
        this.redirectCount = redirectCount;
        this.linksCount = linksCount;
    }
}