
# Phone Book Application

This is a simple phone book application implemented in C# using Windows Forms. It allows users to manage contacts stored in a Microsoft Access database (`.mdb`).

## Features

-   **Add Contact**: Add a new contact with first name, last name, and phone number.
-   **Edit Contact**: Modify existing contact details.
-   **Delete Contact**: Remove a contact from the phone book.
-   **Search Contacts**: Search contacts by first name, last name, or phone number.
-   **View All Contacts**: Display all contacts stored in the database.

## Requirements

-   Windows operating system (tested on Windows 10).
-   Microsoft Access Database Engine (to connect to `.mdb` files).

## Installation and Setup

1.  Clone the repository:
    
    bash
    
    Copy code
    
    `git clone https://github.com/meytiii/Phone-Book.git` 
    
2.  Open the project in Visual Studio (compatible with Visual Studio 2019).
    
3.  Build and run the solution (`Final Project.sln`).
    
4.  Make sure the database file (`PhoneBookDB.mdb`) is correctly located in the project directory or update the connection string in `DatabaseHelper.cs` accordingly.
    

## Usage

1.  **Main Form**:
    
    -   Use the buttons to add, edit, delete, or search contacts.
    -   The DataGridView displays all contacts in the database.
2.  **Add/Edit Form**:
    
    -   Enter or modify contact details and click **Save** to save changes.
    -   Click **Cancel** to discard changes and close the form.
3.  **Search Form**:
    
    -   Enter search criteria (partial or complete first name, last name, or phone number) and click **Search** to filter contacts.
    -   Double-click on a row to edit the contact details.
    -   Click **Delete** to remove the selected contact.

## Troubleshooting

-   If encountering database connectivity issues, ensure that the Microsoft Access Database Engine is installed and correctly configured.
-   Verify the path and location of the `PhoneBookDB.mdb` file or update the connection string in `DatabaseHelper.cs`.

## Contributing

Contributions are welcome! Please fork the repository and submit a pull request with your improvements.

## License

This project is licensed under the MIT License. See the LICENSE file for details.
