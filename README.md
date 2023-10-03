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

## Runnig

### 1. Backend
- In Visual Studio, click the `Start` button (or press `F5`) to initiate the backend server.

### Frontend
- Open the terminal in the folder containing the frontend project.
- Run the following command to start the frontend application:
    ```shell
  npm run dev
    ```

## Usage of SecureCode

### 1. Register
- Enter the data in the form and click the button to save it in the database.
- Go to the next page to enter the code.
- A code will be sent to the e-mail address, which should be entered in the field provided for it, and by clicking on the button, you will go to the page for entering the one-time generated code.
- On that page there is a QR code that needs to be scanned in the Google Authenticator application or, alternatively, the key must be entered.
- Enter the generated code and click the button within 30 seconds of generation (after 30 seconds a new one is generated and the old one is no longer valid).
- The account cannot be used (or logged in) until the email address and TOTP code are confirmed.

### 2. Sign up
- Enter the required information and click the button to go to the confirmation page.
- On that page there is a field for entering a one-time generated code in the Google Authenticator application.
- After confirming the code, you will go to the dashboard page, and the user is logged in.
- In case the user is a moderator, he can only read, but not perform actions until the administrator approves his profile.

### 3. Reset your password
- Enter your email address and click the button to go to the next page.
- Enter the generated code and new password in the form.
- By clicking the button, the password is changed and you go to the login page.

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
