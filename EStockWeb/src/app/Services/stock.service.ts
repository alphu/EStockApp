import { Injectable } from '@angular/core';
import { IStockPrice } from '../Models/IStockPrice.Module';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';
import { ICompany } from '../Models/ICompany.Module';

@Injectable({
  providedIn: 'root'
})
export class StockService {

  readonly rootUrl = 'http://localhost:5002/api/v1/market/Stock';
  constructor(private http: HttpClient,private router: Router) { }
  addStock(stockData:IStockPrice)
  {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json'
    })
    const body: IStockPrice =stockData;
    return this.http.post(this.rootUrl + '/add/' + stockData.companyCode, body,{headers:headers});
  }

  getCompanyStockByDate(id:number,startDate:Date,endDate:Date)
  {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json'
    })
      return this.http.get(this.rootUrl+'/get/'+id+'/'+startDate+'/'+endDate,{ headers: headers })
  }
}
