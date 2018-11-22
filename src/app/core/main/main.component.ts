import { Component, OnInit } from '@angular/core';
import { TrelloService } from '../services/trello.service';
import { LuisService } from '../services/luis.service';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss']
})
export class MainComponent implements OnInit {

  public loggedIn = false;
  command = '';

  constructor(public trelloService: TrelloService, public luisService: LuisService) { }

  ngOnInit() {
  }

  login() {
    this.trelloService.login().then(res => this.loggedIn = true);
  }

  sendCommand() {
    this.luisService.sendCommand(this.command).subscribe(res => {
      console.log(res);
      this.command = '';
    })
  }

}
