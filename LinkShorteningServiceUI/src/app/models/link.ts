export class Link {
    name: string;
    url: string;
    id: string;
    clickCount: number;

    constructor(name: string, url: string, shortUrl: string, redirectCount: number) {
        this.name = name;
        this.url = url;
        this.id = shortUrl;
        this.clickCount = redirectCount;
    }
}