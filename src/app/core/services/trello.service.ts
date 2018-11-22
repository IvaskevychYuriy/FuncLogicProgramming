import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from '../../../../node_modules/rxjs';

const TRELLO_KEY = 'd8a87c275b5aeb935563ce13e64e2007';

@Injectable({
  providedIn: 'root'
})
export class TrelloService {

  constructor(private httpClient: HttpClient) { }

  public login(): Promise<any> {
    let w = window as any;
    let promise = new Promise((resolve, reject) => {
      let opts = {

        type: 'popup',
        name: 'App2',
        scope: {
          read: 'true',
          write: 'true'
        },
        expiration: 'never',
        success: resolve,
        error: reject
      }

      w.Trello.authorize(opts);
    })

    return promise;
  }

  public addBoard(name: string): Observable<any> {
    const token = localStorage.getItem('trello_token');
    return this.httpClient.post(`https://api.trello.com/1/boards/?name=${name}&key=${TRELLO_KEY}&token=${token}`, {});
  }
}
