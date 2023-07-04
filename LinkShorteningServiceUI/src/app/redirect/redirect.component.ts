import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { LinkService } from '../services/link.service';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-redirect',
  templateUrl: './redirect.component.html',
  styleUrls: ['./redirect.component.scss']
})
export class RedirectComponent implements OnInit {
  error: any;

  constructor(private route: ActivatedRoute, private router: Router, private linkService: LinkService, private spinner: NgxSpinnerService) { }

  ngOnInit(): void {
    this.spinner.show();
    this.route.params.subscribe( params => {
        let id = params['id'];
        this.linkService.getLinkUrl(id)
          .subscribe(
            url => { 
              console.log(url);
              window.location.href = url;
            },
            error => {
              if (error.status === 404) {
                this.error = error.error.title;
              }
              this.spinner.hide();
            }
          )
      }
    );
  }

}
