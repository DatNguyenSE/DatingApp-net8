import { Component, inject, signal } from '@angular/core';
import { Router } from '@angular/router';
import { ApiError } from '../../../_models/error';

@Component({
    selector: 'app-server-error',
    standalone: true,
    imports: [],
    templateUrl: './server-error.component.html',
    styleUrl: './server-error.component.css'
})
export class ServerErrorComponent {
  protected error : ApiError;
  private router = inject (Router);
  protected showDetails = false;
  
  constructor() {
    const navigation = this.router.getCurrentNavigation();
    this.error = navigation?.extras?.state?.['error'];
  }

  detailsToggle(){
    this.showDetails = !this.showDetails;
  }
}
