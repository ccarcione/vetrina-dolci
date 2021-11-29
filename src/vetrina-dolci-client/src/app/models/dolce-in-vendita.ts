import { Dolce } from "./dolce";

export class DolceInVendita {
    id: number;
    disponibilita: number;
    inVenditaDa: Date;
    prezzo: number;
    
    dolceId: number;
    dolce: Dolce;
    
    constructor(data?: Partial<DolceInVendita>) {
      Object.assign(this, data);
    }
  }
  