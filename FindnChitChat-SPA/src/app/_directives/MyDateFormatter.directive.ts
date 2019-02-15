
import { Directive, HostListener, ElementRef, OnInit , Pipe, PipeTransform  } from '@angular/core';

@Directive ({ selector: '[appDateFormatter]' })
export class MyDateFormatterDirective implements OnInit, PipeTransform {

  private el: HTMLInputElement;
  private SEPARATOR: string;

  constructor(
    private elementRef: ElementRef
  ) {
    this.el = this.elementRef.nativeElement;
    this.SEPARATOR = '/';
  }

  ngOnInit() {
    this.el.value = this.transform(this.el.value);
  }

  @HostListener('focus', ['$event.target.value'])
  onFocus(value) {
    this.el.value = this.parse(value); // opossite of transform
  }
  @HostListener('input', ['$event.target.value'])
  onInput(value) {
     this.el.value = this.transform(value);
  }
  transform(value: number | string): string {
    value = value.toString().replace(new RegExp(this.SEPARATOR, 'g'), '');
    value = value.toString().replace(/\B(?=(\d{2})+(\d{2})+(?!\d))/g, this.SEPARATOR);
    return value;
  }
  parse(value: string, fractionSize: number = 2): string {
    value = value.toString().replace(new RegExp(this.SEPARATOR, 'g'), '');
    return value;
  }
}
