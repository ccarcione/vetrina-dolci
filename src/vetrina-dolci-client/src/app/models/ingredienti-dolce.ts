import { Dolce } from "./dolce";
import { Ingrediente } from "./ingrediente";

export class IngredientiDolce {
    id: number;
    quantita: string;
    unitaDiMisura: string;
    note: string;

    dolceId: number;
    dolce: Dolce;

    ingredienteId: number;
    ingrediente: Ingrediente;

    constructor(data?: Partial<IngredientiDolce>) {
      Object.assign(this, data);
    }
  }
  