import { DolceInVendita } from "./dolce-in-vendita";
import { IngredientiDolce } from "./ingredienti-dolce";

export class Dolce {
    id: number;
    nome: string;
    prezzo: number;
    tipoPiatto: string;
    ingPrincipale: string;
    persone: number;
    preparazione: string;
    note: string;
    
    DolciInVendita: DolceInVendita[];
    IngredientiDolce: IngredientiDolce[];

    constructor(data?: Partial<Dolce>) {
      Object.assign(this, data);
    }
  }
  