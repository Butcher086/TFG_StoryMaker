import { Injectable } from "@angular/core";
import { HttpHeaders, HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { History } from "../models/history.model";
import { Phrase } from "../models/phrase.model";

@Injectable()
export class HistoriesService {

  url: string;  
  httpheaders: HttpHeaders;

  constructor(private http: HttpClient) {
    //this.url = "https://localhost:44378";
    this.url = "https://storymakerapi.azurewebsites.net";


    this.httpheaders = new HttpHeaders()
      .set('content-type', 'application/json')
      .set('Access-Control-Allow-Origin', '*');
  }

  //Recupera todas las historias
  getHistories(): Observable<any> {
    try {
      var result = this.http.get(this.url +'/api/Histories');

      return result;
    } catch (e) {

    }
  }

  //Crea una historia nueva
  addHistory(history: History): Observable<any> {
    try {
      const headers = { 'content-type': 'application/json' };
      const body = JSON.stringify(history);
      var result = this.http.post(this.url + '/api/Histories', body, { 'headers': headers });

      return result;
    } catch (e) {

    }
  }

  //Crea una historia nueva segun las frases tipo Phrase
  CreateHistory(phrases: Phrase[]): Observable<any> {
    try {
      const headers = { 'content-type': 'application/json' };
      const body = JSON.stringify(phrases);
      var result = this.http.post(this.url + '/api/Histories/CreateHistoryByPhrases', body, { 'headers': headers });

      return result;
    } catch (e) {

    }
  }

  //Crea una historia nueva segun las frases tipo texto
  AddHistory(phrases: string[]): Observable<any> {
    try {
      const headers = { 'content-type': 'application/json' };
      const body = JSON.stringify(phrases);
      var result = this.http.post(this.url + '/api/Histories/AddHistoryByPhrases', body, { 'headers': headers });

      return result;
    } catch (e) {

    }
  }

  //Recupera los textos de una historia
  GetHistoryText(history: History): Observable<any> {
    try {
      const headers = { 'content-type': 'application/json' };
      const body = JSON.stringify(history);
      var result = this.http.post(this.url + '/api/Histories/GetTextHistory', body, { 'headers': headers });

      return result;
    } catch (e) {

    }
  }

  //Borra una historia
  DeleteHistory(id: string): Observable<any> {
    try {
      const headers = { 'content-type': 'application/json' };
      const body = JSON.stringify(id);
      var result = this.http.delete(this.url + '/api/Histories/' + id);

      return result;
    } catch (e) {

    }
  }
}
