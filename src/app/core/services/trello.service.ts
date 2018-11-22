import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';


@Injectable({
  providedIn: 'root'
})
export class TrelloService {

  constructor(private httpClient: HttpClient) { }

  public login() {
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
}
