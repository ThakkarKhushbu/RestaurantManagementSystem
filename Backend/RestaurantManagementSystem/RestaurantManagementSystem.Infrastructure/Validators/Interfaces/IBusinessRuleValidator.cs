namespace RestaurantManagementSystem.Infrastructure.Validators.Interfaces
{
    public interface IBusinessRuleValidator
    {
        Task ValidateTableAvailabilityAsync(Guid tableId, DateOnly date, TimeOnly fromTime, TimeOnly toTime);

        Task ValidateTableCapacityAsync(Guid tableId, int guestCount);

        Task ValidateReservationCancellationAsync(Guid reservationId);

        void ValidateReservationTimeAsync(DateOnly date, TimeOnly fromTime, TimeOnly toTime);
    }
}
