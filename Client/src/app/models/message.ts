export class Message {

    constructor(
        public user: string,
        public content: string,
        public userType: string = 'user',
        public localDate?: string
    ) {

    }
}
