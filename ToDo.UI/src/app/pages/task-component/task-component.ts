import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { Task } from 'src/app/models/task.model';
import { TaskService } from 'src/app/services/task.service';

@Component({
  selector: 'app-task-component',
  templateUrl: './task-component.component.html',
  styleUrls: ['./task-component.component.css']
})
export class TaskComponent implements OnInit {

  todos: Task[];
  newTodo: Task = new Task();
  editing: boolean = false;
  editingTodo: Task = new Task();

  constructor(
    private taskService: TaskService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.getTodos();
  }
  LogOut(){
    localStorage.removeItem('token');
    this.router.navigate(['/mauth']);

  }
  getTodos(): void {
    this.taskService.getMyList().subscribe(
      res => {
        this.todos = res.Entities;
      }
    );
  }

  createTodo(todoForm: NgForm): void {
    this.taskService.post(this.newTodo).subscribe(
      res => {
        todoForm.reset();
        this.newTodo = new Task();
        this.todos.unshift(res.Entity)
      }
    );
  }

  deleteTodo(id: string): void {
    this.taskService.delete(id).subscribe(
      res => {
        this.todos = this.todos.filter(todo => todo.Id != id);
      }
    );
  }

  updateTodo(todoData: Task): void {
    this.taskService.update(todoData).subscribe(
      res => {
        let existingTodo = this.todos.find(todo => todo.Id === res.Entity.Id);
        Object.assign(existingTodo, res.Entity);
        this.clearEditing();
      }
    );
  }

  toggleCompleted(todoData: Task): void {
    todoData.IsFinish = !todoData.IsFinish;
    this.taskService.update(todoData).subscribe(
      res => {
        let existingTodo = this.todos.find(todo => todo.Id === res.Entity.Id);
        Object.assign(existingTodo, res.Entity);
        this.clearEditing();
      }
    );
  }

  editTodo(todoData: Task): void {
    this.editing = true;
    Object.assign(this.editingTodo, todoData);
  }

  clearEditing(): void {
    this.editingTodo = new Task();
    this.editing = false;
  }

}
