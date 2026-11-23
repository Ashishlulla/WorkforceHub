import { Injectable, signal } from "@angular/core";

@Injectable({
    providedIn:'root'
})


export class PageTitleService{

    pageTitle = signal<string>('dashBoard');
    subTitle = signal<string>('Dashboard  subtitle');

    setPageTitle(pageTitle: string, subTitle: string): void{

        this.pageTitle.set(pageTitle);
        this.subTitle.set(subTitle);
    }
}