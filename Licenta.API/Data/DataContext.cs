using Licenta.API.Models;
using Licenta.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Licenta.Data
{
    public class DataContext : IdentityDbContext<User, Role, int,
        IdentityUserClaim<int>,UserRole, IdentityUserLogin<int>,
        IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Weather> Weather { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<UserSpecialization> UserSpecializations { get; set; }
        public DbSet<Specialization> Specializations { get; set; }
        public DbSet<Seminar> Seminars { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Laboratory> Laboratories { get; set; }
        public DbSet<UserSubGroup> UserSubGroups { get; set; }
        public DbSet<SubGroup> SubGroups { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Semester> Semesters { get; set; }
        public DbSet<CompanyPresentation> CompaniesPresentations { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserSpecialization>(userSpecialization =>
            {
                userSpecialization.HasKey(ud => new { ud.UserId, ud.SpecializationId });

                userSpecialization.HasOne(ud => ud.Specializations)
                    .WithMany(d => d.UserSpecializations)
                    .HasForeignKey(ud => ud.SpecializationId)
                    .IsRequired();

                userSpecialization.HasOne(ud => ud.User)
                    .WithMany(u => u.UserSpecialization)
                    .HasForeignKey(ud => ud.UserId)
                    .IsRequired();
            });

            builder.Entity<UserGroup>(userGroup =>
            {
                userGroup.HasKey(ud => new { ud.UserId, ud.GroupId });

                userGroup.HasOne(ud => ud.Group)
                    .WithMany(d => d.UserGroups)
                    .HasForeignKey(ud => ud.GroupId)
                    .IsRequired();

                userGroup.HasOne(ud => ud.User)
                    .WithMany(u => u.UserGroups)
                    .HasForeignKey(ud => ud.UserId)
                    .IsRequired();
            });

            builder.Entity<UserSubGroup>(userSubGroup =>
            {
                userSubGroup.HasKey(ud => new { ud.UserId, ud.SubGroupId });

                userSubGroup.HasOne(ud => ud.SubGroup)
                    .WithMany(d => d.UserSubGroups)
                    .HasForeignKey(ud => ud.SubGroupId)
                    .IsRequired();

                userSubGroup.HasOne(ud => ud.User)
                    .WithMany(u => u.UserSubGroups)
                    .HasForeignKey(ud => ud.UserId)
                    .IsRequired();
            });

            builder.Entity<UserRole>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                userRole.HasOne(ur => ur.User)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });

            builder.Entity<Like>()
                .HasKey(k => new { k.LikerId, k.LikeeId });

            builder.Entity<Like>()
                .HasOne(u => u.Likee)
                .WithMany(u => u.Likers)
                .HasForeignKey(u => u.LikeeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Like>()
                .HasOne(u => u.Liker)
                .WithMany(u => u.Likees)
                .HasForeignKey(u => u.LikerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Message>()
                .HasOne(u => u.Sender)
                .WithMany(m => m.MessagesSent)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Message>()
                .HasOne(u => u.Recipient)
                .WithMany(m => m.MessagesReceived)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
