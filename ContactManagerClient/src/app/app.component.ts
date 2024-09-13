import { Component, ViewChild } from '@angular/core';
import { ContactTableComponent } from './contact-table/contact-table.component';
import { ContactService } from './services/contact.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  @ViewChild(ContactTableComponent) contactTable!: ContactTableComponent;
  selectedFile: File | null = null;

  constructor(private contactService: ContactService) {}

  onFileSelected(event: any): void {
    const file: File = event.target.files[0];
    if (file) {
      this.selectedFile = file;
    }
  }

  uploadCsv(event: Event): void {
    event.preventDefault();

    if (this.selectedFile) {
      this.contactService.uploadCsv(this.selectedFile).subscribe({
        next: _ => {
          alert('Contacts uploaded successfully');
          this.contactTable.loadContacts();
        },
        error: (error) => {
          console.error('Error uploading contacts', error);
          alert('Error uploading contacts');
        }
      });
    } else {
      alert('Please select a CSV file.');
    }
  }
}
