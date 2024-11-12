CREATE VIEW vw_ReservationsWithCustomerAndRestaurant AS
SELECT 
    r.ReservationId,
    r.ReservationDate,
    r.PartySize,
    c.CustomerId,
    c.FirstName AS CustomerFirstName,
    c.LastName AS CustomerLastName,
    c.Email AS CustomerEmail,
    rest.RestaurantId,
    rest.Name AS RestaurantName,
    rest.Address AS RestaurantAddress
FROM Reservations r
JOIN Customers c ON r.CustomerId = c.CustomerId
JOIN Restaurants rest ON r.RestaurantId = rest.RestaurantId;


CREATE VIEW vw_EmployeesWithRestaurantDetails AS
SELECT 
    e.EmployeeId,
    e.FirstName AS EmployeeFirstName,
    e.LastName AS EmployeeLastName,
    e.Position,
    rest.RestaurantId,
    rest.Name AS RestaurantName,
    rest.Address AS RestaurantAddress,
    rest.PhoneNumber AS RestaurantPhoneNumber
FROM Employees e
JOIN Restaurants rest ON e.RestaurantId = rest.RestaurantId;