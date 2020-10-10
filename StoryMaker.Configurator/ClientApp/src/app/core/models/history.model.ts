import { HistoryPhrase } from "./history_phrase.model";

export interface History {
  type: string,  
  phrases: HistoryPhrase[]  
}

export interface HistoryText {
  id: string,
  textPhrases: string[]  
}
