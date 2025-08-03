import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { NavigationExtras, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { catchError } from 'rxjs';    //RxJS (Reactive Extensions for JavaScript)

export const ErrorInterceptor: HttpInterceptorFn = (req, next) => {
  const toast = inject(ToastrService);
  const router = inject(Router);
  return next(req).pipe(
    catchError(error =>{
      if(error) {
        switch (error.status) {
          case 400:
            if(error.error.errors) {
              const modelStateErrors = [];
              for(const key in error.error.errors) {
                if(error.error.errors[key]){
                  modelStateErrors.push(error.error.errors[key])
                }   
              }
              throw modelStateErrors.flat();
            }else{
                toast.error(error.error);
            }
            break;
          case 401:
            toast.error("unauthorized");
            break;
          case 404:
            router.navigateByUrl('/not-found');
            break;
          case 500:
            const navigationExtras: NavigationExtras = {state: {error: error.error}} // objnavigationExtras sẽ chứa state{eror{....}} 
            router.navigateByUrl('/server-error',navigationExtras)
            break;
          default:
             toast.error("Something went wrong");
            break;
        }
      }
      throw error; 
    })
  )
};
