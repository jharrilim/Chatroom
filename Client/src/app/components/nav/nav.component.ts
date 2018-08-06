import { Component, ViewChild, Renderer2, ElementRef, OnInit, Input } from '@angular/core';
import { BreakpointObserver, Breakpoints, BreakpointState } from '@angular/cdk/layout';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { UserService } from '../../services/user.service';

@Component({
    selector: 'app-nav',
    templateUrl: './nav.component.html',
    styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

    private name: string = '';

    @ViewChild('mainContent')
    private main: ElementRef;

    @ViewChild('navTop')
    private navTop: ElementRef;

    @Input()
    public set nameField(name: string) {
        this.userService.setUser(name);
        this.name = name;
    }

    constructor (
        private breakpointObserver: BreakpointObserver,
        private renderer: Renderer2, 
        private userService: UserService
    ) { }

    public ngOnInit() { }

    isHandset$: Observable<boolean> = this.breakpointObserver.observe(Breakpoints.Handset)
        .pipe(
            map(result => result.matches)
        );

    public updateName() {
        console.log(name);
        this.userService.setUser(this.nameField);
    }
}
