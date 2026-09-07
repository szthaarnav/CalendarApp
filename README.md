# Calendar Management App

A robust desktop application built with C# and WPF, demonstrating full CRUD operations, date-based filtering, and a strict MVVM (Model-View-ViewModel) architecture. 

## Features
* **Full CRUD Functionality:** Create, Read, Update, and Delete calendar entries.
* **Date-Based Filtering:** Filter the viewable dashboard by specific start and end dates using LINQ.
* **Data Persistence:** Automatically saves and loads state using a local `calendar_data.json` file.
* **Clean Architecture:** Strict separation of data, business logic, and UI rendering.

## Tech Stack
* **Language:** C#
* **Framework:** .NET (WPF)
* **Data Storage:** System.Text.Json

## Codebase Walkthrough
The project structure separates concerns into five distinct layers, mirroring backend API design patterns:
* **Models:** Contains `CalendarEntry.cs`, defining the core data entity with a unique `Guid`.
* **Repositories:** Contains `JsonCalendarRepository.cs`, handling direct file I/O and JSON serialization.
* **Services:** Contains `CalendarService.cs`, acting as the business layer to enforce validation rules before data is saved.
* **ViewModels:** Contains `MainViewModel.cs` and `EntryViewModel.cs`. These manage UI state, intercept user commands, and securely bind data to the views via `INotifyPropertyChanged`.
* **Views:** Contains the visual XAML files (`MainWindow.xaml`, `EntryWindow.xaml`).

## Getting Started
1. Clone the repository.
2. Open `CalenderApp.sln` in Visual Studio.
3. Press `F5` to build and run the application. The `calendar_data.json` file will automatically generate in the local output directory upon first launch.