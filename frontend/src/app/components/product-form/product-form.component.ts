import { Component, EventEmitter, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ProductService } from '../../services/product.service';
import { CreateProductRequest } from '../../models/product/product';

@Component({
  selector: 'app-product-form',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './product-form.component.html',
  styleUrls: ['./product-form.component.css']
})
export class ProductFormComponent {
  @Output() productAdded = new EventEmitter<void>();

  product: CreateProductRequest = {
    code: '',
    name: '',
    price: 0
  };

  isSubmitting = false;
  errorMessage = '';

  constructor(private productService: ProductService) {}

  onSubmit(): void {
    if (!this.product.code || !this.product.name  ) {
      this.errorMessage = 'Kod i Nazwa są wymagane.';
      return;
    }

    this.isSubmitting = true;
    this.errorMessage = '';

    this.productService.create(this.product).subscribe({
      next: () => {
        this.product = { code: '', name: '', price: 0 };
        this.isSubmitting = false;
        this.productAdded.emit();
      },
      error: (err) => {
        this.errorMessage = err.error || 'Wystąpił błąd podczas dodawania produktu.';
        this.isSubmitting = false;
      }
    });
  }
}
