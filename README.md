README.md

# SweetLightSeerScheduler

SweetLightSeerScheduler is a modern Blazor application targeting .NET 9, designed to provide a seamless scheduling experience for tarot readings, integrating the clarity of AI with the energy of Tarot. The application features a calendar interface powered by Radzen components and integrates with Google Calendar for appointment management.

## Features

- **Blazor Server UI**: Built with Blazor for interactive, real-time web experiences.
- **Radzen Scheduler**: Uses Radzen's `RadzenScheduler` and `RadzenWeekView` for a responsive, user-friendly calendar.
- **Google Calendar Integration**: Appointments are managed and synchronized with Google Calendar via a custom service.
- **Notification System**: Users receive real-time feedback on scheduling actions.
- **Dialog-based Appointment Scheduling**: Users can schedule or edit appointments through modal dialogs.

## Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- A Google Cloud project with Calendar API enabled and service account credentials.

## Getting Started

1. **Clone the repository:**
- git clone <repository-url>
- cd SweetLightSeerScheduler

4. **Run the application:**
- dotnet restore
- dotnet build

5. **Access the app:**
- Open your browser and navigate to `https://localhost:5001` (or the port specified in the output).

## Project Structure

- `SweetLightSeerScheduler/Components/Pages/` 
Main Blazor pages and components, including the calendar and scheduling dialogs.
- `SweetLightSeerScheduler/Services/` 
Google Calendar integration and supporting services.
- `Radzen.Blazor/` 
Contains only the Radzen components required for the scheduler (e.g., `AppointmentData.cs`, `Rendering/WeekView.razor`).

## Customization

- **Styling:** 
Modify `wwwroot/app.css` for custom styles.
- **Scheduler Logic:**  
  Update `CalendarScheduler.razor` and related services for custom scheduling logic or integrations.
