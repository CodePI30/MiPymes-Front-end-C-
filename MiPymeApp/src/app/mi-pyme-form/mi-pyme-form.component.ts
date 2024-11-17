import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';  // Importación necesaria para obtener parámetros de la URL

@Component({
  selector: 'app-mi-pyme-form',
  templateUrl: './mi-pyme-form.component.html',
  styleUrls: ['./mi-pyme-form.component.css']
})
export class MiPymeFormComponent implements OnInit {
  public RNC: string = '';
  public Empresa: string = '';
  public Fecha_Emision: string = '';
  public Fecha_de_Vencimiento: string = '';
  public Clasificacion: string = '';
  public Actividad: string = '';

  constructor(private route: ActivatedRoute) { }

  ngOnInit(): void {
    // Obtener los parámetros de la URL
    this.route.queryParams.subscribe(params => {
      // Asignar los valores de los parámetros a las variables correspondientes
      this.RNC = params['RNC'] || '';
      this.Empresa = params['Empresa'] || '';
      this.Fecha_Emision = params['Fecha_Emision'] || '';
      this.Fecha_de_Vencimiento = params['Fecha_de_Vencimiento'] || '';
      this.Clasificacion = params['Clasificacion'] || '';
      this.Actividad = params['Actividad'] || '';

      // Mostrar los valores
      console.log(this.RNC, this.Empresa, this.Fecha_Emision, this.Fecha_de_Vencimiento, this.Clasificacion, this.Actividad);
    });
  }
}
