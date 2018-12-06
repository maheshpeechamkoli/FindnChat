import { Component, OnInit, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
model: any = {};
@Output() cancelRegister = new EventEmitter();

  constructor() { }

  ngOnInit() {
  }

  register() {
    console.log();
  }

  cancel() {
    this.cancelRegister.emit(false);
  }
}
