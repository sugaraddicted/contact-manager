import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ContactService } from '../services/contact.service';

@Component({
  selector: 'app-contact-table',
  templateUrl: './contact-table.component.html',
  styleUrls: ['./contact-table.component.css']
})
export class ContactTableComponent implements OnInit {
  contacts: any[] = [];
  editingIndex: number | null = null;
  contactForms: { [key: number]: FormGroup } = {};
  searchTerm: string = '';
  sortField: string = '';
  sortDirection: boolean = true;

  constructor(private contactService: ContactService, private fb: FormBuilder) {}

  ngOnInit(): void {
    this.loadContacts();
  }

  public loadContacts(): void {
    this.contactService.getContacts().subscribe({
      next: (data) => {
        this.contacts = data;

        this.contacts.forEach((contact, index) => {
          this.contactForms[index] = this.fb.group({
            name: [contact.name, [Validators.required]],
            dateOfBirth: [contact.dateOfBirth, [Validators.required]],
            married: [contact.married],
            phone: [contact.phone, [Validators.required, Validators.pattern('^[0-9]{10}$')]],
            salary: [contact.salary, [Validators.required, Validators.min(0)]],
          });
        });
      },
      error: (error) => {
        console.error('Error fetching contacts', error);
      }
    });
  }

  editContact(index: number): void {
    this.editingIndex = index;

    const contact = this.contacts[index];

    this.contactForms[index] = this.fb.group({
      name: [contact.name, [Validators.required]],
      dateOfBirth: [contact.dateOfBirth, [Validators.required]],
      married: [contact.married],
      phone: [contact.phone, [Validators.required, Validators.pattern('^[0-9]{10}$')]],
      salary: [contact.salary, [Validators.required, Validators.min(0)]],
    });
  }

  saveContact(index: number): void {
    const contactForm = this.contactForms[index];

    if (contactForm.invalid) {
      return;
    }

    const updatedContact = {
      ...this.contacts[index],
      ...contactForm.value
    };

    this.contactService.updateContact(updatedContact.id, updatedContact).subscribe({
      next: () => {
        this.editingIndex = null;
        delete this.contactForms[index];
        this.loadContacts();
      },
      error: (error) => {
        console.error('Error saving contact', error);
        this.cancelEdit(index);
      }
    });
  }

  cancelEdit(index: number): void {
    this.editingIndex = null;
    delete this.contactForms[index];
  }

  deleteContact(id: string): void {
    this.contactService.deleteContact(id).subscribe({
      next: () => {
        this.loadContacts(); 
      },
      error: (error) => {
        console.error('Error deleting contact', error);
      }
    });
  }

  filteredAndSortedContacts() {
    let filtered = this.contacts;

    if (this.searchTerm) {
      filtered = filtered.filter(contact => {
        return Object.keys(contact).some(key =>
          String(contact[key]).toLowerCase().includes(this.searchTerm.toLowerCase())
        );
      });
    }

    if (this.sortField) {
      filtered = filtered.sort((a, b) => {
        const fieldA = a[this.sortField];
        const fieldB = b[this.sortField];

        if (fieldA < fieldB) {
          return this.sortDirection ? -1 : 1;
        } else if (fieldA > fieldB) {
          return this.sortDirection ? 1 : -1;
        } else {
          return 0;
        }
      });
    }

    return filtered;
  }

  sortBy(field: string) {
    if (this.sortField === field) {
      this.sortDirection = !this.sortDirection;
    } else {
      this.sortField = field;
      this.sortDirection = true;
    }
  }
}
