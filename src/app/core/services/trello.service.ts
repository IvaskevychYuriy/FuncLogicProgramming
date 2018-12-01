import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, from } from 'rxjs';
import { switchMap, tap, map } from 'rxjs/operators';

const TRELLO_KEY = 'd8a87c275b5aeb935563ce13e64e2007';

@Injectable({
  providedIn: 'root'
})
export class TrelloService {

  constructor(private httpClient: HttpClient) { }

  private userId: string;

  public activeBoardId: string;

  public login(): Observable<any> {
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

    const token = localStorage.getItem('trello_token');

    return from(promise).pipe(
      switchMap(res => {
        return this.httpClient.get(`https://api.trello.com/1/tokens/${token}/member/?key=${TRELLO_KEY}&token=${token}`);
      }),
      tap(res => {
        this.userId = res.id;
      })
    )
  }

  public openBoard(name: string) {
    const token = localStorage.getItem('trello_token');
    return this.httpClient.get(`https://api.trello.com/1/members/${this.userId}/boards/?key=${TRELLO_KEY}&token=${token}`)
      .pipe(
        map((res: any[]) => {
          const index = res.map(b => b.name).findIndex(n => n.toUpperCase() === name.toUpperCase());
          this.activeBoardId = index > -1 ? res[index].id : null
          return index > -1 ? res[index].shortUrl : null;
        })
      );
  }

  public addBoard(name: string): Observable<any> {
    const token = localStorage.getItem('trello_token');
    return this.httpClient.post(`https://api.trello.com/1/boards/?name=${name}&key=${TRELLO_KEY}&token=${token}`, {});
  }

  public createList(name: string): Observable<any> {
    const token = localStorage.getItem('trello_token');
    return this.httpClient.post(`https://api.trello.com/1/lists/?name=${name}&idBoard=${this.activeBoardId}&key=${TRELLO_KEY}&token=${token}`, {});
  }

  public createTask(taskName: string, listName: string) {
    const token = localStorage.getItem('trello_token');
    return this.httpClient.get(`https://api.trello.com/1/boards/${this.activeBoardId}/lists?key=${TRELLO_KEY}&token=${token}`)
      .pipe(
        switchMap((res: any[]) => {
          const list = res.find(l => l.name.toUpperCase() === listName.toUpperCase());

          return this.httpClient.post(`https://api.trello.com/1/cards?name=${taskName}&idList=${list.id}&key=${TRELLO_KEY}&token=${token}`, {});
        })
      )
  }
}
