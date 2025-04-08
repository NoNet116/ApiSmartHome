using ApiSmartHome.Contracts.Models.Rooms;
using FluentValidation;

namespace ApiSmartHome.Contracts.Validation
{
    public class EditRoomRequestValidator : AbstractValidator<EditRoomRequest>
    {
        public EditRoomRequestValidator()
        {
            RuleFor(x => x.NewName).NotEmpty();
            RuleFor(x => x.NewArea).Must(x => x >= 0).WithMessage("Не доспускается значение ниже нуля");
            RuleFor(x => x.NewGasConnected).NotNull();
            RuleFor(x => x.NewVoltage).InclusiveBetween(120, 220);

        }
    }
}
