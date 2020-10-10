import { NamedEntity } from "./named_entity.model";
import { KeyPhrase } from "./key_phrase.model";

export interface Phrase {
  id: string,
  text: string,
  sentiment: string,
  lenght: number,
  positive_score: number,
  neutral_score: number,
  negative_score: number,
  language: string,
  phrase_type: string,
  named_entities: NamedEntity[],
  key_phrases: KeyPhrase[]
}
