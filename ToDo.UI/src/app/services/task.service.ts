import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { MResponse } from '../models/response.model';
import { AuthResponse } from '../models/auth-response';
import { Task } from '../models/task.model';

@Injectable({
  providedIn: 'root'
})

export class TaskService {

  constructor(private http: HttpClient) { }
  readonly BaseURI = environment.apiBaseURl;

  post(model: Task): Observable<MResponse<Task>> {
    return this.http.post<MResponse<Task>>(this.BaseURI + '/task', model).pipe();
  }

  update(member: Task): Observable<MResponse<Task>> {
    return this.http.put<MResponse<Task>>(this.BaseURI + '/Task', member).pipe();
  }

  delete(id: string): Observable<MResponse<Task>> {
    return this.http.delete<MResponse<Task>>(this.BaseURI + '/Task/' + id).pipe();
  }

  get(id: string): Observable<MResponse<Task>> {
    return this.http.get<MResponse<Task>>(this.BaseURI + '/Task/' + id).pipe();
  }

  getMyList(): Observable<MResponse<Task>> {
    return this.http.get<MResponse<Task>>(this.BaseURI + '/Task').pipe();
  }
 
}


