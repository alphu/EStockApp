import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { ICompany } from '../Models/ICompany.Module';

@Injectable({
  providedIn: 'root'
})
export class CompanyService {

  readonly rootUrl = 'https://estockgetway.azurewebsites.net/api/v1/market/Company';
  constructor(private http: HttpClient,private router: Router) { }

  addCompany(companyData:ICompany)
  {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json'
    })
    const body: ICompany =companyData;
    return this.http.post(this.rootUrl + '/register', body,{headers:headers});
  }
  deleteCompany(companyId:number)
  {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json'
    })
    return this.http.delete(this.rootUrl + '/delete/'+companyId,{headers:headers});
  }

  viewAllCompanies()
  {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json'
    })
      return this.http.get(this.rootUrl+'/getall/',{ headers: headers })
  }
  viewCompanyByCode(id:number)
  {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json'
    })
      return this.http.get(this.rootUrl+'/info/'+id,{ headers: headers })
  }
}
