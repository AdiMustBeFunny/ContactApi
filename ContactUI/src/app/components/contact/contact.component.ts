import { HttpClient } from '@angular/common/http';
import { Component, Input, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CategoryDto, CategoryRootSelectItem, CategoryChildSelectItem } from '../../models/CategoryDto';
import { Contact, ContactImpl } from '../../models/Contact';

@Component({
  selector: 'app-contact',
  templateUrl: './contact.component.html',
  styleUrls: ['./contact.component.css']
})
export class ContactComponent implements OnInit {

  public contact: Contact;
  public editable: string;
  public categories?: CategoryDto[];
  public customCategoryId?: string;
  public selectedSubcategoryId?: string;

  public categoryRootSelectItems: CategoryRootSelectItem[];
  public categoryChildSelectItems: CategoryChildSelectItem[];

  public errors: any = {
    name: null,
    surname: null,
    email: null,
    password: null,
    phoneNumber: null,
    birthDate: null,
    categoryId: null,
    categoryText: null
  }

  constructor(private router: Router, private http: HttpClient, private _activatedRoute: ActivatedRoute) {
    this.editable = 'false';
    this.contact = new ContactImpl("", "", "", "", "2012-01-01", "", "", undefined)
    this.categoryRootSelectItems = []
    this.categoryChildSelectItems = []
  }
  

  ngOnInit(): void {
    this.http.get<CategoryDto[]>('https://localhost:5001/api/Category/all').subscribe(result => {
      this.categories = result;

      this.categories.forEach(category => {
        if (!category.parentCategoryId) {
          this.categoryRootSelectItems.push(new CategoryRootSelectItem(category.name, category.id))
        }
      })

      this._activatedRoute.params.subscribe(params => {


        console.log()
        if ((<any>params).hasOwnProperty('edit')) {
          this.editable = (<any>params).edit
        } else {
          this.editable = 'true'
        }

        if ((<any>params).id) {
          this.http.get<Contact>(`https://localhost:5001/api/Contact?id=${(<any>params).id}`).subscribe(result => {
            this.contact = result;

            let dateRaw = result.birthDate.toString()
            let dateLastIndex = dateRaw.indexOf("T")
            let date = dateRaw.substr(0, dateLastIndex)
            this.contact.birthDate = date;

            if (this.categoryRootSelectItems.filter(category => category.value == this.contact.categoryId).length == 0) {
              this.selectedSubcategoryId = this.contact.categoryId
              let haxSubcategoryId = this.contact.categoryId
              let selectedSubcategory: CategoryDto

              this.categories?.forEach(category => {
                if (category.id == this.selectedSubcategoryId) {
                  selectedSubcategory = category
                }
              })

              this.categories?.forEach(category => {
                if (category.id == selectedSubcategory.parentCategoryId) {
                  this.contact.categoryId = category.id
                }
              })

              this.changeSubcategories()
              //this is a hax :)
              this.selectedSubcategoryId = haxSubcategoryId;
            }

          }, error => console.error(error))
        }

      })

    }, error => console.error(error))

    this.http.get<any>('https://localhost:5001/api/Category/getCustomCategoryId').subscribe(result => {
      this.customCategoryId = (<any>result).id;
    }, error => console.error(error))
  }

  changeSubcategories() {
    this.categoryChildSelectItems = []

    this.categories?.forEach(category => {
      if (category.parentCategoryId == this.contact.categoryId) {
        this.categoryChildSelectItems.push(new CategoryChildSelectItem(category.name, category.id, category.parentCategoryId))
      } 
    })

    if (this.categoryChildSelectItems.length > 0) {
      this.categoryChildSelectItems = [new CategoryChildSelectItem("Select subcategory", "", ""), ...this.categoryChildSelectItems]
      this.selectedSubcategoryId = "";
    }
  }

  hideSubcategories(): boolean {
    if (this.categories?.filter(category => category.parentCategoryId == this.contact.categoryId).length == 0) {
      return true;
    }
    return false;
  }

  submit() {
    if (this.editable == 'false')
      return;

    if (!this.contact.password) {
      this.errors.password = 'Password cannot be empty'
      return;
    }

    let categoryId: string | undefined = this.contact.categoryId;
    if (this.categoryChildSelectItems.filter(c => c.value == this.selectedSubcategoryId)) {
      if (this.selectedSubcategoryId)
      categoryId = this.selectedSubcategoryId 
    }

    this.clearErrors()
    if (this.contact.id) {
      this.http.post(`https://localhost:5001/api/Contact/edit`, {
        id: this.contact.id,
        name: this.contact.name,
        surname: this.contact.surname,
        email: this.contact.email,
        password: this.contact.password,
        phoneNumber: this.contact.phoneNumber,
        birthDate: this.contact.birthDate,
        categoryId: categoryId,
        categoryText: this.contact.categoryText
      }).subscribe(result => {
        this.router.navigate(["/"]);

      }, err => {
        this.handleResponseError(err)
      })
      return;
    }
    this.http.post(`https://localhost:5001/api/Contact/create`, {
      name: this.contact.name,
      surname: this.contact.surname,
      email: this.contact.email,
      password: this.contact.password,
      phoneNumber: this.contact.phoneNumber,
      birthDate: this.contact.birthDate,
      categoryId: categoryId,
      categoryText: this.contact.categoryText
    }, {}).subscribe(result => {
      this.router.navigate(["/"]);

    }, err => {
      this.handleResponseError(err)
    })

  }

  handleResponseError(err: any) {
    if (err) {
      if (err.error) {
        if (err.error.fieldErrors)//hehe
        {
          var error = err.error.fieldErrors
          if (error.Name) {
            this.errors.name = error.Name
          }
          if (error.Surname) {
            this.errors.surname = error.Surname
          }
          if (error.Email) {
            this.errors.email = error.Email
          }
          if (error.PhoneNumber) {
            this.errors.phoneNumber = error.PhoneNumber
          }
          if (error.Password) {
            this.errors.password = error.Password
          }
          if (error.BirthDate) {
            this.errors.birthDate = error.BirthDate
          }
          if (error.CategoryId) {
            this.errors.categoryId = error.CategoryId
          }
          if (error.CategoryText) {
            this.errors.categoryText = error.CategoryText
          }
        }
      }
    }
  }
  clearErrors() {
    Object.keys(this.errors).forEach(key => { this.errors[key] = null });
  }
  inputChanged(field: string) {
    this.errors[field] = null;
  }
}
