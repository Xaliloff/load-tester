import { HttpClient } from '@angular/common/http';
import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatTabGroup } from '@angular/material/tabs';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss'],
})
export class MainComponent implements OnInit, AfterViewInit {
  @ViewChild('tab', { static: false }) public tab!: MatTabGroup;
  public tabs = ['Recent', 'All'];
  public selectedTab = this.tabs[0];

  constructor(public http: HttpClient) {}
  ngOnInit(): void {}
  ngAfterViewInit(): void {
    this.tab.selectedTabChange.subscribe((x) => {
      this.selectedTab = x.tab.textLabel;
    });
  }
}
