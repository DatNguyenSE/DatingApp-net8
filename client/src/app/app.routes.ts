import { Routes } from '@angular/router';
import { HomeComponent } from '../features/home/home.component';
import { MemberListComponent } from '../features/members/member-list/member-list.component';
import { MemberDetailComponent } from '../features/members/member-detail/member-detail.component';
import { ListsComponent } from '../features/lists/lists.component';
import { MessagesComponent } from '../features/messages/messages.component';
import { authGuard } from '../core/_guards/auth.guard';
import { TestErrors } from '../features/test-error/test-error.component';
import { NotFoundComponent } from '../shared/errors/not-found/not-found.component';
import { ServerErrorComponent } from '../shared/errors/server-error/server-error.component';

    export const routes: Routes = [
    { path: '', component: HomeComponent },
    {
        path: '',                               // start page
        runGuardsAndResolvers: 'always',
        canActivate: [authGuard],
        children: [
            { path: 'members', component: MemberListComponent},
            { path: 'members/:id', component: MemberDetailComponent },
            { path: 'lists', component: ListsComponent },
            { path: 'messages', component: MessagesComponent },

        ]
    },

    { path :'errors', component: TestErrors},
    { path :'server-error', component: ServerErrorComponent},
    { path: '**', component: NotFoundComponent},

];
