import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { UsuarioService } from '../servicios/usuario.service';
import { CompartidoService } from 'src/app/compartido/compartido.service';
import { Login } from '../interfaces/login';
import { CookieService } from 'ngx-cookie-service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  formLogin: FormGroup;
  ocultarPassword: boolean = true;
  mostrarLoading: boolean = false;

  constructor(private fb: FormBuilder, 
              private router: Router, 
              private usuarioService: UsuarioService, 
              private compartidoService: CompartidoService,
              private cookeService: CookieService){
                  this.formLogin = this.fb.group({
                    username: ['', Validators.required],
                    password: ['', Validators.required]
                  });
              }

  iniciarSesion(){
    this.mostrarLoading = true;
    const request: Login = {
      username: this.formLogin.value.username,
      password: this.formLogin.value.password
    };

    this.usuarioService.iniciarSesion(request).subscribe({
      next: (response) => {
        this.compartidoService.guardarSesion(response);

        this.cookeService.set(
          'Authorization',
          `Bearer ${response.token}`,
          undefined,
          '/',
          undefined,
          true,
          'Strict'
        );

        this.router.navigate(['layout']);
      },
      complete: () => {
        this.mostrarLoading = false
      },
      error: (error) => {
        this.compartidoService.mostrarAlerta(error.error, 'Error!');
        this.mostrarLoading = false;
      }
    })
  }
}
