import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../../core/_services/account.service';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { Router,RouterLink, RouterLinkActive } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { TitleCasePipe } from '@angular/common';
@Component({
  selector: 'app-nav',
  standalone: true,
  imports: [FormsModule, BsDropdownModule, RouterLink, RouterLinkActive, TitleCasePipe],
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.css'
})


export class NavComponent {
  accountService =inject(AccountService);
  private router = inject(Router);
  private toastr = inject(ToastrService);

  protected creds: any = {}

  login() {
    this.accountService.login(this.creds).subscribe({ // model ở đây được truyền qua nav.component.html
      next : _ => {
        this.router.navigateByUrl('/members'); //khỏi tạo obj router inject Router -> truy cập Url
        this.toastr.success('Logged in successfully');
        this.creds = {};
      },
      error : error => this.toastr.error(error.error) // obj toastr -> alert 
    })
  }

  logout() {
    this.accountService.logout();
    this.router.navigateByUrl('/')
  }
}
