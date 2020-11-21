import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Member } from 'src/app/models/member.model';
import { AuthService } from 'src/app/services/auth.service';


@Component({
  selector: 'app-mauth',
  templateUrl: './mauth.component.html',
  styleUrls: ['./mauth.component.css']
})
export class MauthComponent implements OnInit {

  model: Member = new Member;
  constructor(private service:AuthService,private toaster: ToastrService,private router: Router) { }

  ngOnInit() {
  }

  Login(form:NgForm) {    
    this.LoginRequest(form.value.Username,form.value.Password);
  }
  LoginRequest(username:string,password:string){
    var body = {
      Password: password,
      Username: username
    };
    this.service.login(body).subscribe(
      (res) => {
        if(res.Meta.IsSuccess){
          this.toaster.success('', 'Success');
          localStorage.setItem('token', res.Entity.Token);
          this.router.navigateByUrl('tasks');
        }else{
          this.toaster.warning('',res.Meta.MessageDetail);
        }  
     
      }
      
    );
  }

  Register(form: NgForm) {
    if(form.value.Password != form.value.RePassword){
      this.toaster.warning('','Password does not match');
    }
    this.model =this.model;
    this.service.register(this.model).subscribe(
      (res) => {
        if(res.Meta.IsSuccess){
          this.toaster.success('', 'Success');
        }else{
          this.toaster.warning('',res.Meta.MessageDetail);
        }      
      }
      
    );
  }


}
