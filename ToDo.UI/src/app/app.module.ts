import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { ErrorHandler } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatButtonModule, MatCardModule, MatIconModule, MatInputModule, MatSliderModule, MatSnackBarModule, MatTabsModule } from '@angular/material';
import { FormsModule } from '@angular/forms';
import { ToastrModule } from 'ngx-toastr';
import { AuthService } from './services/auth.service';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthInterceptor } from './guard/auth.interceptor';
import { ErrorHandlerService } from './helpers/error-handler.service';
import { ErrorService } from './models/error-service';
import { MauthComponent } from './pages/mauth/mauth.component';
import { TaskComponent } from './pages/task-component/task-component';

@NgModule({
  declarations: [
    AppComponent,
    MauthComponent,
    TaskComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatButtonModule,
    MatSliderModule,
    MatCardModule,
    MatTabsModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule,
    FormsModule,
    MatSnackBarModule,
    HttpClientModule,
    ToastrModule.forRoot({ toastClass: 'toast toast-bootstrap-compatibility-fix'}),
  ],
  providers: [  
      AuthService,ErrorService,
    {
    provide:HTTP_INTERCEPTORS,
    useClass :AuthInterceptor,
    multi: true
    },
    
    {
      provide:ErrorHandler,
      useClass:ErrorHandlerService
    },

  

],
  bootstrap: [AppComponent]
})
export class AppModule { }
