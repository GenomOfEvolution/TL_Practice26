namespace Domain.Entities;

/// <summary>
/// Средство размещения (отель, апартаменты и др.).
/// </summary>
public class Property
{
    public int Id { get; init; }
    public string Name { get; set; } = String.Empty;
    public string Country { get; set; } = String.Empty;
    public string City { get; set; } = String.Empty;
    public string Address { get; set; } = String.Empty;
    public double Latitude { get; set; }
    public double Longitude { get; set; }

    public void Update( Property property )
    {
        Name = property.Name;
        Country = property.Country;
        City = property.City;
        Address = property.Address;
        Latitude = property.Latitude;
        Longitude = property.Longitude;
    }
}
