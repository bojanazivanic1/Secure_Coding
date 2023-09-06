# Secure_Coding

Two projects - demonstrating good and bad practices of secure coding in .NET/C#

## Installation

### 1. Install SQL Server Management Studio

- Install SSMS on your computer from the [official website](https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver16).

### 2. Install Google Authenticator

- Install Google Authenticator on your phone using Google Play or the App Store.

### 3. Installation of Required Packages and Frontend Tools

- Open a terminal in the folder containing the frontend project and run the following command:

  ```shell
  npm install
  ```
    
## Configuration

### 1. SQL Server Configuration

- Create a server in SSMS named `(localdb)\MSSQLLocalDB` or change the server name in the connection string in the `appsettings.json` file.
- In the connection string, you can also change the name of your database before performing migrations.
- In the case of `secure`: In the Package Manager Console within Visual Studio, enter the following commands to create migrations and tables in the database:

  ```shell
  add-migration <migration-name>
  update-database
    ```
- In the case of `insecure`: Execute the queries from `create-tables.sql` on the database in SSMS.


### 2. Port Configuration

If you need to change the port where the server runs, please follow these steps:

1. Open the `lunchSettings.json` file.

2. Locate the `profiles` attribute within the file, and find the `applicationUrl` field.

3. Modify the port number to your desired value (note that the application uses HTTPS).

4. Next, ensure that you set the same port in the frontend part of the application by editing the `.env` file.

5. In the `.env` file, look for the `VITE_LINK` string, and update it with the same port number as in the `lunchSettings.json` file.


## Usage of SecureCode

### 1. Register
- Enter data in the form and click the button to save it to the database.
- Proceed to the next page for entering the code.
- The account cannot be used (or logged in) until the email address is verified.
- A code will be sent to the email address, which should be entered in the designated field, and by clicking the button, you proceed to the login page.
- The profile can then be used.

### 2. Log In
- Enter the required data and click the button to proceed to the confirmation page.
- On that page, there is a QR code that should be scanned within the Google Authenticator app or the key should be entered alternatively.
- Enter the generated code and click the button within 30 seconds of generation (after 30 seconds, a new one is generated, and the old one is no longer valid).
- After that, you will proceed to the dashboard page, and the user is logged in.
- In the case that the user is a moderator, they can only read but not execute actions until an admin approves their profile.

### 3. Reset Password
- Enter the email address, and by clicking the button, you proceed to the next page.
- A QR code and key are displayed, which are used in the Google Authenticator to generate a code.
- Enter the received code and a new password in the form.
- By clicking the button, the password is changed, and you proceed to the login page.

### 4. Contributor's Functionalities
- View approved posts.
- Add new ones, which moderators or admins need to approve before they are displayed.

### 5. Moderator's Functionalities
- View all posts and verify them.

### 6. Admin's Functionalities
- View all posts and can verify them.
- View unverified moderators and can verify them.
- Delete users.

## Usage of InsecureCode

### 1. Register
- Enter data in the form and click the button to save it to the database.
- Proceed to the login page.
- The account can be used immediately.

### 2. Log In
- Enter the required data and click the button to proceed to the dashboard page. The user is logged in.

### 3. Reset Password
- Enter the password in the form.
- By clicking the button, the password is changed, and you proceed to the login page.

### 4. Contributor's Functionalities
- View all posts.
- Add new posts, which are immediately displayed.

### 5. Moderator's Functionalities
- View all posts and verify them.
- Can perform actions even if not verified.

### 6. Admin's Functionalities
- View all posts and can verify them.
- View unverified moderators and can verify them.
- Delete users.

## Testing
- In text input fields, you can enter a malicious database query or a malicious script. In the secure version, they will be blocked, while in the insecure version, they will be executed.
- The password in the insecure version is hashed using the SHA-1 algorithm, which is vulnerable to attacks.
