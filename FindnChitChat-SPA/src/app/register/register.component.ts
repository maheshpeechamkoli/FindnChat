import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { BsDatepickerConfig } from 'ngx-bootstrap';
import { User } from '../_models/user';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
user: User;
registerForm: FormGroup;

  colorTheme = 'theme-blue';
  bsConfig: Partial<BsDatepickerConfig>;

@Output() cancelRegister = new EventEmitter();

  constructor(private authSerive: AuthService, private alertify: AlertifyService, private fb: FormBuilder, private router: Router) { }

  ngOnInit() {
    this.bsConfig = Object.assign({}, { containerClass: this.colorTheme });
    this.createRegisterForm();
  }

  createRegisterForm() {
    this.registerForm = this.fb.group({
      gender: ['male'],
      username: ['', [Validators.required, Validators.minLength(4)]],
      knownAs: ['', [Validators.required]],
      dateOfBirth: [null, [Validators.required]],
      city: ['', [Validators.required]],
      country: ['', [Validators.required]],
      password: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(8)]],
      confirmpassword: ['', Validators.required]
    }, {validator: this.passwordMatchValidator});
  }

  passwordMatchValidator(g: FormGroup) {
    return g.get('password').value === g.get('confirmpassword').value ? null : {'mismatch': true};
  }

  register() {
    if (this.registerForm.valid) {
        this.user = Object.assign({}, this.registerForm.value);
        this.authSerive.register(this.user).subscribe(() => {
          this.alertify.success('Registration Succsfully');
        }, error => {
          this.alertify.error(error);
        }, () => {
          this.authSerive.login(this.user).subscribe(() => {
            this.router.navigate(['/members']);
          });
        });
    }

    // this.authSerive.register(this.model).subscribe(() => {
    //   this.alertify.success('Registraion successfull');
    // }, error => {
    //   this.alertify.error(error);
    // });

  }

  cancel() {
    this.cancelRegister.emit(false);
  }
}
