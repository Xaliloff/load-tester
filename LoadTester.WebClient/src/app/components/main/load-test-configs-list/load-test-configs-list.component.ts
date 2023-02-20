import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatTable } from '@angular/material/table';
import { Router } from '@angular/router';
import { LoadTestConfig } from 'src/app/interfaces/models/load-test-config';
import { LoadTestConfigService } from 'src/app/services/load-test-config.service';
import { ConfirmationDialogComponent } from '../../confirmation-dialog/confirmation-dialog.component';

@Component({
  selector: 'app-load-test-configs-list',
  templateUrl: './load-test-configs-list.component.html',
  styleUrls: ['./load-test-configs-list.component.scss'],
})
export class LoadTestConfigsListComponent implements OnInit {
  @ViewChild(MatTable) configsTable!: MatTable<LoadTestConfig>;

  public displayedColumns: string[] = [
    'name',
    'url',
    'lastTimeUsed',
    'actions',
  ];
  public loadTestConfigs: LoadTestConfig[] = [];

  constructor(
    private loadTestConfigService: LoadTestConfigService,
    private dialog: MatDialog,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.loadTestConfigService.getList().subscribe((data) => {
      this.loadTestConfigs = data;
    });

    this.loadTestConfigService.loadTestConfigCreated.subscribe(
      (loadTestConfigId: string) => {
        this.loadTestConfigService
          .getById(loadTestConfigId)
          .subscribe((data) => {
            this.loadTestConfigs.push(data);
            this.configsTable.renderRows();
          });
      }
    );
  }

  runLoadTest(id: string) {
    console.log(id);
    this.loadTestConfigService.run(id).subscribe((data: any) => {
      console.log(data);
    });
  }

  openTestConfig(configId: string) {
    this.router.navigate(['load-testing', configId]);
  }

  onDeleteLoadTestConfig(id: string) {
    this.loadTestConfigService.getById(id).subscribe((data) => {
      // if (data.tests.length > 0) {
      this.areYouSure(id);
      // }
      // this.removeConfig(id);
    });
  }

  private areYouSure(id: string) {
    this.dialog.open(ConfirmationDialogComponent, {
      width: '500px',
      data: {
        title: 'Are you sure?',
        message: 'Do you really want to delete this test configuration?',
        buttons: [
          {
            text: 'Yes',
            action: () => this.removeConfig(id),
          },
        ],
      },
    });
  }

  private removeConfig(id: string) {
    this.loadTestConfigService.remove(id).subscribe((data) => {
      this.loadTestConfigs = this.loadTestConfigs.filter(
        (item) => item.id !== id
      );
    });
  }
}
