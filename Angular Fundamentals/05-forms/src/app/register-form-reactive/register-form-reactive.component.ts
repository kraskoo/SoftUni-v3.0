import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';

@Component({
// tslint:disable-next-line: component-selector
  selector: 'register-form-reactive',
  templateUrl: './register-form-reactive.component.html',
  styleUrls: ['./register-form-reactive.component.css']
})
export class RegisterFormReactiveComponent implements OnInit {
  form;

  constructor(private fb: FormBuilder) {}

  ngOnInit() {
    this.form = this.fb.group({
      fullName: ['', [Validators.required, Validators.pattern(/[A-Z][a-z]+ [A-Z][a-z]+/)]],
      email: [
        '',
        [
          Validators.required,
          Validators.pattern(
// tslint:disable-next-line: max-line-length
            /^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$/)
          ]
        ],
      phoneCode: ['', Validators.required],
      phoneNumber: ['', [Validators.required, Validators.pattern(/\d{9}/)]],
      position: ['', [Validators.required]],
      password: ['', [Validators.required, Validators.pattern(/^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{3,16}$/)]],
      confirmPassword: ['', [Validators.required]],
      imageUrl: ['', [Validators.pattern(/^http.+\.[jpgn]{3}$/)]]
    });
  }

  register() {
    if (this.form.valid) {
      this.form.reset();
    }
  }

  get controls() {
    return this.form.controls;
  }
}
