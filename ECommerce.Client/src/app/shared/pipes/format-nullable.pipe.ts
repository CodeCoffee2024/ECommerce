import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'formatNullable',
  standalone: true
})
export class FormatNullablePipe implements PipeTransform {
  transform(value): string {
    return value && value.toString().trim() ? value : '--';
  }
}
