import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { first } from 'rxjs';
import { AuthenticationService } from './authentication.service';
import { User } from './usuarioEntity';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
 // encapsulation: ViewEncapsulation.None
})
export class LoginComponent implements OnInit {

  usuario: User = new User();
  blockedDocument: boolean = false;
  returnUrl: string = '';


  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private authenticationService: AuthenticationService) { }

  ngOnInit() {
    // get return url from route parameters or default to '/'
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
  }

  fazerLogin() {

    this.blockedDocument = true;

    this.authenticationService.login(this.usuario.username!, this.usuario.password!)
      .pipe(first())
      .subscribe({
        next: (r) => {
          this.blockedDocument = false; 
          this.router.navigate(['/']);
        },
        error: error => {
          this.blockedDocument = false; 
          //LimparToken
        }
      });

    //this.authService.fazerLogin({ usuario: this.usuario });
  }

}
