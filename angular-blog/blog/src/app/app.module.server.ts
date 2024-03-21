import { NgModule } from '@angular/core';
import { ServerModule } from '@angular/platform-server';

import { AppModule } from './app.module';
import { AppComponent } from './app.component';
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { ActivatedRoute, RouterModule } from '@angular/router';


@NgModule({
  imports: [
    ServerModule,
    RouterModule.forRoot([]),
  ],
  providers: [
  ],
  bootstrap: [AppComponent],
})
export class AppServerModule {}
