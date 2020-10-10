import { Component, OnInit, OnChanges } from '@angular/core';
import { Phrase } from '../core/models/phrase.model';
import { History, HistoryText } from '../core/models/history.model';
import { PhrasesService } from '../core/services/phrases.service';
import { HistoriesService } from '../core/services/histories.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls:['./home.component.scss']
})
export class HomeComponent implements OnInit, OnChanges{
  ngOnChanges(changes: import("@angular/core").SimpleChanges): void {
    this.refrehPhrases();
    this.refrehHistories();
    }  

  phrases: Phrase[] = [];
  histories: History[] = [];  
  selectedPhrase: Phrase;
  selectedPhraseAdded: Phrase;
  selectedHistory: HistoryText;
  phrasesSelected: Phrase[] = [];
  historyText: HistoryText[] = [];
  newPhrase: string;  

  loading: boolean = false;
  errorMessage;

  dropdownList = [];
  selectedItems = [];
  dropdownSettings = {};

  constructor(private phrasesService: PhrasesService, private historiesService: HistoriesService) {
    
  }

  ngOnInit(): void {        

    this.getPhrases();
    this.getHistories();
   
  }

  onSelect(phrase: Phrase): void {
    this.selectedPhrase = phrase;    
  }

  onSelectSelected(phrase: Phrase): void {
    this.selectedPhraseAdded = phrase;
  }

  clearPhraseAdded(): void {
    const index: number = this.phrasesSelected.indexOf(this.selectedPhraseAdded);
    if (index !== -1) {
      this.phrasesSelected.splice(index, 1);
    }     
  }

  onSelectHistory(history: HistoryText): void {
    this.selectedHistory = history;
  }

  onItemSelect(item: any) {
    console.log(item);
  }
  onSelectAll(items: any) {
    console.log(items);
  }

  //*******Phrase*******
  getPhrases() {
    this.phrasesService.getPhrases().subscribe((response) => { //next() callback
      console.log('response received')
      this.phrases = response;
    },
      (error) => {                              //error() callback
        console.error('Request failed with error')
        this.errorMessage = error;
        this.loading = false;
      },
      () => {                                   //complete() callback
        console.error('Request completed')      //This is actually not needed 
        this.loading = false;
      })
  }

  addPhrase() {
    if (this.newPhrase) {
      this.phrasesService.addPhrase(this.newPhrase).subscribe((response) => {
        console.log('response received')
        this.refrehPhrases();
        this.newPhrase = "";
      },
        (error) => {                              //error() callback
          console.error('Request failed with error')
          this.errorMessage = error;
          this.loading = false;
        },
        () => {                                   //complete() callback
          console.error('Request completed')      //This is actually not needed 
          this.loading = false;
        });
    }
  }

  deletePhrase() {
    if (this.selectedPhrase) {
      this.phrasesService.deletePhrase(this.selectedPhrase.id).subscribe((response) => {
        console.log('response received')
        this.refrehPhrases();
        this.newPhrase = "";
      },
        (error) => {                              //error() callback
          console.error('Request failed with error')
          this.errorMessage = error;
          this.loading = false;
        },
        () => {                                   //complete() callback
          console.error('Request completed')      //This is actually not needed 
          this.loading = false;
        });
    }
  }

  refrehPhrases() {
    this.phrases = [];
    this.getPhrases();
    this.newPhrase = "";
  }


  //*******History********
  getHistories() {
    this.historiesService.getHistories().subscribe((response) => { //next() callback
      console.log('response received')
      this.histories = response;      
      for (var history of this.histories) {
        this.historiesService.GetHistoryText(history).subscribe((response) => {
          this.historyText.push(response);
        });
      }
    },
      (error) => {                              //error() callback
        console.error('Request failed with error')
        this.errorMessage = error;
        this.loading = false;
      },
      () => {                                   //complete() callback
        console.error('Request completed')      //This is actually not needed 
        this.loading = false;
      })
  }
  addPhraseToHistory() {
    if (this.selectedPhrase != null) {
      this.phrasesSelected.push(this.selectedPhrase);
    }
    
  }

  createHistory() {
    if (this.phrasesSelected.length > 0) {
      this.historiesService.CreateHistory(this.phrasesSelected).subscribe((response) => {
        console.log('response received')        
        this.phrasesSelected = [];        
        this.refrehHistories();
      },
        (error) => {                              //error() callback
          console.error('Request failed with error')
          this.errorMessage = error;
          this.loading = false;
        },
        () => {                                   //complete() callback
          console.error('Request completed')      //This is actually not needed 
          this.loading = false;
        });      
    }
  }

  refrehHistories() {
    this.historyText = [];
    this.selectedHistory = null; 
    this.getHistories();
  }

  deleteHistory() {
    if (this.selectedHistory) {
      this.historiesService.DeleteHistory(this.selectedHistory.id).subscribe((response) => {
        console.log('response received')
        this.refrehHistories();        
      },
        (error) => {                              //error() callback
          console.error('Request failed with error')
          this.errorMessage = error;
          this.loading = false;
        },
        () => {                                   //complete() callback
          console.error('Request completed')      //This is actually not needed 
          this.loading = false;
        });
    }
  }
  
}
