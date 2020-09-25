import { Component, OnInit } from '@angular/core';
import { AutenticacionService } from '../_servicios/autenticacion.service';


@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  modelo: any = {};

  constructor(private servicioAuten: AutenticacionService) { }

  ngOnInit() {
  }

  login(){


    this.servicioAuten.login(this.modelo).subscribe( siguiente => {
      console.log('Sesion iniciada correctamente');
    }, error => {
        console.log('fallo');
      }
      );
  }

  estaLogeado() {

    const token = localStorage.getItem('token');
    return !!token;

  }

  salir(){

    localStorage.removeItem('token');
    console.log('Se ha cerrado la sesion');

  }

}
