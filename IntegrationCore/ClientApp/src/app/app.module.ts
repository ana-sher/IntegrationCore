import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { HomeComponent } from './pages/home/home.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NbThemeModule, NbLayoutModule, NbSidebarModule,
   NbButtonModule, NbCardModule, NbListModule, NbIconModule, NbInputModule,
   NbSelectModule, NbCheckboxModule, NbAccordionModule, NbMenuModule, NbDialogModule } from '@nebular/theme';
import { NbEvaIconsModule } from '@nebular/eva-icons';
import { SystemComponent } from './pages/system/system.component';
import { IntegrationComponent } from './pages/integration/integration.component';
import { TypeComponent } from './pages/type/type.component';
import { TypeExpandedComponent } from './components/type-expanded/type-expanded.component';
import { SystemsComponent } from './pages/systems/systems.component';
import { TypesComponent } from './pages/types/types.component';
import { IntegrationsComponent } from './pages/integrations/integrations.component';
import { IntegrationResultsComponent } from './pages/integration-results/integration-results.component';
import { GenerateConnectionsDialogComponent } from './pages/integration/generate-connections-dialog.component';
import { GenerateTypesDialogComponent } from './pages/types/generate-types-dialog.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    SystemComponent,
    IntegrationComponent,
    TypeComponent,
    TypeExpandedComponent,
    SystemsComponent,
    TypesComponent,
    IntegrationsComponent,
    GenerateConnectionsDialogComponent,
    GenerateTypesDialogComponent,
    IntegrationResultsComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'system/:id', component: SystemComponent },
      { path: 'system', component: SystemComponent },
      { path: 'type/:id/:typeId', component: TypeComponent },
      { path: 'type/:id', component: TypeComponent },
      { path: 'integration', component: IntegrationComponent },
      { path: 'integrations', component: IntegrationsComponent },
      { path: 'systems', component: SystemsComponent },
      { path: 'types/:id', component: TypesComponent },
      { path: 'results/:id', component: IntegrationResultsComponent },
    ]),
    BrowserAnimationsModule,
    NbThemeModule.forRoot({ name: 'default' }),
    NbLayoutModule,
    NbEvaIconsModule,
    NbSidebarModule.forRoot(),
    NbDialogModule.forRoot(),
    NbButtonModule,
    NbListModule,
    NbInputModule,
    NbCheckboxModule,
    NbSelectModule,
    NbIconModule,
    NbAccordionModule,
    NbMenuModule.forRoot(),
    NbCardModule
  ],
  providers: [],
  bootstrap: [AppComponent],
  entryComponents: [GenerateConnectionsDialogComponent,
    GenerateTypesDialogComponent,],
  exports: [TypeExpandedComponent]
})
export class AppModule { }
