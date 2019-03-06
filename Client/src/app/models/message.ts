export class Message {

    constructor(
        public user: string,
        public content: string,
        public type: string = 'user',
        public unixTime?: number
    ) { }
}
