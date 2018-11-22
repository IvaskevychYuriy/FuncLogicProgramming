import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { DialogResponse } from '../models/dialog-response.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DialogService {

  constructor(private httpClient: HttpClient) { }

  public sendCommand(command: string): Observable<DialogResponse> {
      return this.httpClient.get<DialogResponse>('https://cors-anywhere.herokuapp.com/https://api.dialogflow.com/v1/query', {
          params: {
              'v': '20170712',
              'sessionId': '1234567890',
              'query': command,
              'lang': 'en'
          },
          headers: {
              'Authorization': 'Bearer afc6a500958b409c8cfc25d90264c94c'
          }
      });
  }
}
