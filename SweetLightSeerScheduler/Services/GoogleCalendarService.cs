using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Microsoft.Extensions.Logging;
using SweetLightSeerScheduler.Interfaces;
using SweetLightSeerScheduler.Models;

namespace SweetLightSeerScheduler.Services
{
    public class GoogleCalendarService : IGoogleCalendarService
    {
        private readonly IGoogleAuthSettings _googleAuthSettings;

        public GoogleCalendarService(IGoogleAuthSettings googleAuthSettings)
        {
            _googleAuthSettings = googleAuthSettings;
        }

        private ServiceAccountCredential GetCredential()
        {
            return new ServiceAccountCredential(
                    new ServiceAccountCredential.Initializer(_googleAuthSettings.ClientEmail)
                    {
                        Scopes = _googleAuthSettings.Scope
                    }.FromPrivateKey(_googleAuthSettings.PrivateKey));
        }

        public async Task<Result<List<Appointment>>> GetAppointments()
        {
            try
            {
                var services = new CalendarService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = GetCredential(),
                    ApplicationName = _googleAuthSettings.ApplicationName,
                });

                var readingRequest = services.Events.List(_googleAuthSettings.ReadingId);

                readingRequest.TimeMinDateTimeOffset = new DateTimeOffset(DateTime.Now);

                var readingAppointments = await readingRequest.ExecuteAsync(CancellationToken.None);


                var availableAppointments = new List<Appointment>();

                foreach (var appointment in readingAppointments.Items)
                {
                    if (appointment.Start.DateTimeDateTimeOffset > DateTime.Now
                        && appointment.Summary == null)
                    {
                        availableAppointments.Add(new()
                        {
                            Summary = $"Available \n {appointment.Start.DateTime.Value.ToString("hh:mm tt")}",
                            Start = appointment.Start.DateTime ?? DateTime.Parse(appointment.Start.Date),
                            End = appointment.End.DateTime ?? DateTime.Parse(appointment.End.Date),
                            EventId = appointment.Id,
                        });
                    }
                }

                return Result<List<Appointment>>.Success(availableAppointments);
            }
            catch
            {
                return Result<List<Appointment>>.Failure();
            }

        }

        public async Task<Result<bool>> ScheduleAppointment(Appointment app)
        {
            try
            {
                var services = new CalendarService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = GetCredential(),
                    ApplicationName = _googleAuthSettings.ApplicationName,
                });

                var eventToUpdate = await services.Events.Get(_googleAuthSettings.ReadingId, app.EventId).ExecuteAsync(CancellationToken.None);
                
                if (eventToUpdate.Status == "cancelled" || eventToUpdate.Description!=null)
                { 
                    throw new Exception(); 
                }

                eventToUpdate.Summary = $"Reading request with {app.FirstName}";
                eventToUpdate.Description = $"{app.FirstName} {app.LastName}\n Email: {app.Email}\n Phone: {app.Phone} \n\n {app.Comment}";


                var readingRequest = services.Events.Update(eventToUpdate, _googleAuthSettings.ReadingId, app.EventId);

                var readingAppointments = await readingRequest.ExecuteAsync(CancellationToken.None);

                return Result<bool>.Success(true);
            }
            catch
            {
                return Result<bool>.Failure();
            }
        }
    }
}
