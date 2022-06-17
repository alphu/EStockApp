import { IStockPrice } from "./IStockPrice.Module";

export interface ICompanyStock
{

    id?:string;
    code?:number
    name?:string 
    ceo?:string
    turnover?:number
    website?:string 
    stockExchange?:string
    stock?:IStockPrice[]
}