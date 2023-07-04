import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-auth-callback',
  templateUrl: './auth-callback.component.html',
  styleUrls: ['./auth-callback.component.scss']
})
export class AuthCallbackComponent implements OnInit {

  error: boolean;

  constructor(private authService: AuthService, private router: Router, private route: ActivatedRoute, private spinner: NgxSpinnerService) {}

  async ngOnInit() {
    this.spinner.show();
    if (this.route.snapshot.fragment && this.route.snapshot.fragment.indexOf('error') >= 0) {
      this.spinner.hide();
      this.error=true;
      return;
    }

    await this.authService.completeAuthentication();
  }
}