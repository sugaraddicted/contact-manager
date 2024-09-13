import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class ContactService {
  private apiUrl = 'https://localhost:7103/api/contacts';

  constructor(private http: HttpClient) {}

  uploadCsv(file: File): Observable<any> {
    const formData = new FormData();
    formData.append('csv', file, file.name);

    return this.http.post(this.apiUrl, formData);
  }
  
  getContacts(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrl);
  }

  updateContact(id: string, updatedContact: any): Observable<any> {
    return this.http.patch(`${this.apiUrl}/${id}`, updatedContact);
  }

  deleteContact(id: string): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}