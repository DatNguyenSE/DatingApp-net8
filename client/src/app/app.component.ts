import { Component, inject, OnInit } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';
import { NavComponent } from '../features/nav/nav.component';
import { AccountService } from '../core/_services/account.service';


@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, NavComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})


//connect backend with fontend
//export = public
export class AppComponent { 

  protected router = inject(Router);

}
