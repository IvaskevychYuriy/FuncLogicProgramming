import { EntityModel } from './entity.model';

export class LuisResponseModel {
  query: string;

  topScoringIntent: {
    intent: string,
    score: number
  };

  entities: EntityModel[] = [];

}