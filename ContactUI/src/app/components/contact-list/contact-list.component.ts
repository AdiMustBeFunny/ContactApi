import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ContactListItem } from '../../models/ContactListItem'
import { AuthService } from '../../services/auth.service';
@Component({
  selector: 'app-contact-list',
  templateUrl: './contact-list.component.html',
  styleUrls: ['./contact-list.component.css']
})
export class ContactListComponent implements OnInit {
  public contacts?: ContactListItem[];

  constructor(private http: HttpClient, public authService: AuthService) { }

  ngOnInit(): void {
    this.http.get<ContactListItem[]>('https://localhost:5001/api/Contact/all').subscribe(result => {
      this.contacts = result

      if (this.authService.isUserAuthenticated()) {
        this.contacts.forEach(c => {
          c.editable = true;
        })
      }
    }, error => console.error(error))
  }

  removeContact(contactId: string) {
    this.http.post(`https://localhost:5001/api/Contact/remove?id=${contactId}`, {}).subscribe(result => {
      console.log(result)
      this.contacts = this.contacts?.filter(c => c.id != contactId)
    }, error => {
      console.error(error)
    })
  }

  logout() {
    this.authService.logOut();
  }
}
