import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
// development API key
const pixibayKey = '19990507-50871329fbe1e55552ad597e9';

@Injectable({
  providedIn: 'root'
})
export class HelperService {

  constructor(private http: HttpClient) { }

  getImageFromPixabay(name: string) {
    return this.http.get('https://pixabay.com/api/?key=' + pixibayKey
      + '&q=' + name
      + '&image_type=photo&pretty=true&lang=it');
  }
}
