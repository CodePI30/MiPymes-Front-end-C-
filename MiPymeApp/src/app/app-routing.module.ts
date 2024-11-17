import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MiPymeFormComponent } from './mi-pyme-form/mi-pyme-form.component';

const routes: Routes = [
  { path: '', redirectTo: '/mi-pyme-form', pathMatch: 'full' }, // Redirige a mi-pyme-form si la ruta está vacía
  { path: 'mi-pyme-form', component: MiPymeFormComponent }, // Ruta para tu componente
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
