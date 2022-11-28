import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(private router: Router, private http: HttpClient, private authService: AuthService) { }
  ngOnInit(): void {
    if (this.authService.isUserAuthenticated()) {
      this.router.navigate(["/"]);
    }
    }

  login(form: NgForm) {
    const credentials = {
      username: form.value.username,
      password: form.value.password
    }

    this.http.post("https://localhost:5001/api/Auth/login", credentials)
      .subscribe(response => {
        const token = (<any>response).token;
        localStorage.setItem('jwt', token);
        this.router.navigate(["/"]);
      }, err => {
        console.log(err);
      });

  }
}
