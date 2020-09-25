import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AutenticacionService } from '../_servicios/autenticacion.service';

@Component({
  selector: 'app-registrar',
  templateUrl: './registrar.component.html',
  styleUrls: ['./registrar.component.css']
})
export class RegistrarComponent implements OnInit {

  @Output() cancelarRegistro = new EventEmitter();

  variable: any = {};

  constructor(private servicioAuten: AutenticacionService) { }

  ngOnInit() {
  }

  registrar(){
    this.servicioAuten.registrar(this.variable).subscribe( () => {
        console.log('Registro exitoso');
    }, error => {
      console.log(error);
    });

  }

  cancelar(){
    this.cancelarRegistro.emit(false);
    console.log('cancelado');

  }

}
