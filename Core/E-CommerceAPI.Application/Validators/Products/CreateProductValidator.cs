using E_CommerceAPI.Application.ViewModels.Products;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Application.Validators.Products
{
    public class CreateProductValidator: AbstractValidator<VM_CreateProduct>
    {
        public CreateProductValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Lutfen Urun Adini Giriniz")
                .MaximumLength(150)
                .MinimumLength(2)
                    .WithMessage("Lutfen minumum 2 maximum 150 karakter olacak sekilde urun ismi giriniz");

            RuleFor(p => p.Stock)          
                .NotNull()
                    .WithMessage("Lutfen sock adedini giriniz")
                .Must(s => s >= 0)
                    .WithMessage("Stok bilgisi negatif olamaz");

            RuleFor(p => p.Price)
                .NotNull()
                    .WithMessage("Lutfen fiyati  giriniz")
                .Must(p => p >= 0)
                    .WithMessage("Fiyat bilgisi negatif olamaz");
        }
    }
}
