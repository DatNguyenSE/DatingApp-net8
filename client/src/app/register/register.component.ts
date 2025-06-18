import { Component, inject, input, output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../_services/account.service';



@Component({
  selector: 'app-register',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  private accountService = inject(AccountService)
  // @Input() usersFromHomeComponent : any; angular<=17
  usersFromHomeComponent = input.required<any>() //  <!--option: pratice load data 'parent to child' -->
  // @Output() cancelRegister = new EventEmitter();
  cancelRegister = output<boolean>();   //out event         // <! pratice load data 'child to parent' -->
  model:any={}

  register(){
    this.accountService.register(this.model).subscribe({
      next: response => {
        console.log(response);
        this.cancel(); // if success -> rollback 
      },
      error: error => console.log(error)
    })
  }

  cancel() {
   this.cancelRegister.emit(false);
  }
}
