import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'secondsToTime',
})
export class SecondsToTimePipe implements PipeTransform {
  transform(value: number, ...args: unknown[]): string {
    if (value <= 60) return Math.trunc(value) + ' seconds';
    else if (value > 0) {
      const seconds = value % 60;
      const minutes = (value - seconds) / 60;
      return (
        Math.trunc(minutes) +
        ' minutes ' +
        (Math.trunc(seconds) !== 0 ? Math.trunc(seconds) + ' seconds' : '')
      );
    }
    return '';
  }
}
