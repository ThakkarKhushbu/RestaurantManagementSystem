INSERT INTO Reservations (Id, CustomerName, ContactNumber, GuestCount, ReservationDate, FromTime, ToTime, Status, CreatedAt, UpdatedAt, TableId)
VALUES
    (NEWID(), 'John Doe', '555-1234', 2, '2024-12-25', '18:00', '20:00', 1, GETDATE(), GETDATE(), (SELECT Id FROM Tables WHERE TableNumber = 1)),
    (NEWID(), 'Jane Smith', '555-5678', 4, '2024-12-25', '19:00', '21:00', 0, GETDATE(), GETDATE(), (SELECT Id FROM Tables WHERE TableNumber = 2)),
    (NEWID(), 'Robert Brown', '555-9876', 3, '2024-12-26', '12:00', '14:00', 1, GETDATE(), GETDATE(), (SELECT Id FROM Tables WHERE TableNumber = 3)),
    (NEWID(), 'Emily White', '555-1122', 5, '2024-12-26', '13:00', '15:00', 1, GETDATE(), GETDATE(), (SELECT Id FROM Tables WHERE TableNumber = 4)),
    (NEWID(), 'Michael Green', '555-3344', 2, '2024-12-27', '17:30', '19:30', 2, GETDATE(), GETDATE(), (SELECT Id FROM Tables WHERE TableNumber = 5)),
    (NEWID(), 'Linda Blue', '555-6677', 4, '2024-12-27', '18:30', '20:30', 0, GETDATE(), GETDATE(), (SELECT Id FROM Tables WHERE TableNumber = 6)),
    (NEWID(), 'William Black', '555-9988', 2, '2024-12-28', '20:00', '22:00', 1, GETDATE(), GETDATE(), (SELECT Id FROM Tables WHERE TableNumber = 7)),
    (NEWID(), 'Olivia Gray', '555-7766', 6, '2024-12-28', '19:00', '21:00', 1, GETDATE(), GETDATE(), (SELECT Id FROM Tables WHERE TableNumber = 8)),
    (NEWID(), 'Sophia Yellow', '555-4455', 3, '2024-12-29', '12:00', '14:00', 0, GETDATE(), GETDATE(), (SELECT Id FROM Tables WHERE TableNumber = 9)),
    (NEWID(), 'James Red', '555-2233', 4, '2024-12-29', '15:00', '17:00', 1, GETDATE(), GETDATE(), (SELECT Id FROM Tables WHERE TableNumber = 10));