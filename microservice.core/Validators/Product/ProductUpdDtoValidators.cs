using FluentValidation;
using microservice.core.DTOs.Product.Requests;
using microservice.core.Interfaces.Repositories;

namespace microservice.core.Validators.Product;

public class ProductUpdDtoValidators : AbstractValidator<ProductUpdDtoRequest>
{
    private readonly IUnitOfWork _unitOfWork;
    public ProductUpdDtoValidators(
        IUnitOfWork unitOfWork
        )
    {
        _unitOfWork = unitOfWork;

        RuleFor(x => x.CodigoProducto)
        .NotEmpty().WithMessage("El campo {PropertyName} no puede ser vacío.")
        .NotNull().WithMessage("El campo {PropertyName}  es requerido.")
        .Must(CodigoProductoExistsAsync)
             .WithMessage("El campo {PropertyName} que incluye el valor {PropertyValue} ya existe en el sistemas.")
        ;

        RuleFor(x => x.Descripcion)
        .NotEmpty().WithMessage("El campo {PropertyName} no puede ser vacío.")
        .NotNull().WithMessage("El campo {PropertyName}  es requerido.")
        .MinimumLength(2).WithMessage("El campo {PropertyName} debe  tener minimo 2 caracteres.")
        .MaximumLength(200).WithMessage("El campo {PropertyName} debe  tener un maximo de caracteres de 50.")
        ;
    }

    public bool CodigoProductoExistsAsync(int codigoProducto) => !_unitOfWork.ProductRepositoryAsync
                       .GetExists(x => x.CodigoProducto
                                                     .Equals(codigoProducto)
                       );
}