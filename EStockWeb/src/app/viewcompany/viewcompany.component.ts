import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ICompanyStock } from '../Models/ICompanyStock.Module';
import { IStockPrice } from '../Models/IStockPrice.Module';
import { CompanyService } from '../Services/company.service';
import { StockService } from '../Services/stock.service';

@Component({
  selector: 'app-viewcompany',
  templateUrl: './viewcompany.component.html',
  styleUrls: ['./viewcompany.component.css']
})
export class ViewcompanyComponent implements OnInit {

  code:number=0
  flagError:string="";
  company:ICompanyStock={}
  companyStock:ICompanyStock={}
  startdate:Date= new Date()
  enddate:Date= new Date()
  stock:IStockPrice[]=[];
  flagStock:boolean=false;
  flag:boolean=false
  msgError:string=""
  min:number=0;
  max:number=0;
  avg:number=0;
  sum:number=0;
  price:number[]=[];
  flagInvalid:boolean=false
  constructor(private route:ActivatedRoute, private companyService:CompanyService, private stockService:StockService) {
    this.code =this.route.snapshot.params['id']
   }

  ngOnInit(): void {
    console.log(this.code)
    this.companyService.viewCompanyByCode(this.code)
    .subscribe({
      next: (v) => {console.log(v), this.company = v as ICompanyStock },
      error: (e) => {this.flagInvalid=true, console.log(e)},
      complete: () => {console.info('view company complete')} 
    })
  }
  searchStock()
  {
    this.min=0   
      this.max=0 
      this.avg=0
    this.flagStock=false 
    this.flag=false;
    this.stockService.getCompanyStockByDate(this.code, this.startdate,this.enddate)
    .subscribe({
      next: (v) => {console.log(v), this.companyStock = v as ICompanyStock, this.getStock()},
     // error: (e) => {this.flagError=true, this.msgError=e.error, console.log(e)},
      complete: () => {console.info('view company complete')} 
    })
  }

  getStock()
  {
    this.stock=this.companyStock.stock as IStockPrice[]
    if(this.stock.length!=0)
    {
      this.flag=false;
      this.flagStock=true 
      this.stock.sort()
      for (let j of this.stock??[])
    {
      this.price.push(<number>j.price)
      this.sum+=<number>j.price;
    }
      this.min=Math.min(...this.price)   
      this.max=Math.max(...this.price)  
      this.avg=this.sum/this.price.length
      console.log(this.min);
      console.log(this.max);
      console.log(this.avg);
    }
    else
    {
      this.flagStock=false 
      this.flag=true;
    }
  }
}


