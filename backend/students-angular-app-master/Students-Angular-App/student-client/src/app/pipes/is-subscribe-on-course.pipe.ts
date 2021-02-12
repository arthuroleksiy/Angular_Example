import { Pipe, PipeTransform } from '@angular/core';
import { StudCourse } from '../interfaces/stud-curse';

@Pipe({
  name: 'IsSubscribeOnCourse'
})
export class IsSubscribeOnCoursePipe implements PipeTransform {

  transform(items: StudCourse[], IsSubscribed: boolean): any {
    if(!items || IsSubscribed === undefined){
      return items;
    }

    return items.filter(it => it.isSubscribed === IsSubscribed);
  }

}
