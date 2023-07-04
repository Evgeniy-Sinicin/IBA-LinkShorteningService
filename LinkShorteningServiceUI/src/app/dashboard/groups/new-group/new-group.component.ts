import { Component, OnInit } from '@angular/core';
import { Group } from 'src/app/models/group';
import { NgxSpinnerService } from 'ngx-spinner';
import { LinkService } from 'src/app/services/link.service';
import { finalize } from 'rxjs/operators';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-new-group',
  templateUrl: './new-group.component.html',
  styleUrls: ['./new-group.component.scss']
})
export class NewGroupComponent implements OnInit {
  group: Group = { name: '', id: undefined, redirectCount: undefined, linksCount: undefined };
  success: boolean;
  error: string;
  submitted: boolean = false;

  constructor(private spinner: NgxSpinnerService, private linksService: LinkService, private router: Router, private route: ActivatedRoute) { }

  ngOnInit(): void {
  }

  onSubmit(): void {
    this.spinner.show();

    this.linksService.addLinksGroup(this.group)
      .pipe(finalize(() => {
        this.spinner.hide();
      }))
      .subscribe(
        result => {
          this.success = true;
          this.router.navigate(['groups'], { relativeTo: this.route.parent });
        },
        error => {
          this.error = error;
        }
      );
  }
}
