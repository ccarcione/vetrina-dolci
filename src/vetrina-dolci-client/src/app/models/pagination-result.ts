export class PageInfo {
    totalCount: number;
    hasNext: boolean;
    hasPrevious: boolean;
    pageSize: number;
    currentPage: number;
    totalPages: number;
  }
  
  export class PaginationResult<T> {
    data: T[];
    pagination: PageInfo;
  }
  