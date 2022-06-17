import { Routes } from '@angular/router'
import { HomeComponent } from './home/home.component';
import { NavbarComponent } from './navbar/navbar.component';
import { AddcompanyComponent } from './addcompany/addcompany.component';
import { ViewcompanyComponent } from './viewcompany/viewcompany.component';
import { ListcompaniesComponent } from './listcompanies/listcompanies.component';

export const appRoutes: Routes = [
    { path: 'Home', component: HomeComponent },
    { path : '', redirectTo:'/ListCompany', pathMatch : 'full'},
    {path:'AddCompany',component:AddcompanyComponent},
    {path:'ViewCompany/:id',component:ViewcompanyComponent},
    {path:'ListCompany',component:ListcompaniesComponent}
];