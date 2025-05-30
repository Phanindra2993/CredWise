import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzCardModule } from 'ng-zorro-antd/card';
import { NzFormModule } from 'ng-zorro-antd/form';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzInputModule } from 'ng-zorro-antd/input';
import { AuthService } from '../../../_services/auth.service';
import { Router, RouterLink } from '@angular/router';

@Component({
  selector: 'app-register',
  imports: [
    NzCardModule,
    NzFormModule,
    ReactiveFormsModule,
    FormsModule,
    CommonModule,
    NzButtonModule,
    NzCardModule,
    NzInputModule,
    NzIconModule,
    RouterLink,
  ],
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
})
export class RegisterComponent {
  registerForm!: FormGroup;
  hidePassword = true;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) {
    this.registerForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      phoneNumber: ['', [Validators.required, Validators.pattern(/^\d{10}$/) ,
,]
    ],
      password: [
        '',
        [
          Validators.required,
          Validators.minLength(8),
          Validators.pattern(/^(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])/),
        ],
      ],
      createdBy: ['system'],  // or use a real value like logged-in user ID

    });
  }

  get emailError(): string {
    const control = this.registerForm.get('email');
    if (control?.hasError('required')) return 'Email is required';
    if (control?.hasError('email')) return 'Invalid email format';
    if(control ?.hasError('phone'))  return 'Please enter a valid 10-digit phone number starting with 6-9';



    return '';
  }

  get passwordError(): string {
    const control = this.registerForm.get('password');
    if (control?.hasError('required')) return 'Password is required';
    if (control?.hasError('minlength')) return 'Min 8 characters required';
    if (control?.hasError('pattern'))
      return 'Must include uppercase, number & special char';
    return '';
  }

  onSubmit() {
    if (this.registerForm.valid) {
      console.log('Registration Data:', this.registerForm.value);
      const formData = this.registerForm.value;
      this.authService.register(formData).subscribe({
        next: (res) => {
          console.log('Registration Success:', res);
          this.router.navigate(['/login']);
        },
        error: (err) => {
          console.log('Registration Failed:', err);
        },
      });
    }
  }
}
