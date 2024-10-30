-- Drop the existing schema if needed
DROP SCHEMA public CASCADE;
CREATE SCHEMA public;

-- Create ENUM type for user roles
CREATE TYPE user_role AS ENUM ('Admin', 'Player', 'GameManager');

-- Universes table to categorize planets into different universes
CREATE TABLE Universes (
                           UniverseID SERIAL PRIMARY KEY,
                           Name VARCHAR(100) NOT NULL,
                           Description TEXT
);

-- Users table to store user information and roles
CREATE TABLE Users (
                       UserID SERIAL PRIMARY KEY,
                       UserName VARCHAR(50) UNIQUE NOT NULL,
                       NormalizedUserName VARCHAR(50) UNIQUE NOT NULL,
                       Email VARCHAR(256),
                       NormalizedEmail VARCHAR(256),
                       EmailConfirmed BOOLEAN DEFAULT FALSE,
                       PasswordHash VARCHAR(255) NOT NULL,
                       SecurityStamp VARCHAR(36),
                       ConcurrencyStamp VARCHAR(36) DEFAULT gen_random_uuid(),
                       PhoneNumber VARCHAR(15),
                       PhoneNumberConfirmed BOOLEAN DEFAULT FALSE,
                       TwoFactorEnabled BOOLEAN DEFAULT FALSE,
                       LockoutEnd TIMESTAMP,
                       LockoutEnabled BOOLEAN DEFAULT FALSE,
                       AccessFailedCount INT DEFAULT 0,
                       CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Role table
CREATE TABLE Roles (
                       RoleID SERIAL PRIMARY KEY,
                       Name VARCHAR(50) UNIQUE NOT NULL,
                       NormalizedName VARCHAR(50) UNIQUE NOT NULL
);

-- join table of users and roles 
CREATE TABLE UserRoles (
                           UserID INT NOT NULL,
                           RoleID INT NOT NULL,
                           PRIMARY KEY (UserID, RoleID),
                           FOREIGN KEY (UserID) REFERENCES Users(UserID) ON DELETE CASCADE,
                           FOREIGN KEY (RoleID) REFERENCES Roles(RoleID) ON DELETE CASCADE
);

-- Planets table to represent planets within different universes
CREATE TABLE Planets (
                         PlanetID SERIAL PRIMARY KEY,
                         Name VARCHAR(100) NOT NULL,
                         Description TEXT,
                         UniverseID INT NOT NULL,
                         IsDiscovered BOOLEAN DEFAULT FALSE,
                         FOREIGN KEY (UniverseID) REFERENCES Universes(UniverseID)
);

-- Quests table to define quests associated with specific planets or universes
CREATE TABLE Quests (
                        QuestID SERIAL PRIMARY KEY,
                        Title VARCHAR(100) NOT NULL,
                        Description TEXT,
                        PlanetID INT,
                        UniverseID INT,
                        IsCompleted BOOLEAN DEFAULT FALSE,
                        FOREIGN KEY (PlanetID) REFERENCES Planets(PlanetID),
                        FOREIGN KEY (UniverseID) REFERENCES Universes(UniverseID)
);

-- Scoreboard table to track users' scores
CREATE TABLE Scoreboard (
                            ScoreID SERIAL PRIMARY KEY,
                            UserID INT NOT NULL,
                            Points INT NOT NULL,
                            UpdatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
                            FOREIGN KEY (UserID) REFERENCES Users(UserID)
);

-- Quizzes table for quiz or riddle questions within quests
CREATE TABLE Quizzes (
                         QuizID SERIAL PRIMARY KEY,
                         Question TEXT NOT NULL,
                         Answer VARCHAR(100) NOT NULL,
                         Hint TEXT,
                         QuestID INT,
                         FOREIGN KEY (QuestID) REFERENCES Quests(QuestID)
);

-- UserQuestProgress table to track user progress on quests
CREATE TABLE UserQuestProgress (
                                   ProgressID SERIAL PRIMARY KEY,
                                   UserID INT NOT NULL,
                                   QuestID INT NOT NULL,
                                   IsCompleted BOOLEAN DEFAULT FALSE,
                                   LastUpdated TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
                                   FOREIGN KEY (UserID) REFERENCES Users(UserID),
                                   FOREIGN KEY (QuestID) REFERENCES Quests(QuestID)
);


CREATE EXTENSION IF NOT EXISTS pgcrypto;
