import { Component, signal } from '@angular/core';
import { RegisterComponent } from "../../features/account/register/register.component";
import { HttpClient } from '@angular/common/http';

@Component({
    selector: 'app-home', 
    standalone: true,
    imports: [RegisterComponent],
    templateUrl: './home.component.html',
    styleUrl: './home.component.css'
})
export class HomeComponent {
  protected registerMode = signal(false);

  showRegister(value: boolean) {
    this.registerMode.set(value);
  }
}