namespace API.DTO;

public class PropertyDTO
{
    public string Name { get; set; } = String.Empty;
    public string Country { get; set; } = String.Empty;
    public string City { get; set; } = String.Empty;
    public string Address { get; set; } = String.Empty;
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}
