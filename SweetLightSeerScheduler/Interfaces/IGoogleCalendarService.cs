using SweetLightSeerScheduler.Models;

namespace SweetLightSeerScheduler.Interfaces
{
    public interface IGoogleCalendarService
    {
        public Task<Result<List<Appointment>>> GetAppointments();
        public Task<Result<bool>> ScheduleAppointment(Appointment app);
    }
}
