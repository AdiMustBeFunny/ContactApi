export interface Contact {
  id?: string;
  name: string;
  surname: string;
  email: string;
  password: string;
  phoneNumber: string;
  birthDate: string
  categoryId: string;
  categoryText: string;
}

export class ContactImpl implements Contact {
    id?: string | undefined;
    name: string;
    surname: string;
    password: string;
    email: string;
    phoneNumber: string;
    birthDate: string;
    categoryId: string;
    categoryText: string;

  constructor(name: string, surname: string, email: string, phoneNumber: string, birthdate: string, categoryId: string, categoryText: string, id?: string | undefined) {
    this.name = name
    this.surname = surname
    this.password = ""
    this.email = email
    this.phoneNumber = phoneNumber
    this.birthDate = birthdate
    this.categoryId = categoryId
    this.categoryText = categoryText
    this.id = id
  }
}
