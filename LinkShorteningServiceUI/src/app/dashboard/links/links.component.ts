import { Component, OnInit } from '@angular/core';
import { Link } from 'src/app/models/link';
import { Router, ActivatedRoute } from '@angular/router';
import { LinkService } from 'src/app/services/link.service';
import { GetGroupLinksPageRequest } from 'src/app/models/get-group-links-page-request.model';
import { finalize } from 'rxjs/operators';
import { NgxSpinnerService } from 'ngx-spinner';
import { PageRequest } from 'src/app/models/page-request.model';
import { environment } from 'src/environments/environment';
import { Group } from 'src/app/models/group';

@Component({
  selector: 'app-links',
  templateUrl: './links.component.html',
  styleUrls: ['./links.component.scss']
})
export class LinksComponent implements OnInit {
  group: Group;
  links: Link[];
  page = 1;
  size = 15;
  isLoaded = false;

  constructor(private router: Router, 
              private route: ActivatedRoute, 
              private linksService: LinkService,
              private spinner: NgxSpinnerService) { }

  ngOnInit(): void {
    this.spinner.show();
    this.route.params.subscribe(params => {
      let id = +params['id'];
      if (id) {
        this.linksService.getGroupLinks(new GetGroupLinksPageRequest(this.page, this.size, id))
          .pipe(finalize(() => { 
            this.spinner.hide();
            this.isLoaded = true;
          })
        ).subscribe(
            response => {
              this.links = response.links;
              this.group = response.group;
            }
        )
        return;
      }
      this.linksService.getUserLinks(new PageRequest(this.page, this.size))
        .pipe(finalize(() => { 
            this.spinner.hide();
            this.isLoaded = true;
          })
        ).subscribe(
          links => this.links = links
        )
      }
    )
  }

  getShortUrl(id: string) {
    return `${environment.clientUrl}/${id}`;
  }

  addLink(): void {
    if (this.group && this.group.id) {
      this.router.navigate([`new-link/${this.group.id}`], { relativeTo: this.route.parent });
    }
  }

  redirect(url: string): void {
    window.location.href = url;
  }
}
