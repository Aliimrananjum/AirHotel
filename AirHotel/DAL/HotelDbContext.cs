using System;
using AirHotel.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AirHotel.Models;


public class HotelDbContext : IdentityDbContext
{
    public HotelDbContext(DbContextOptions<HotelDbContext> options) : base(options)
    {
        //Database.EnsureCreated();
    }

    public DbSet<Hotel> Hotels { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }

    public DbSet<OrderItem> OrderItems { get; set; }    

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLazyLoadingProxies();
    }

}

