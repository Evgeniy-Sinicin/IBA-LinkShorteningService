import { Component, OnInit } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  constructor(private authService: AuthService, private spinner: NgxSpinnerService, private route: ActivatedRoute) { }

    title = "Login";

    ngOnInit() {
      this.spinner.show();
      this.route.params.subscribe(params => {
        let redirect = params['id'];
        this.authService.login(redirect);
      });
    }
}