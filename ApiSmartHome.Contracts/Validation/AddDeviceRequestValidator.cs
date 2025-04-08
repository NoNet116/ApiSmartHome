using ApiSmartHome.Contracts.Models.Devices;
using FluentValidation;

namespace ApiSmartHome.Contracts.Validation
{
    /// <summary>
    /// Класс-валидатор запросов подключения
    /// </summary>
    public class AddDeviceRequestValidator : AbstractValidator<AddDeviceRequest>
    {
        /// <summary>
        /// Метод, конструктор, устанавливающий правила
        /// </summary>
        public AddDeviceRequestValidator()
        {
            /* Зададим правила валидации */
            RuleFor(x => x.Name).NotEmpty(); // Проверим на null и на пустое свойство
            RuleFor(x => x.Manufacturer).NotEmpty();
            RuleFor(x => x.Model).NotEmpty();
            RuleFor(x => x.SerialNumber).NotEmpty();
            RuleFor(x => x.CurrentVolts)
                .Must(v => v == 0 || v == 120 || v == 220)
                .WithMessage("Только значения 0, 120 или 220 допустимы.");
            RuleFor(x => x.GasUsage).NotNull();
            RuleFor(x => x.RoomId).NotEmpty()
                .WithMessage($"Please choose locations");
        }

    }
}
