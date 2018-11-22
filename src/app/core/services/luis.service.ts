import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class LuisService {

  constructor(private httpClient: HttpClient) { }

  public sendCommand(command: string) {
    return this.httpClient.get('https://westus.api.cognitive.microsoft.com/luis/v2.0/apps/3fd4edf2-0f52-4eff-ad3a-f6ce7682d0ca?subscription-key=de0cb9a041d64cfca72293866fcac317&q=' + command)
  }
}
