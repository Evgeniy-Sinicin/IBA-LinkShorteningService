import { Component, OnInit } from '@angular/core';
import { Link } from 'src/app/models/link';
import { NgxSpinnerService } from 'ngx-spinner';
import { Router, ActivatedRoute } from '@angular/router';
import { LinkService } from 'src/app/services/link.service';
import { AddLinkRequest } from 'src/app/models/add-link.model';
import { finalize } from 'rxjs/operators';

@Component({
  selector: 'app-new-link',
  templateUrl: './new-link.component.html',
  styleUrls: ['./new-link.component.scss']
})
export class NewLinkComponent implements OnInit {
  link: AddLinkRequest = { name: '', url: '', groupId: undefined};
  success: boolean;
  error: string;
  submitted: boolean = false;

  constructor(private spinner: NgxSpinnerService, private router: Router, private route: ActivatedRoute, private linksService: LinkService) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
        let id = +params['id'];
        this.link.groupId = id;
      }
    );
  }

  onSubmit(): void {
    this.spinner.show();

    this.linksService.addLink(this.link)
      .pipe(finalize(() => {
        this.spinner.hide();
      }))
      .subscribe(
        result => {
          this.success = true;
          this.router.navigate([`group/${this.link.groupId}`], { relativeTo: this.route.parent });
        },
        error => {
          this.error = error;
        }
      );
  }

}
