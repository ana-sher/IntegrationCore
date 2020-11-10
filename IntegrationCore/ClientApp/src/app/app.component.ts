import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent {
  title = 'app';
  items = [
    {
      title: 'Systems',
      link: ['/systems'],
    },
    {
      title: 'Integrations',
      link: ['/integrations'],
    },
  ];
}
