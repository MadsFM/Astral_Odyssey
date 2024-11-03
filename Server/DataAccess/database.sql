-- UserRoles join table with cascade delete on both foreign keys
CREATE TABLE UserRoles (
                           UserID INT NOT NULL,
                           RoleID INT NOT NULL,
                           CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
                           PRIMARY KEY (UserID, RoleID),
                           FOREIGN KEY (UserID) REFERENCES Users(UserID) ON DELETE CASCADE,
                           FOREIGN KEY (RoleID) REFERENCES Roles(RoleID) ON DELETE CASCADE
);

-- Planets table
CREATE TABLE Planets (
                         PlanetID SERIAL PRIMARY KEY,
                         Name VARCHAR(100) NOT NULL,
                         Description TEXT,
                         UniverseID INT NOT NULL,
                         IsDiscovered BOOLEAN DEFAULT FALSE,
                         FOREIGN KEY (UniverseID) REFERENCES Universes(UniverseID) ON DELETE CASCADE
);

-- Quests table 
CREATE TABLE Quests (
                        QuestID SERIAL PRIMARY KEY,
                        Title VARCHAR(100) NOT NULL,
                        Description TEXT,
                        PlanetID INT,
                        UniverseID INT,
                        IsCompleted BOOLEAN DEFAULT FALSE,
                        FOREIGN KEY (PlanetID) REFERENCES Planets(PlanetID) ON DELETE CASCADE, 
                        FOREIGN KEY (UniverseID) REFERENCES Universes(UniverseID) ON DELETE CASCADE 
);

-- Scoreboard table 
CREATE TABLE Scoreboard (
                            ScoreID SERIAL PRIMARY KEY,
                            UserID INT NOT NULL,
                            Points INT NOT NULL,
                            UpdatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
                            FOREIGN KEY (UserID) REFERENCES Users(UserID) ON DELETE CASCADE 
);

-- UserQuestProgress table 
CREATE TABLE UserQuestProgress (
                                   ProgressID SERIAL PRIMARY KEY,
                                   UserID INT NOT NULL,
                                   QuestID INT NOT NULL,
                                   IsCompleted BOOLEAN DEFAULT FALSE,
                                   LastUpdated TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
                                   FOREIGN KEY (UserID) REFERENCES Users(UserID) ON DELETE CASCADE, 
                                   FOREIGN KEY (QuestID) REFERENCES Quests(QuestID) ON DELETE CASCADE 
);
