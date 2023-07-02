import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ContactComponent } from './contact.component';
import { HttpClientModule } from '@angular/common/http';
import { ModalModule } from 'ngx-bootstrap/modal';
import { ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from '../shared/shared.module';

@NgModule({
  declarations: [ContactComponent],
  imports: [
    CommonModule,
    HttpClientModule,
    ModalModule.forRoot(),
    ReactiveFormsModule,
    SharedModule,
  ],
  exports: [ContactComponent],
})
export class ContactModule {}
