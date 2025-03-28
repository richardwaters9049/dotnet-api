public class Facility
{
    public string Name { get; set; }
    public string Location { get; set; }

    // Constructor that ensures properties are initialized
    public Facility(string name, string location)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Location = location ?? throw new ArgumentNullException(nameof(location));
    }
}
