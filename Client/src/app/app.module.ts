import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RouterModule, Route } from '@angular/router';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NavComponent } from './components/nav/nav.component';
import { LayoutModule } from '@angular/cdk/layout';
import { MatToolbarModule, MatButtonModule, MatSidenavModule, MatIconModule, MatListModule, MatCardModule, MatInputModule, MatFormFieldModule } from '@angular/material';
import { ChatComponent } from './components/chat/chat.component';
import { FormsModule } from '@angular/forms';

const routes: Route[] = [
    { path: '', component: ChatComponent }
];

@NgModule({
    declarations: [
        AppComponent,
        NavComponent,
        ChatComponent
    ],
    imports: [
        BrowserModule,
        BrowserAnimationsModule,
        FormsModule,
        LayoutModule,
        MatToolbarModule,
        MatButtonModule,
        MatInputModule,
        MatFormFieldModule,
        MatSidenavModule,
        MatIconModule,
        MatListModule,
        MatCardModule,
        RouterModule.forRoot(routes)
    ],
    providers: [],
    bootstrap: [AppComponent]
})
export class AppModule { }
