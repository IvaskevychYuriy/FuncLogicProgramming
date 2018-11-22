import { Injectable } from '@angular/core';
import { Observable, from } from 'rxjs';
import { ApiAiClient } from "api-ai-javascript";
import { IServerResponse } from 'api-ai-javascript/ts/Interfaces';

@Injectable({
  providedIn: 'root'
})
export class DialogService {
  private readonly client = new ApiAiClient({
    accessToken: 'afc6a500958b409c8cfc25d90264c94c'
  })

  public sendCommand(command: string): Observable<IServerResponse> {
      const promise = this.client.textRequest(command);
      return from(promise);
  }
}
