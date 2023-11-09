import { Component } from '@angular/core';
import { Observable, map } from 'rxjs';
import { LoaderService } from '../../services/loader.service'

@Component({
  selector: 'app-spinner',
  templateUrl: './spinner.component.html',
  styleUrls: ['./spinner.component.scss']
})
export class SpinnerComponent {
  public  showSpinner$: Observable<any>
    constructor(loaderService:LoaderService){
      console.log('spinner');
      this.showSpinner$ = loaderService.pendingHttpRequest$
      .pipe(map(pendingHttpRequests => {
        return pendingHttpRequests > 0
      }))
    }
}
