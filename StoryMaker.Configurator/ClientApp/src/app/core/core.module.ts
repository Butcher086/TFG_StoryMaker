// imports
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { PhrasesService } from './services/phrases.service';
import { HistoriesService } from './services/histories.service';

// @NgModule decorator with its metadata
@NgModule({
  declarations: [],
  imports: [BrowserModule],
  providers: [PhrasesService, HistoriesService],
  bootstrap: []
})
export class CoreModule {

  constructor() {

  }
}
