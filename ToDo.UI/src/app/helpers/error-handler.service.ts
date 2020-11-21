import { Injectable, ErrorHandler } from "@angular/core";
import { HttpErrorResponse } from '@angular/common/http';
import { error } from 'util';

@Injectable()
export class ErrorHandlerService implements ErrorHandler{
    handleError(error:any):void{
}
}