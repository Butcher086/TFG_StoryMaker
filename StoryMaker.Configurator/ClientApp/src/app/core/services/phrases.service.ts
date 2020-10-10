import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders, HttpParams } from "@angular/common/http";
import { Observable, throwError } from 'rxjs';
import { map, catchError } from 'rxjs/operators';

@Injectable()
export class PhrasesService {

  url: string;  
  httpheaders: HttpHeaders;

  constructor(private http: HttpClient) {
    //this.url = "https://localhost:44378";
    this.url = "https://storymakerapi.azurewebsites.net";


    this.httpheaders = new HttpHeaders()
      .set('content-type', 'application/json')
      .set('Access-Control-Allow-Origin', '*');
  }

  //Recupera todas las frases
  getPhrases(): Observable<any> {
    try {
      var result = this.http.get(this.url +'/api/Phrases');      

      return result;
    } catch (e) {

    }
  }

  //Crea una frase nueva con analisis
  addPhrase(phrase:string): Observable<any> {
    try {
      const headers = { 'content-type': 'application/json' };
      const body = JSON.stringify(phrase);
      var result = this.http.post(this.url + '/api/Phrases/AddAnalycedPhrase', body, { 'headers': headers });

      return result;
    } catch (e) {

    }
  }

  //Borra una frase
  deletePhrase(id: string): Observable<any> {
    try {
      const headers = { 'content-type': 'application/json' };
      const body = JSON.stringify(id);
      var result = this.http.delete(this.url + '/api/Phrases/' + id);

      return result;
    } catch (e) {

    }
  }
}
