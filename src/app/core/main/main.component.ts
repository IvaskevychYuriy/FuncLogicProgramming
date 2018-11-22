import { Component, OnInit } from "@angular/core";
import { TrelloService } from "../services/trello.service";
import { LuisService } from "../services/luis.service";
import { DialogService } from "../services/dialog.service";

@Component({
  selector: "app-main",
  templateUrl: "./main.component.html",
  styleUrls: ["./main.component.scss"]
})
export class MainComponent implements OnInit {
  public loggedIn = false;
  command = "";

  constructor(
    public trelloService: TrelloService,
    public luisService: LuisService,
    public dialogService: DialogService
  ) {}

  ngOnInit() {}

  login() {
    this.trelloService.login().then(res => (this.loggedIn = true));
  }

  sendCommand() {
    this.dialogService.sendCommand(this.command).subscribe(res => {
      console.log(res);
      switch (res.result.action) {
        case "CreateBoard":
          const entityName = res.result.parameters["EntityName"] as string;

          this.trelloService.addBoard(entityName).subscribe(res => {
            console.log(res);
          });
          break;

        default:
          break;
      }
    });
    this.command = '';
  }
  //this.luisService.sendCommand(this.command).subscribe(res => {
  //  console.log(res);
  //
  //  switch (res.topScoringIntent.intent) {
  //    case 'CreateBoard':
  //      const maximum = Math.max(...res.entities.map(e => e.score));
  //
  //      const lookup = res.entities.find(e => e.score >= maximum && e.type === 'EntityName');
  //
  //      this.trelloService.addBoard(lookup.entity).subscribe(res => {
  //        console.log(res);
  //      })
  //      break;
  //
  //    default:
  //      break;
  //  }
  //
  //  this.command = '';
  //})
}
