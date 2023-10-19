import { Directive, ElementRef, Input, ComponentFactoryResolver, ViewContainerRef, Renderer2 } from '@angular/core';


@Directive({
  selector: '[appButtonLoader]'
})
export class ButtonLoaderDirective {
  progressElement: any;

  @Input() set appButtonLoader(value: boolean) {
    debugger;
    //this.toggle(value);
  }

  constructor() {
    this.loadComponent();
  }

  loadComponent() {
    debugger;
    
  }

  toggle(condition: boolean) {
   // condition ? this.show() : this.hide()
  }

  show() {
    //this.progressElement.style.opacity = '0.7';
    //this.matButton.disabled = true;
  }

  hide() {
    //this.progressElement.style.opacity = '0';
    //this.matButton.disabled = false;
  }

}