export interface CategoryDto {
  id: string
  name: string
  parentCategoryId?: string | undefined
}

export class CategoryRootSelectItem {
  text: string
  value: string
  constructor(text: string, value: string) {
    this.text = text
    this.value = value
  }
}

export class CategoryChildSelectItem {
  text: string
  value: string
  parentId: string

  constructor(text: string, value: string, parentId: string) {
    this.text = text
    this.value = value
    this.parentId = parentId
  }
}
