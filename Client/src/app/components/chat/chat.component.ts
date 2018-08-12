import { Component, OnInit, OnDestroy, ViewChild, ElementRef } from '@angular/core';
import { HubConnection, HubConnectionBuilder, LogLevel } from '@aspnet/signalr';
import * as signalR from '@aspnet/signalr';
import { Message } from '../../models/message';
import { UserService } from '../../services/user.service';
import { environment } from '../../../environments/environment';
@Component({
    selector: 'app-chat',
    templateUrl: './chat.component.html',
    styleUrls: ['./chat.component.css']
})
export class ChatComponent implements OnInit, OnDestroy {
    public messages: Message[] = [];
    public message: string = '';
    private hubConnection: HubConnection | undefined;
    @ViewChild('msgBox')
    private messageBox: ElementRef;

    constructor(private userService: UserService) { }

    ngOnInit() {
        this.hubConnection = new HubConnectionBuilder()
            .withUrl(environment.url)
            .configureLogging(LogLevel.Information)
            .build();

        this.hubConnection.on('GetChatHistory', (messages: Message[]) => {
            console.log(messages);
            this.messages = messages;
        });

        this.hubConnection.on('SelfJoined', (id: string) => {
            console.log('Successfully joined the chat.');
            this.userService.setUser(id);
        });

        this.hubConnection.on('UserJoined', (id: string) => {
            console.log(`${id} has entered.`);
        });

        this.hubConnection.on('ReceiveMessage', (message: Message) => {
            this.messages.push(message);
        });

        this.hubConnection.start().catch(err => console.error(err.toString()));
    }

    ngOnDestroy() {
        if (this.hubConnection)
            this.hubConnection.stop().catch(err => console.error(err.toString()));
    }

    sendMessage() {
        if (!this.hubConnection) {
            console.error('Not connected.');
            return;
        }
        this.hubConnection.invoke('SendMessage', this.userService.getUser(), this.message);
        this.message = '';
        this.messageBox.nativeElement.focus();
    }

    private getLocalTime(): string {
        const date: Date = new Date();
        const ampm: string = date.getHours() >= 12 ? 'PM' : 'AM';
        const hour = date.getHours() == 0 ? '12' : date.getHours() > 12 ? `${date.getHours() - 12}` : date.getHours().toString();
        const dateFormat: string = `${hour}:${date.getMinutes()} ${ampm}`;
        return dateFormat;
    }
}
