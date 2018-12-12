import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Observable } from 'rxjs';
import { environment } from '../environments/environment';

export class Currencydata {
  Symbol: string;
  Name: string;
  Country: string;
  EuroRate: number;
}

@Injectable()
export class ConverterService {
  constructor(private httpClient: HttpClient)
  {

  }

  getAll(): Observable<Currencydata[]> {
    return this.httpClient.get<Currencydata[]>(environment.currencyServiceUrl);
  }

  getBySymbol(symbol: string): Observable<Currencydata> {
    return this.httpClient.get<Currencydata>(
      `${environment.currencyServiceUrl}/${symbol}`);
  }
}
