import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormGroup } from '@angular/forms';

@Component({
  selector: 'app-modal',
  templateUrl: './modal.component.html',
  styleUrls: ['./modal.component.scss'],
})
export class ModalComponent {
  @Input() title: string = '';
  @Input() formGroup: FormGroup = new FormGroup({});
  @Output() hideModal = new EventEmitter<void>();
  @Output() formSubmit = new EventEmitter<void>();

  hideModalOnClose() {
    this.hideModal.emit();
  }

  submitForm() {
    this.formSubmit.emit();
  }
}
