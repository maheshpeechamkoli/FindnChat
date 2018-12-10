import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
model: any = {};
@Output() cancelRegister = new EventEmitter();

  constructor(private authSerive: AuthService, private alertify: AlertifyService) { }

  ngOnInit() {
  }

  register() {
    this.authSerive.register(this.model).subscribe(() => {
      this.alertify.success('Registraion successfull');
    }, error => {
      this.alertify.success(error);
    });
  }

  cancel() {
    this.cancelRegister.emit(false);
  }
}
