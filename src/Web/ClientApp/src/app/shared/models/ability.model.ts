export class Ability {
  id: string;
  score: number;

  constructor(id: string, score: number = -1) {
    this.id = id;
    this.score = score;
  }
};
