import { Component } from '@angular/core';
import { AuthService } from '../auth/auth.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  email: string = '';
  password: string = '';

  constructor(private authService: AuthService, private toastr: ToastrService) { }

  login() {
    this.authService.login(this.email, this.password).subscribe({
      next: () => {
        this.toastr.success('Login succesfull', 'Success');
      },
      error: (error: any) => {
        // Handle registration error (e.g., display error message)
        console.log(error)
      }
    });
  }

}
