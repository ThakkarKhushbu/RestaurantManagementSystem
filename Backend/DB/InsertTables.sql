INSERT INTO Tables (Id, TableNumber, Location, SeatingCapacity, IsActive, CreatedAt, UpdatedAt)
VALUES
    (NEWID(), 1, 'Near Window', 2, 1, GETDATE(), GETDATE()),
    (NEWID(), 2, 'Near Door', 4, 1, GETDATE(), GETDATE()),
    (NEWID(), 3, 'Center', 2, 0, GETDATE(), GETDATE()),
    (NEWID(), 4, 'Near Bar', 6, 1, GETDATE(), GETDATE()),
    (NEWID(), 5, 'Near Kitchen', 4, 1, GETDATE(), GETDATE()),
    (NEWID(), 6, 'Near Window', 4, 1, GETDATE(), GETDATE()),
    (NEWID(), 7, 'Near Door', 2, 0, GETDATE(), GETDATE()),
    (NEWID(), 8, 'Outdoor', 6, 1, GETDATE(), GETDATE()),
    (NEWID(), 9, 'Near Exit', 2, 1, GETDATE(), GETDATE()),
    (NEWID(), 10, 'Private Room', 4, 1, GETDATE(), GETDATE());