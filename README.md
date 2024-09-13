# Contact Manager

This is a .NET web application that allows users to upload a CSV file containing contact information, store the data in an MS SQL database, and view, edit, delete, search, and sort the contacts. The app also supports inline editing of contact details and client-side filtering and sorting.

## Features

- Upload CSV files with contact details.
- Display, edit, and delete contacts.
- Inline editing for contacts.
- Search and filter contacts by any field.
- Sort contacts by any field.
- Responsive UI for the contact table.

## CSV Format

The CSV file should contain the following fields:
- `Name` (string)
- `DateOfBirth` (date)
- `Married` (boolean)
- `Phone` (string)
- `Salary` (decimal)

## Setup Instructions

### Prerequisites

- .NET 6 SDK or later
- MS SQL Server
- SQL Server Management Studio (SSMS) or Azure Data Studio (optional)
- Node.js (for Angular frontend)

### Backend Setup

1. Clone the repository:
    ```bash
    git clone https://github.com/your-repo/contact-manager.git
    cd contact-manager
    ```

2. Install the necessary dependencies:
    ```bash
    cd ContactManager
    dotnet restore
    ```

3. Update the MS SQL connection string in `appsettings.json`.

4. Run the application:
    ```bash
    dotnet run
    ```

### Frontend Setup (Angular)

1. Navigate to the frontend folder:
    ```bash
    cd ContactManagerClient
    ```

2. Install the dependencies:
    ```bash
    npm install
    ```

3. Run the Angular development server:
    ```bash
    ng serve
    ```

## API Endpoints

- `GET /api/contacts` - Fetch all contacts.
- `POST /api/contacts` - Upload a CSV file to add new contacts.
- `PATCH /api/contacts/{id}` - Update a contact.
- `DELETE /api/contacts/{id}` - Delete a contact.

## Tech Stack

- **Backend**: .NET 6, Entity Framework Core, MS SQL Server
- **Frontend**: Angular
- **Database**: MS SQL Server
