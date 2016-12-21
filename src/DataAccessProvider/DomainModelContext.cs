using System;
using System.Linq;
using Domain.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccessProvider
{
    public class DomainModelContext : IdentityDbContext<User>
    {
        public DomainModelContext(DbContextOptions<DomainModelContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            #region Primary Keys

            builder.Entity<Answer>().HasKey(answer => answer.Id);
            builder.Entity<ChildParent>().HasKey(parent => new {parent.ParentId, parent.StudentId});
            builder.Entity<Comment>().HasKey(comment => comment.Id);
            builder.Entity<Consultation>().HasKey(consultation => consultation.Id);
            builder.Entity<Discipline>().HasKey(discipline => discipline.Id);
            builder.Entity<Document>().HasKey(document => document.Id);
            builder.Entity<Group>().HasKey(group => group.Id);
            builder.Entity<Journal>().HasKey(journal => journal.Id);
            builder.Entity<JournalDiscipline>()
                .HasKey(discipline => new {discipline.DisciplineId, discipline.JournalId});
            builder.Entity<Mark>().HasKey(mark => mark.Id);
            builder.Entity<Quarter>().HasKey(quarter => quarter.Id);
            builder.Entity<Question>().HasKey(question => question.Id);
            builder.Entity<TeacherDiscipline>()
                .HasKey(discipline => new {discipline.DisciplineId, discipline.TeacherId});
            builder.Entity<Test>().HasKey(test => test.Id);
            builder.Entity<TestResult>().HasKey(result => result.Id);
            builder.Entity<User>().HasKey(user => user.Id);
            builder.Entity<UserMark>()
                .HasKey(um => new {um.MarkId, um.UserId});
            builder.Entity<UserParent>().HasKey(user => user.Id);
            builder.Entity<UserSetting>().HasKey(setting => setting.Id);
            builder.Entity<UserStudent>().HasKey(user => user.Id);
            builder.Entity<UserTeacher>().HasKey(user => user.Id);

            #endregion

            builder.Entity<Answer>()
                .HasOne(answer => answer.Question)
                .WithMany(question => question.Answers)
                .HasForeignKey(answer => answer.QuestionId);
            builder.Entity<Answer>().HasIndex(answer => answer.QuestionId);

            builder.Entity<ChildParent>()
                .HasOne(chp => chp.Parent)
                .WithMany(parent => parent.Children)
                .HasForeignKey(chp => chp.ParentId);
            builder.Entity<ChildParent>().HasIndex(chp => chp.ParentId);

            builder.Entity<ChildParent>()
                .HasOne(chp => chp.Student)
                .WithMany(student => student.Parents)
                .HasForeignKey(chp => chp.StudentId);
            builder.Entity<ChildParent>().HasIndex(chp => chp.StudentId);

            builder.Entity<Comment>()
                .HasOne(comment => comment.User)
                .WithMany(user => user.Comments)
                .HasForeignKey(comment => comment.UserId);
            builder.Entity<Comment>().HasIndex(comment => comment.UserId);

            builder.Entity<Comment>()
                .HasOne(comment => comment.Test)
                .WithMany(test => test.Comments)
                .HasForeignKey(comment => comment.TestId);
            builder.Entity<Comment>().HasIndex(comment => comment.TestId);

            builder.Entity<Consultation>()
                .HasOne(consultation => consultation.Teacher)
                .WithMany(teacher => teacher.Consultations)
                .HasForeignKey(consultation => consultation.TeacherId);
            builder.Entity<Consultation>().HasIndex(consultation => consultation.TeacherId);

            builder.Entity<Discipline>().HasIndex(discipline => discipline.Id);

            builder.Entity<Document>()
                .HasOne(document => document.Teacher)
                .WithMany(teacher => teacher.Documents)
                .HasForeignKey(document => document.TeacherId);
            builder.Entity<Document>().HasIndex(document => document.TeacherId);
            builder.Entity<Document>().HasIndex(document => document.Name);

            builder.Entity<Group>().HasIndex(group => group.Id);

            builder.Entity<Journal>()
                .HasOne(journal => journal.Group)
                .WithMany(group => group.Journals)
                .HasForeignKey(journal => journal.GroupId);
            builder.Entity<Journal>().HasIndex(journal => journal.GroupId);

            builder.Entity<JournalDiscipline>()
                .HasOne(jd => jd.Journal)
                .WithMany(journal => journal.Disciplines)
                .HasForeignKey(jd => jd.JournalId);
            builder.Entity<JournalDiscipline>().HasIndex(jd => jd.JournalId);

            builder.Entity<JournalDiscipline>()
                .HasOne(jd => jd.Discipline)
                .WithMany(discipline => discipline.Journal)
                .HasForeignKey(jd => jd.DisciplineId);
            builder.Entity<JournalDiscipline>().HasIndex(jd => jd.DisciplineId);

            builder.Entity<Mark>()
                .HasOne(mark => mark.Journal)
                .WithMany(journal => journal.Marks)
                .HasForeignKey(mark => mark.JournalId);
            builder.Entity<Mark>().HasIndex(mark => mark.JournalId);

            builder.Entity<Mark>()
                .HasOne(mark => mark.Discipline)
                .WithMany(discipline => discipline.Marks)
                .HasForeignKey(mark => mark.DisciplineId);
            builder.Entity<Mark>().HasIndex(mark => mark.DisciplineId);

            builder.Entity<PendingUserData>()
                .HasOne(data => data.User)
                .WithMany(user => user.PendingUserData)
                .HasForeignKey(data => data.UserId);
            builder.Entity<PendingUserData>().HasIndex(data => data.UserId)
                .IsUnique();

            builder.Entity<Question>()
                .HasOne(question => question.Test)
                .WithMany(test => test.Questions)
                .HasForeignKey(question => question.TestId);
            builder.Entity<Question>().HasIndex(question => question.TestId);

            builder.Entity<TeacherDiscipline>()
                .HasOne(td => td.Teacher)
                .WithMany(teacher => teacher.Disciplines)
                .HasForeignKey(td => td.TeacherId);
            builder.Entity<TeacherDiscipline>().HasIndex(td => td.TeacherId);

            builder.Entity<TeacherDiscipline>()
                .HasOne(td => td.Discipline)
                .WithMany(discipline => discipline.Teachers)
                .HasForeignKey(td => td.DisciplineId);
            builder.Entity<TeacherDiscipline>().HasIndex(td => td.DisciplineId);

            builder.Entity<Test>()
                .HasOne(test => test.Teacher)
                .WithMany(teacher => teacher.Tests)
                .HasForeignKey(test => test.TeacherId);
            builder.Entity<Test>().HasIndex(test => test.TeacherId);

            builder.Entity<Test>()
                .HasOne(test => test.Discipline)
                .WithMany(discipline => discipline.Tests)
                .HasForeignKey(test => test.DisciplineId);
            builder.Entity<Test>().HasIndex(test => test.DisciplineId);

            builder.Entity<TestResult>()
                .HasOne(tr => tr.Test)
                .WithMany(test => test.TestResults)
                .HasForeignKey(tr => tr.TestId);
            builder.Entity<TestResult>().HasIndex(tr => tr.TestId);

            builder.Entity<TestResult>()
                .HasOne(tr => tr.User)
                .WithMany(user => user.TestResults)
                .HasForeignKey(tr => tr.UserId);
            builder.Entity<TestResult>().HasIndex(tr => tr.UserId);

            builder.Entity<User>().HasIndex(u => u.FirstName);
            builder.Entity<User>().HasIndex(u => u.LastName);

            builder.Entity<UserMark>()
                .HasOne(um => um.Mark)
                .WithMany(mark => mark.Users)
                .HasForeignKey(um => um.MarkId);
            builder.Entity<UserMark>().HasIndex(um => um.MarkId);

            builder.Entity<UserMark>()
                .HasOne(um => um.User)
                .WithMany(user => user.Marks)
                .HasForeignKey(um => um.UserId);
            builder.Entity<UserMark>().HasIndex(um => um.UserId);

            builder.Entity<UserParent>()
                .HasOne(parent => parent.User)
                .WithMany(user => user.Parent)
                .HasForeignKey(parent => parent.UserId);
            builder.Entity<UserParent>().HasIndex(parent => parent.UserId)
                .IsUnique();

            builder.Entity<UserSetting>()
                .HasOne(us => us.User)
                .WithMany(user => user.UserSettings)
                .HasForeignKey(us => us.UserId);
            builder.Entity<UserSetting>().HasIndex(us => us.UserId);

            builder.Entity<UserStudent>()
                .HasOne(student => student.User)
                .WithMany(user => user.Student)
                .HasForeignKey(student => student.UserId);
            builder.Entity<UserStudent>().HasIndex(student => student.UserId)
                .IsUnique();

            builder.Entity<UserTeacher>()
                .HasOne(teacher => teacher.User)
                .WithMany(user => user.Teacher)
                .HasForeignKey(teacher => teacher.UserId);
            builder.Entity<UserTeacher>().HasIndex(teacher => teacher.UserId)
                .IsUnique();

            #region UpdatedTimestamp Property

            builder.Entity<Answer>().Property<DateTime>("UpdatedTimestamp");
            builder.Entity<Discipline>().Property<DateTime>("UpdatedTimestamp");
            builder.Entity<Group>().Property<DateTime>("UpdatedTimestamp");
            builder.Entity<Journal>().Property<DateTime>("UpdatedTimestamp");
            builder.Entity<JournalDiscipline>().Property<DateTime>("UpdatedTimestamp");
            builder.Entity<Mark>().Property<DateTime>("UpdatedTimestamp");
            builder.Entity<Quarter>().Property<DateTime>("UpdatedTimestamp");
            builder.Entity<Question>().Property<DateTime>("UpdatedTimestamp");
            builder.Entity<TeacherDiscipline>().Property<DateTime>("UpdatedTimestamp");
            builder.Entity<Test>().Property<DateTime>("UpdatedTimestamp");
            builder.Entity<TestResult>().Property<DateTime>("UpdatedTimestamp");
            builder.Entity<User>().Property<DateTime>("UpdatedTimestamp");
            builder.Entity<UserSetting>().Property<DateTime>("UpdatedTimestamp");

            #endregion

            base.OnModelCreating(builder);
        }

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();

            UpdateUpdatedProperty<Answer>();
            UpdateUpdatedProperty<Discipline>();
            UpdateUpdatedProperty<Group>();
            UpdateUpdatedProperty<Journal>();
            UpdateUpdatedProperty<JournalDiscipline>();
            UpdateUpdatedProperty<Mark>();
            UpdateUpdatedProperty<Quarter>();
            UpdateUpdatedProperty<Question>();
            UpdateUpdatedProperty<TeacherDiscipline>();
            UpdateUpdatedProperty<Test>();
            UpdateUpdatedProperty<TestResult>();
            UpdateUpdatedProperty<User>();
            UpdateUpdatedProperty<UserSetting>();

            return base.SaveChanges();
        }

        private void UpdateUpdatedProperty<T>() where T : class
        {
            var modifiedSourceInfo = ChangeTracker.Entries<T>()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            foreach (var entry in modifiedSourceInfo)
                entry.Property("UpdatedTimestamp").CurrentValue = DateTime.UtcNow;
        }

        #region DbSets

        public DbSet<Answer> Answers { get; set; }
        public DbSet<ChildParent> ChildParents { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Consultation> Consultations { get; set; }
        public DbSet<Discipline> Disciplines { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Journal> Journals { get; set; }
        public DbSet<JournalDiscipline> JournalDisciplines { get; set; }
        public DbSet<Mark> Marks { get; set; }
        public DbSet<PendingUserData> PendingUserData { get; set; }
        public DbSet<Quarter> Quarters { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<TeacherDiscipline> TeacheDisciplines { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<TestResult> TestResults { get; set; }
        public DbSet<UserMark> UserMarks { get; set; }
        public DbSet<UserParent> UserParents { get; set; }
        public DbSet<UserSetting> UserSettings { get; set; }
        public DbSet<UserStudent> UserStudents { get; set; }
        public DbSet<UserTeacher> UserTeachers { get; set; }

        #endregion
    }
}