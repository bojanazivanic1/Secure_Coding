CREATE TABLE Users (
    Id INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(MAX),
    Password NVARCHAR(MAX),
    Email NVARCHAR(MAX),
    UserRole NVARCHAR(MAX) NOT NULL,
    ModeratorVerifiedAt DATETIME
);

CREATE TABLE Posts (
    Id INT PRIMARY KEY IDENTITY,
    Message NVARCHAR(MAX),
    MessageVerified BIT,
    ContributorId INT,
    FOREIGN KEY (ContributorId) REFERENCES Users(Id) ON DELETE CASCADE
);