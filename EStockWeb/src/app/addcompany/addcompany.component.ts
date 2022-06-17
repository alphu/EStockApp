import { Component, OnInit } from '@angular/core';
import { ICompany } from '../Models/ICompany.Module';
import { CompanyService } from '../Services/company.service';

@Component({
  selector: 'app-addcompany',
  templateUrl: './addcompany.component.html',
  styleUrls: ['./addcompany.component.css']
})
export class AddcompanyComponent implements OnInit {

  constructor(private companyService:CompanyService) { }
  company:ICompany={};
  result:ICompany={};
  flagError:boolean=false;
  flag:boolean=false;
 msgError:string="";
  ngOnInit(): void {
  }

  registerCompany()
  {
this.companyService.addCompany(this.company)
.subscribe({
  next: (v) => console.log(v),
  error: (e) => {this.flagError=true, this.msgError=e.error, console.log(e)},
  complete: () => {console.info('registration complete'),this.flag=true} 
})

  }
}
