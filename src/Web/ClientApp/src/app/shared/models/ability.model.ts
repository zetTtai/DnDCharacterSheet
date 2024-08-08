import { ABILITIES } from "src/app/shared/constants/app-constants";

export class Ability {
  id: string;
  score: number;

  constructor(id: string, score: number = ABILITIES.DEFAULT_VALUE) {
    this.id = id;
    this.score = score;
  }
};
