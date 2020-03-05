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
        public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<HollywoodAPI.Model.Tournament.Tournament> Tournaments { get; set; }

        public DbSet<HollywoodAPI.Model.Event.Event> Events { get; set; }

        public DbSet<HollywoodAPI.Model.EventDetailStatus.EventDetailStatus> EventDetailStatus { get; set; }

        public DbSet<HollywoodAPI.Model.EventDetail.EventDetail> EventDetail { get; set; }

    }
}
    