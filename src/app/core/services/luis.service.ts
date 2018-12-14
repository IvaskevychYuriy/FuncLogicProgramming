import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { LuisResponseModel } from '../models/luis-response.model';
import { Observable } from '../../../../node_modules/rxjs';
import { TrelloService } from './trello.service';

@Injectable({
  providedIn: 'root'
})
export class LuisService {

  constructor(private httpClient: HttpClient, public trelloService: TrelloService) { }

  public sendCommand(command: string): Observable<LuisResponseModel> {
    return this.httpClient.get<LuisResponseModel>('https://westus.api.cognitive.microsoft.com/luis/v2.0/apps/3fd4edf2-0f52-4eff-ad3a-f6ce7682d0ca?subscription-key=de0cb9a041d64cfca72293866fcac317&q=' + command)
  }

  public sendCommandToBackend(command: string) {
    
    let headers = new HttpHeaders({
      'TRELLO_USERID': this.trelloService.userId,
      'TRELLO_TOKEN': localStorage.getItem('trello_token')
    });

    return this.httpClient.post<any>('https://localhost:5001/api/query/performAction', {query: command}, {
      headers: headers
    })
  }
}
