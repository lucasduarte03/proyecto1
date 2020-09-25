import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {map} from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AutenticacionService {
  url = 'http://localhost:5000/api/auth/';

  constructor(private http: HttpClient) { }

  // tslint:disable-next-line: typedef
  login(modelo: any){
    return this.http.post(this.url + 'login', modelo).pipe(
      map((respuesta: any) => {
        const usuario = respuesta;
        if (usuario){
          localStorage.setItem('token', usuario.token);
        }

      })

  );

}

  registrar(modelo: any){

      return this.http.post(this.url + 'registrar', modelo);

  }



}
