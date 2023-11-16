using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ApiIsolated;

public class LiveForeverContext : DbContext
{
    private readonly MongoClient _client;

    public LiveForeverContext(MongoClient client)
    {
        _client = client;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMongoDB(_client, "live-forever");

    public DbSet<Elderly> Elderlies => Set<Elderly>();
    public DbSet<Volunteer> Volunteers => Set<Volunteer>();
    public DbSet<Visit> Visits => Set<Visit>();
    public DbSet<ChatMessage> ChatMessages => Set<ChatMessage>();
}

public static class LiveForeverContextQueries
{
    public static IQueryable<Elderly> GetElderlies(this LiveForeverContext context, int page, int pageSize)
        => context.Elderlies.Include(e => e.PhoneNumbers)
            .Skip(page * pageSize)
            .Take(pageSize);
    public static Elderly? GetElderly(this LiveForeverContext context, Guid id)
        => context.Elderlies
            .Include(e => e.PhoneNumbers)
            .Include(e => e.ContactPerson)
            .Include(e => e.Volunteers)
            .Include(e => e.Visits)
            .FirstOrDefault(e => e.Id == id);
    public static IQueryable<Volunteer> GetVolunteers(this LiveForeverContext context, int page, int pageSize)
        => context.Volunteers.Include(v => v.PhoneNumbers)
            .Skip(page * pageSize)
            .Take(pageSize);
    public static Volunteer? GetVolunteer(this LiveForeverContext context, Guid id)
        => context.Volunteers
            .Include(v => v.PhoneNumbers)
            .Include(v => v.Elderlies)
            .Include(v => v.Visits)
            .FirstOrDefault(v => v.Id == id);

    public static Visit? GetVisit(this LiveForeverContext context, Guid id)
        => context.Visits
            .Include(v => v.Elderly)
            .Include(v => v.Volunteer)
            .FirstOrDefault(v => v.Id == id);


}

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


    public ICollection? Volunteers { get; set; }
    public ICollection? Visits { get; set; }
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


    public ICollection? Elderlies { get; set; }
    public ICollection? Visits { get; set; }
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
