import { Component, OnInit } from '@angular/core';
import { ICompany } from '../Models/ICompany.Module';
import { ICompanyStock } from '../Models/ICompanyStock.Module';
import { CompanyService } from '../Services/company.service';

@Component({
  selector: 'app-listcompanies',
  templateUrl: './listcompanies.component.html',
  styleUrls: ['./listcompanies.component.css']
})
export class ListcompaniesComponent implements OnInit {

  constructor(private companyService:CompanyService) { }
  companyList:ICompany[]=[];
  flagError:boolean=false;
  msgError:string=""
  ngOnInit(): void {
    this.companyService.viewAllCompanies()
    .subscribe({
      next: (v) => {console.log(v), this.companyList = v as ICompanyStock[]},
      error: (e) => {this.flagError=true, this.msgError=e.error, console.log(e)},
      complete: () => {console.info('registration complete')} 
    })
  }

}
