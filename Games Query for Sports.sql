INSERT INTO Sports.Game (GameId, HomeTeamID, VisitorTeamID, DateTime, FieldId, HomeScore, VisitorScore, StatusId)
VALUES 
(NEWID(), 1, 2, '2024-08-06 18:00:00', 1, 4, 3, 2),  -- Squirrels vs Goats
(NEWID(), 2, 1, '2024-08-06 19:00:00', 1, 7, 3, 2),  -- Goats vs Squirrels
(NEWID(), 3, 4, '2024-08-13 20:00:00', 1, 8, 4, 2),  -- Squirrels vs Sea Wolves
(NEWID(), 8, 1, '2024-08-13 21:00:00', 1, 8, 7, 2),  -- Sea Wolves vs Squirrels
(NEWID(), 9, 3, '2024-08-20 20:00:00', 1, 8, 4, 2),  -- Squirrels vs Rubber Ducks
(NEWID(), 10, 1, '2024-08-20 21:00:00', 1, 4, 7, 2),  -- Rubber Ducks vs Squirrels
(NEWID(), 12, 2, '2024-08-27 20:00:00', 1, 7, 4, 2),  -- Squirrels vs Goats
(NEWID(), 13, 1, '2024-08-27 21:00:00', 1, 4, 7, 2),  -- Goats vs Squirrels
(NEWID(), 14, 2, '2024-09-03 20:00:00', 1, NULL, NULL, 3),  -- Squirrels vs Goats
(NEWID(), 15, 1, '2024-09-03 21:00:00', 1, NULL, NULL, 3),  -- Goats vs Squirrels
(NEWID(), 18, 4, '2024-09-10 18:30:00', 1, NULL, NULL, 1),  -- Squirrels vs Sea Wolves
(NEWID(), 19, 1, '2024-09-10 19:30:00', 1, NULL, NULL, 1),  -- Sea Wolves vs Squirrels
(NEWID(), 24, 4, '2024-09-17 20:00:00', 1, NULL, NULL, 1),  -- Squirrels vs Sea Wolves
(NEWID(), 25, 1, '2024-09-17 21:00:00', 1, NULL, NULL, 1),  -- Sea Wolves vs Squirrels
(NEWID(), 26, 3, '2024-09-24 20:00:00', 1, NULL, NULL, 1),  -- Squirrels vs Rubber Ducks
(NEWID(), 27, 1, '2024-09-24 21:00:00', 1, NULL, NULL, 1),  -- Rubber Ducks vs Squirrels
(NEWID(), 32, 3, '2024-10-01 18:30:00', 1, NULL, NULL, 1),  -- Squirrels vs Rubber Ducks
(NEWID(), 33, 1, '2024-10-01 19:30:00', 1, NULL, NULL, 1),  -- Rubber Ducks vs Squirrels
(NEWID(), 34, 4, '2024-10-08 20:00:00', 1, NULL, NULL, 1),  -- Squirrels vs Sea Wolves
(NEWID(), 35, 1, '2024-10-08 21:00:00', 1, NULL, NULL, 1),  -- Sea Wolves vs Squirrels
(NEWID(), 38, 2, '2024-10-15 20:00:00', 1, NULL, NULL, 1),  -- Squirrels vs Goats
(NEWID(), 39, 1, '2024-10-15 21:00:00', 1, NULL, NULL, 1),  -- Goats vs Squirrels
(NEWID(), 44, 4, '2024-10-22 18:30:00', 1, NULL, NULL, 1),  -- Squirrels vs Sea Wolves
(NEWID(), 45, 1, '2024-10-22 19:30:00', 1, NULL, NULL, 1),  -- Sea Wolves vs Squirrels
(NEWID(), 48, 3, '2024-10-29 18:30:00', 1, NULL, NULL, 1),  -- Squirrels vs Rubber Ducks
(NEWID(), 49, 1, '2024-10-29 19:30:00', 1, NULL, NULL, 1),  -- Rubber Ducks vs Squirrels
(NEWID(), 52, 3, '2024-11-05 18:30:00', 1, NULL, NULL, 1),  -- Squirrels vs Rubber Ducks
(NEWID(), 53, 1, '2024-11-05 19:30:00', 1, NULL, NULL, 1),  -- Rubber Ducks vs Squirrels
(NEWID(), 54, 4, '2024-11-12 20:00:00', 1, NULL, NULL, 1),  -- Squirrels vs Sea Wolves
(NEWID(), 55, 1, '2024-11-12 21:00:00', 1, NULL, NULL, 1),  -- Sea Wolves vs Squirrels
(NEWID(), 58, 4, '2024-11-19 18:30:00', 1, NULL, NULL, 1),  -- Rubber Ducks vs Sea Wolves
(NEWID(), 59, 1, '2024-11-19 18:30:00', 1, NULL, NULL, 1),  -- Goats vs Squirrels
(NEWID(), 64, 3, '2024-11-19 20:00:00', 1, NULL, NULL, 1),  -- Sea Wolves vs Rubber Ducks
(NEWID(), 65, 2, '2024-11-19 20:00:00', 1, NULL, NULL, 1),  -- Squirrels vs Goats

-- Games on 11/21/2024 (No Squirrels game listed on this date, skipping)

-- Games on 11/26/2024
(NEWID(), 66, 1, '2024-11-26 18:30:00', 1, NULL, NULL, 1, 'Scheduled'),  -- Rubber Ducks vs Squirrels
(NEWID(), 67, 4, '2024-11-26 18:30:00', 1, NULL, NULL, 1, 'Scheduled')  -- Goats vs Sea Wolves