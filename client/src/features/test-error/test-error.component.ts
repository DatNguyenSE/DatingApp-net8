import { HttpClient } from '@angular/common/http';
import { Component, inject, signal } from '@angular/core';

@Component({
    selector: 'app-test-error',
    imports: [],
    templateUrl: './test-error.component.html',
    styleUrl: './test-error.component.css'
})
export class TestErrors {
  private htttp = inject(HttpClient);
  baseUrl='https://localhost:5001/api/';
  validationError = signal<String[]>([])

  get404Error(){
    this.htttp.get(this.baseUrl+'buggy/not-found').subscribe({
      next: response => console.log(response),
      error: error => console.log(error)
    })
  }
  get400Error(){
    this.htttp.get(this.baseUrl+'buggy/bad-request').subscribe({
      next: response => console.log(response),
      error: error => console.log(error)
    })
  }
  get400ValidationError(){ 
    this.htttp.post(this.baseUrl+'account/register',{}).subscribe({
      next: response => console.log(response),
      error: error => {
        console.log(error);
        this.validationError.set(error);
      }
    })
  }
  get401Error(){
    this.htttp.get(this.baseUrl+'buggy/auth').subscribe({
      next: response => console.log(response),
      error: error => console.log(error)
    })
  }
   get500Error(){
    this.htttp.get(this.baseUrl+'buggy/server-error').subscribe({
      next: response => console.log(response),
      error: error => console.log(error)
    })
  }
}
