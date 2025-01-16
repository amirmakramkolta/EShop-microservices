using FluentValidation;

namespace Ordering.Application.Orders.Commands.CreateOrder
{
    public record CreateOrderCommand(OrderDto Order) : ICommand<CreateOrderResult>;
    public record CreateOrderResult(Guid Id);

    public class CreateOrderValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderValidator()
        {
            RuleFor(x=>x.Order.OrderName).NotEmpty().WithMessage("Name is required");
            RuleFor(x=>x.Order.CustomerId).NotNull().WithMessage("Customer id is required");
            RuleFor(x=>x.Order.OrderItems).NotEmpty().WithMessage("OrderItem should not be Empty");
        }
    }
}
