import { IngredientiDolce } from "./ingredienti-dolce";

export class Ingrediente {
    id: number;
    nome: string;
    Proteine: number;
    Zuccheri: number;
    Grassi: number;
    Colesterolo: number;
    Fibra: number;
    Kcal: number;

    ingredientiDolce: IngredientiDolce[]

    constructor(data?: Partial<Ingrediente>) {
      Object.assign(this, data);
    }
  }
  