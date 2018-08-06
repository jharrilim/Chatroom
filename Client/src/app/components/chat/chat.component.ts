import { Component, OnInit, OnDestroy } from '@angular/core';
import { HubConnection, HubConnectionBuilder, LogLevel } from '@aspnet/signalr';
import * as signalR from '@aspnet/signalr';
import { Message } from '../../models/message';

@Component({
    selector: 'app-chat',
    templateUrl: './chat.component.html',
    styleUrls: ['./chat.component.css']
})
export class ChatComponent implements OnInit, OnDestroy {
    public  messages: Message[] = [];
    public  message: string = '';
    private hubConnection: HubConnection | undefined;

    constructor() { }

    ngOnInit() {
        this.hubConnection = new HubConnectionBuilder()
            .withUrl('/myHub')
            .configureLogging(LogLevel.Information)
            .build();

        this.hubConnection.start().catch(err => console.error(err.toString()));

        this.hubConnection.on('ReceiveMessage', (user: string, message: string) => {
            console.log(user + ' ' + message);
            const date: Date = new Date();
            const ampm: string = date.getHours() >= 12 ? 'PM' : 'AM';
            const hour = date.getHours() == 0 ? '12' : date.getHours() > 12 ? `${date.getHours() - 12}` : date.getHours().toString();
            const dateFormat: string = `${hour}:${date.getMinutes()} ${ampm}`;
            const received = new Message(user, message, dateFormat);
            this.messages.push(received);
        });
    }

    ngOnDestroy() {
        if (this.hubConnection)
            this.hubConnection.stop().catch(err => console.error(err.toString()));
    }

    sendMessage() {
        if (!this.hubConnection) {
            console.error('Not connected.');
        }
        this.hubConnection.invoke('SendMessage', 'Anonymous', this.message)
            .then(_ => {
                this.message = '';
            });
    }


}
