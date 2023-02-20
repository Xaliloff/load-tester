import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'runningTime',
})
export class RunningTimePipe implements PipeTransform {
  transform(value: Date, ...args: unknown[]): string {
    const secs = Date.now() - value.getSeconds();

    if (secs <= 60) return value + ' seconds';

    const seconds = secs % 60;
    const minutes = (secs - seconds) / 60;
    return minutes + ' minutes ' + (seconds != 0 ? seconds + ' seconds' : '');
  }
}
