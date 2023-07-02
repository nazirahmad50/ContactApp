import { Component, OnInit, TemplateRef } from '@angular/core';
import { ContactService } from './contact.service';
import { Contact } from '../contact.models';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FormBuilder, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-contact',
  templateUrl: './contact.component.html',
  styleUrls: ['./contact.component.scss'],
})
export class ContactComponent implements OnInit {
  contacts: Contact[] = [];
  modalRef?: BsModalRef;

  form = this.fb.group({
    id: new FormControl<number>(0),
    name: new FormControl<string>('', [Validators.required]),
    phoneNumber: new FormControl<number>(0, [Validators.required]),
  });

  constructor(
    private contactService: ContactService,
    private modalService: BsModalService,
    private fb: FormBuilder
  ) {}

  ngOnInit(): void {
    this.contactService.getContacts().subscribe(
      (res) => (this.contacts = res),
      (error) => console.error('Failed to load contacts:', error)
    );
  }

  openModal(template: TemplateRef<any>, id: number | undefined) {
    if (id != null) {
      this.contactService.getContact(id).subscribe(
        (contact) => {
          this.form.patchValue({
            id: contact.id,
            name: contact.name,
            phoneNumber: contact.phoneNumber,
          });
        },
        (error) => console.error('Failed to load contact:', error)
      );
    } else {
      this.form.reset();
    }
    this.modalRef = this.modalService.show(template);
  }

  populateContact() {
    if (this.form.value.id != null) {
      this.updateContact();
    } else {
      this.createContact();
    }
  }

  createContact() {
    if (this.form.valid) {
      const contact: Contact = {
        name: this.form.value.name,
        phoneNumber: this.form.value.phoneNumber,
      } as Contact;

      this.contactService.CreateContact(contact).subscribe(
        (c) => this.contacts.push(c),
        (error) => console.error('Failed to create contact:', error)
      );
      this.modalRef?.hide();
    }
  }

  updateContact() {
    if (this.form.valid) {
      const index: number = this.contacts.findIndex(
        (user: Contact) => user.id === this.form.value.id
      );

      if (index !== -1) {
        const id = this.form.value.id;
        const contact = this.form.value as Contact;

        this.contactService.updateContact(id!, contact).subscribe(
          (c) => (this.contacts[index] = c),
          (error) => console.error('Failed to update contact:', error)
        );
      }

      this.modalRef?.hide();
    }
  }

  deleteContact(id: number) {
    if (confirm('Are you sure you want to delete this contact?')) {
      const contact = this.contacts.findIndex((c) => c.id === id);

      if (contact !== -1) {
        this.contactService.deleteContact(id).subscribe(
          () => this.contacts.splice(contact, 1),
          (error) => console.error('Failed to delete contact:', error)
        );
      } else {
        alert('Contact not found');
      }
    }
  }
}
