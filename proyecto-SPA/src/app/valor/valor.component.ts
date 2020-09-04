import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-valor',
  templateUrl: './valor.component.html',
  styleUrls: ['./valor.component.scss']
})
export class ValorComponent implements OnInit {

  valores: any;

  constructor(private http: HttpClient) { }


  // tslint:disable-next-line: typedef
  ngOnInit() {
    this.getValores();
  }

  // tslint:disable-next-line: typedef
  getValores(){
    this.http.get('http://localhost:5000/api/values').subscribe(respuesta => {
        this.valores = respuesta;

    }, error => {console.log(error);}
    );

  }

}
