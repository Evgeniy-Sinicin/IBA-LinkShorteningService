import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Group } from 'src/app/models/group';
import { LinkService } from 'src/app/services/link.service';
import { PageRequest } from 'src/app/models/page-request.model';
import { finalize } from 'rxjs/operators';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-groups',
  templateUrl: './groups.component.html',
  styleUrls: ['./groups.component.scss']
})
export class GroupsComponent implements OnInit {
  groups: Group[];
  page = 1;
  size = 15;

  constructor(private router: Router, private linksService: LinkService, private spinner: NgxSpinnerService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.spinner.show();
    this.linksService.getGroups(new PageRequest(this.page, this.size))
      .pipe(finalize(() => this.spinner.hide()))
      .subscribe(
        groups => this.groups = groups
      )
  }

  addGroup() {
    this.router.navigate(["new-group"], { relativeTo: this.route.parent });
  }

  redirect(id: number): void {
    this.router.navigate([`dashboard/group/${id}`]);
  }

  get getLinksCount(): number {
    let sum = 0;
    if (this.groups && this.groups.length) {
      for (let index = 0; index < this.groups.length; index++) {
        sum += this.groups[index].linksCount;      
      }
    }
    return sum;
  }
}
