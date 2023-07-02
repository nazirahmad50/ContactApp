import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { Contact } from '../contact.models';

@Injectable({
  providedIn: 'root',
})
export class ContactService {
  baseUrl = 'http://localhost:5062/api/contacts/';

  constructor(private http: HttpClient) {}

  getContacts(): Observable<Contact[]> {
    return this.http.get<Contact[]>(this.baseUrl);
  }

  getContact(id: number): Observable<Contact> {
    return this.http.get<Contact>(this.baseUrl + id);
  }

  CreateContact(contact: Contact): Observable<Contact> {
    return this.http.post<Contact>(this.baseUrl, contact);
  }

  deleteContact(id: number): Observable<void> {
    return this.http.delete<void>(this.baseUrl + id);
  }

  updateContact(id: number, contact: Contact): Observable<Contact> {
    return this.http.put<Contact>(this.baseUrl + id, contact);
  }
}
