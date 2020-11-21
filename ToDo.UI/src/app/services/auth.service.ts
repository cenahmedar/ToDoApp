import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { MResponse } from '../models/response.model';
import { AuthResponse } from '../models/auth-response';
import { Member } from '../models/member.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http: HttpClient) { }
  readonly BaseURI = environment.apiBaseURl;

  login(body) : Observable<MResponse<AuthResponse>>{
    return this.http.post<MResponse<AuthResponse>>(this.BaseURI + '/auth/login', body);
  }

  register(member: Member): Observable<MResponse<Member>> {
    return this.http.post<MResponse<Member>>(this.BaseURI + '/auth/register', member).pipe();
  }

  getProfile(): Observable<MResponse<Member>> {
    return this.http.get<MResponse<Member>>(this.BaseURI + '/auth/profile').pipe();
  }

  
}


