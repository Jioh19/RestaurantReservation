using RestaurantReservation.Domain.EntityReferences;

namespace RestaurantReservation.Domain.Employees.Models;

public class Employee
{
    public long Id { get; set; }

    public string FirstName { get; set; } = string.Empty;
    
    public string LastName { get; set; } = string.Empty;
    
    public string Position { get; set; } = string.Empty;

    public EntityReference<long> Restaurant { get; set; } = EntityReference<long>.Empty;
}