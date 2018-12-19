import { Component, OnInit } from '@angular/core';
import { ConverterService, Currencydata } from './converter.service'

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  selectedCurrency: Currencydata = null;
  currencies: Currencydata[] = [];

  constructor(private converterService: ConverterService) {
  }

  ngOnInit(): void {
    this.converterService.getAll().subscribe(list => this.currencies = list);
  }

  getCurrencyData(symbol: string): void {
    
  }

  changeCurrency(symbol): void {
    this.converterService.getBySymbol(symbol).subscribe(currency => this.selectedCurrency = currency);
  }
}
