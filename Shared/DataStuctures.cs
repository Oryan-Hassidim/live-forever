namespace LiveForever;

public class Phone
{
    public required string PhoneNumber { get; set; }
    public bool HasWhatsApp { get; set; } = false;
}

public class Person
{
    public Guid Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Address { get; set; }
    public List<Phone>? PhoneNumbers { get; set; }
    public string? Email { get; set; }
}

public class Elderly : Person
{
    public Person? ContactPerson { get; set; }
    public string? MedicalHistory { get; set; }
    public string? Allergies { get; set; }
    public string? Medication { get; set; }
    public string? Notes { get; set; }


    public ICollection<Volunteer>? Volunteers { get; set; }
    public ICollection<Visit>? Visits { get; set; }
}

public enum Role
{
    Admin,
    Volunteer,
    NotInvited
}

public class Volunteer : Person
{
    public string? Availability { get; set; }
    public string? Skills { get; set; }
    public string? Notes { get; set; }
    public required List<Role> Roles { get; set; }
    public required string IdentityProvider { get; set; }
    public required string UserDetails { get; set; }


    public ICollection<Elderly>? Elderlies { get; set; }
    public ICollection<Visit>? Visits { get; set; }
}

public class Visit
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public DateTimeOffset Duration { get; set; }
    public Guid ElderlyId { get; set; }
    public Guid VolunteerId { get; set; }
    public string? Notes { get; set; }


    public Elderly? Elderly { get; set; }
    public Volunteer? Volunteer { get; set; }
}
public class ChatMessage
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public Guid Sender { get; set; }
    public Guid Receiver { get; set; }
    public required string Message { get; set; }
}