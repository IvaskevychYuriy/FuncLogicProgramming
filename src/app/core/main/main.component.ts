import { Component, OnInit, ElementRef, ViewChild, AfterViewInit } from '@angular/core';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';
import { TrelloService } from '../services/trello.service';
import { LuisService } from '../services/luis.service';
import { LuisSpeechService } from '../services/luis-speech.service';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss']
})
export class MainComponent implements OnInit, AfterViewInit {

  public loggedIn = false;
  command = '';

  @ViewChild('iframe') iframe: ElementRef;

  public url: SafeResourceUrl;

  constructor(public trelloService: TrelloService, public luisService: LuisService, public luisSpeechService: LuisSpeechService, public sanitizer: DomSanitizer) { }

  ngOnInit() {
  }

  ngAfterViewInit() {
    setTimeout(() => {
      console.log(this.iframe.nativeElement.contentWindow.location);
    }, 10000)
    
  }

  login() {
    this.trelloService.login().subscribe(res => {
      this.loggedIn = true;
      this.url = this.sanitizer.bypassSecurityTrustResourceUrl('https://trello.com/user39986765/boards');
    });
  }

  sendCommand() {
    debugger;
    // this.luisService.sendCommand(this.command).subscribe(res => {
    //   console.log(res);

    //   switch (res.topScoringIntent.intent) {
    //     case 'CreateBoard':
    //       {
    //       const maximum = Math.max(...res.entities.map(e => e.score));

    //       const lookup = res.entities.find(e => e.score >= maximum && e.type === 'EntityName');

    //         this.trelloService.addBoard(lookup.entity).subscribe(res => {
    //           console.log(res);
    //         })
    //       }
    //       break;
    //     case 'OpenBoard':
    //       {
    //         const maximum = Math.max(...res.entities.map(e => e.score));

    //         const lookup = res.entities.find(e => e.score >= maximum && e.type === 'EntityName');
    //         this.trelloService.openBoard(lookup.entity).subscribe(url => this.url = this.sanitizer.bypassSecurityTrustResourceUrl(url));
    //       }
    //       break;
    //     case 'CreateList':
    //       {
    //         const maximum = Math.max(...res.entities.map(e => e.score));

    //         const lookup = res.entities.find(e => e.score >= maximum && e.type === 'EntityName');

    //         this.trelloService.createList(lookup.entity).subscribe(res => {
    //           console.log(res);
    //         })
    //       }
    //       break;
    //     case 'CreateTask':
    //       {
    //         const listName = res.entities.find(e => e.role === 'ListName');
    //         const taskName = res.entities.find(e => e.role === 'TaskName');

    //         if (listName == null || taskName == null) {
    //           break;
    //         }

            

    //         this.trelloService.createTask(taskName.entity, listName.entity).subscribe();
    //       }
    //     default:
    //       break;
    //   }

    //   this.command = '';
    // })

    this.luisService.sendCommandToBackend(this.command).subscribe(res => {
      console.log(res);
      if (res && res.url) {
        this.url = this.sanitizer.bypassSecurityTrustResourceUrl(res.url);
      }
    })
  }

  record() {
    this.luisSpeechService.record().subscribe(command => {
      this.command = command;
      this.luisSpeechService.destroySpeechObject();
      this.sendCommand();
    });
  }

}
