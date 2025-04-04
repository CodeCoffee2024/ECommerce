import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class TitleService {

  setTitle(title: string) {
    document.title = title; // Correct way to update the page title
  }
}
