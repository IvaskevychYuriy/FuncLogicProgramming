import { Component, OnInit } from '@angular/core';
import { TrelloService } from '../services/trello.service';
import { LuisService } from '../services/luis.service';
import { LuisSpeechService } from '../services/luis-speech.service';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss']
})
export class MainComponent implements OnInit {

  public loggedIn = false;
  command = '';

  constructor(public trelloService: TrelloService, public luisService: LuisService, public luisSpeechService: LuisSpeechService) { }

  ngOnInit() {
  }

  login() {
    this.trelloService.login().then(res => this.loggedIn = true);
  }

  sendCommand() {
    this.luisService.sendCommand(this.command).subscribe(res => {
      console.log(res);

      switch (res.topScoringIntent.intent) {
        case 'CreateBoard':
          const maximum = Math.max(...res.entities.map(e => e.score));

          const lookup = res.entities.find(e => e.score >= maximum && e.type === 'EntityName');

          this.trelloService.addBoard(lookup.entity).subscribe(res => {
            console.log(res);
          })
          break;
      
        default:
          break;
      }

      this.command = '';
    })
  }

  record() {
    this.luisSpeechService.record().subscribe(res => console.log(res));
  }

}
