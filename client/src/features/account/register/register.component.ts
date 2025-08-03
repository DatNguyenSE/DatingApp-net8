import { Component, inject, input, output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../../../core/_services/account.service';
import { ToastrService } from 'ngx-toastr';



@Component({
  selector: 'app-register',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  private accountService = inject(AccountService)
  // @Output() cancelRegister = new EventEmitter();
  cancelRegister = output<boolean>();   //out event         // <! pratice load data 'child to parent' -->
  creds:any={}
  private toastr = inject(ToastrService);
  register(){
    this.accountService.register(this.creds).subscribe({
      next: response => {
        console.log(response);
        this.cancel(); // if success -> rollback 
      },
      error: error => this.toastr.error(error.error)
    })
  }

  cancel() {
   this.cancelRegister.emit(false);
  }
}
