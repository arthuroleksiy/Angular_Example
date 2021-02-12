import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Message } from '@angular/compiler/src/i18n/i18n_ast';
import { EventEmitter, Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';


@Injectable({
  providedIn: 'root'
})
export class ChatService {
  messageReceived = new EventEmitter<Message>();
  connectionEstablished = new EventEmitter<Boolean>();

  private connectionIsEstablished = false;
  private _hubConnection: signalR.HubConnection;

  constructor(private http: HttpClient) {
    this.createConnection();
    this.registerOnServerEvents();
    this.startConnection();
  }

  sendMessage(message: Message) {
    this._hubConnection.invoke('NewMessage', message);
  }

  private createConnection() {

    this._hubConnection = new HubConnectionBuilder()
      .withUrl('https://localhost:44385/chatter')
      .build();
  }

  private startConnection(): void {
    this._hubConnection
      .start()
      .then(() => {
        this.connectionIsEstablished = true;
        console.log('Hub connection started');
        this.connectionEstablished.emit(true);
      })
      .catch(err => {
        console.log('Error while establishing connection, retrying...');
        setTimeout( () => { this.startConnection(); }, 5000);
      });
  }

  addMessage(message: any){
    return this.http.post('https://localhost:44385/api/Message', message);
  }

  private registerOnServerEvents(): void {
    this._hubConnection.on('getNewMessage', (data: any) => {
      this.messageReceived.emit(data);
    });
  }

  getAllMessages(courseId: number){
    return this.http.get('https://localhost:44385/api/Message?courseId=' + courseId);
  }
}
