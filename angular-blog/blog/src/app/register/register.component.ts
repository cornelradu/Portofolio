import { Component } from '@angular/core';
import { AuthService } from '../auth/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  email: string = '';
  password: string = '';
  confirmPassword: string = '';
  firstName: string = '';
  lastName: string = '';
  gender: string = '';

  constructor(private authService: AuthService) { }

  register() {
    const userInfo = {
      email: this.email,
      password: this.password,
      passwordConfirm: this.confirmPassword,
      firstName: this.firstName,
      lastName: this.lastName,
      gender: this.gender
    };

    this.authService.register(userInfo).subscribe({
      next: () => {
        // Registration successful
        // You can redirect to login page or display a success message
      },
      error: (error: any) => {
        // Handle registration error (e.g., display error message)
        console.log(error)
      }
    });
  }
}
