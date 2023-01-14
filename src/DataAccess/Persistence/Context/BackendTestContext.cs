using DataAccess.Models;
using DataAccess.Persistence.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace DataAccess.Persistence.Context
{
    public partial class BackendTestContext : DbContext, IAppContext
    {
        private readonly IConfiguration configuration;
        public BackendTestContext()
        {
        }

        public BackendTestContext(DbContextOptions<BackendTestContext> options,
            IConfiguration config)
            : base(options)
        {
            configuration = config;
        }

        public virtual DbSet<Flight> Flights { get; set; }
        public virtual DbSet<Journey> Journeys { get; set; }
        public virtual DbSet<Journeyflight> Journeyflights { get; set; }
        public virtual DbSet<Transport> Transports { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL(configuration.GetConnectionString("Backend"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Flight>(entity =>
            {
                entity.ToTable("flights");

                entity.HasIndex(e => e.TransportId, "IX_Flights_TransportId");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.TransportId).HasColumnType("int(11)");

                entity.HasOne(d => d.Transport)
                    .WithMany()
                    .HasForeignKey(d => d.TransportId)
                    .HasConstraintName("FK_Flights_Transports_TransportId");
            });

            modelBuilder.Entity<Journey>(entity =>
            {
                entity.ToTable("journeys");

                entity.Property(e => e.Id).HasColumnType("int(11)");
            });

            modelBuilder.Entity<Journeyflight>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("journeyflights");

                entity.HasIndex(e => e.IdFlight, "journeyflights_flights_fk");

                entity.HasIndex(e => e.IdJourney, "journeyflights_journeys_fk");

                entity.Property(e => e.IdFlight)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_flight");

                entity.Property(e => e.IdJourney)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_journey");

                entity.HasOne(d => d.IdFlightNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdFlight)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("journeyflights_flights_fk");

                entity.HasOne(d => d.IdJourneyNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdJourney)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("journeyflights_journeys_fk");
            });

            modelBuilder.Entity<Transport>(entity =>
            {
                entity.ToTable("transports");

                entity.Property(e => e.Id).HasColumnType("int(11)");

            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
