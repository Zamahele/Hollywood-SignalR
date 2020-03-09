using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HollywoodAPI.Model.Tournament;
using HollywoodAPI.Model.Event;
using HollywoodAPI.Model.EventDetailStatus;
using HollywoodAPI.Model.EventDetail;


namespace HollywoodAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }

        public DbSet<HollywoodAPI.Model.Tournament.Tournament> Tournaments { get; set; }

        public DbSet<HollywoodAPI.Model.Event.Event> Events { get; set; }

        public DbSet<HollywoodAPI.Model.EventDetailStatus.EventDetailStatus> EventDetailStatus { get; set; }

        public DbSet<HollywoodAPI.Model.EventDetail.EventDetail> EventDetails { get; set; }

    }
}
    