import { PageInfo } from "./pagination-result";

export class Pagination {
    selectItemsPerPage: number[] = [10, 50, 100];
    pageSize = this.selectItemsPerPage[0];
    pageIndex = 1;
    allItemsLength = 0;

    pageInfo: PageInfo;
    
    constructor(data?: Partial<Pagination>) {
      Object.assign(this, data);
    }
  }
  