using FluentValidation;
using microservice.core.DTOs.Product.Requests;
using microservice.core.Interfaces.Repositories;

namespace microservice.core.Validators.Product;

public class ProductAddDtoValidators : AbstractValidator<ProductAddDtoRequest>
{
    private readonly IUnitOfWork _unitOfWork;
    public ProductAddDtoValidators(
        IUnitOfWork unitOfWork
        )
    {
        _unitOfWork = unitOfWork;

        RuleFor(x => x.CodigoProducto)
        .NotEmpty().WithMessage("El campo {PropertyName} no puede ser vacío.")
        .NotNull().WithMessage("El campo {PropertyName}  es requerido.")    
        ;

        RuleFor(x => x.Nombre)
        .NotEmpty().WithMessage("El campo {PropertyName} no puede ser vacío.")
        .NotNull().WithMessage("El campo {PropertyName}  es requerido.")
        .MinimumLength(2).WithMessage("El campo {PropertyName} debe  tener minimo 2 caracteres.")
        .MaximumLength(50).WithMessage("El campo {PropertyName} debe  tener un maximo de caracteres de 50.")
        .Must(NombreExistsAsync)
             .WithMessage("El campo {PropertyName} que incluye el valor {PropertyValue} ya existe en el sistemas.")
        ;
        
        RuleFor(x => x.Descripcion)
        .NotEmpty().WithMessage("El campo {PropertyName} no puede ser vacío.")
        .NotNull().WithMessage("El campo {PropertyName}  es requerido.")
        .MinimumLength(2).WithMessage("El campo {PropertyName} debe  tener minimo 2 caracteres.")
        .MaximumLength(200).WithMessage("El campo {PropertyName} debe  tener un maximo de caracteres de 50.")
        ;

        RuleFor(x => x.FechaCreacion)
        .NotEmpty().WithMessage("El campo {PropertyName} no puede ser vacío.")
        .NotNull().WithMessage("El campo {PropertyName}  es requerido.")
        .LessThan(DateTime.Today).WithMessage("No puede ingresar una fecha de creación en el presente o futuro.")
        ;
    }

    public bool NombreExistsAsync(string nombre) => !_unitOfWork.ProductRepositoryAsync
                       .GetExists(x => x.Nombre!.Trim().ToUpper()
                                                     .Equals(nombre.Trim().ToUpper())
                                  );

}
