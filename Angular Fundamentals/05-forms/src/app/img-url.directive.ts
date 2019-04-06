import { Directive, ElementRef, HostListener } from '@angular/core';
import { NgForm } from '@angular/forms';

@Directive({
  selector: '[appImgUrl]'
})
export class ImgUrlDirective {
  constructor(private elRef: ElementRef, private form: NgForm) {}

  @HostListener('input')
  inputHandle() {
    const element = this.elRef.nativeElement;
    const value: string = element.value;
    const hasValidState = value.startsWith('http') && (value.endsWith('.jpg') || value.endsWith('png'));
    this.form.control.setErrors(hasValidState ? null : { image: true });
    this.elRef.nativeElement.style.borderColor = hasValidState ? 'green' : 'red';
  }
}
