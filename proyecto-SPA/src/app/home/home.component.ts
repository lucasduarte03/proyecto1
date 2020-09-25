import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  modo = false;


  constructor(private http: HttpClient) { }

  ngOnInit() {

  }

  banderaRegistro(){

    this.modo = !this.modo;

  }


  cambiarBanderaRegistro(modo: boolean){

      this.modo = modo;
  }

}
