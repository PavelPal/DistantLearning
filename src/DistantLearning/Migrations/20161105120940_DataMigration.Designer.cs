using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using DataAccessProvider;

namespace distantlearning.Migrations
{
    [DbContext(typeof(DomainModelContext))]
    [Migration("20161105120940_DataMigration")]
    partial class DataMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Domain.Model.Answer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Body");

                    b.Property<bool>("IsCorrect");

                    b.Property<int>("QuestionId");

                    b.Property<DateTime>("UpdatedTimestamp");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.ToTable("Answers");
                });

            modelBuilder.Entity("Domain.Model.ChildParent", b =>
                {
                    b.Property<int>("ParentId");

                    b.Property<int>("StudentId");

                    b.HasKey("ParentId", "StudentId");

                    b.HasIndex("ParentId");

                    b.HasIndex("StudentId");

                    b.ToTable("ChildParents");
                });

            modelBuilder.Entity("Domain.Model.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Body");

                    b.Property<DateTime>("Date");

                    b.Property<int>("TestId");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("TestId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("Domain.Model.Consultation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("DayOfWeek");

                    b.Property<int>("TeacherId");

                    b.Property<TimeSpan>("Time");

                    b.HasKey("Id");

                    b.HasIndex("TeacherId");

                    b.ToTable("Consultations");
                });

            modelBuilder.Entity("Domain.Model.Discipline", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<DateTime>("UpdatedTimestamp");

                    b.HasKey("Id");

                    b.ToTable("Disciplines");
                });

            modelBuilder.Entity("Domain.Model.Document", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<byte[]>("File");

                    b.Property<string>("FileType");

                    b.Property<bool>("IsLocked");

                    b.Property<string>("Name");

                    b.Property<int>("TeacherId");

                    b.HasKey("Id");

                    b.HasIndex("TeacherId");

                    b.ToTable("Documents");
                });

            modelBuilder.Entity("Domain.Model.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<DateTime>("UpdatedTimestamp");

                    b.HasKey("Id");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("Domain.Model.Journal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("ActivatedDate");

                    b.Property<DateTime>("ClosedDate");

                    b.Property<int>("GroupId");

                    b.Property<DateTime>("UpdatedTimestamp");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.ToTable("Journals");
                });

            modelBuilder.Entity("Domain.Model.JournalDiscipline", b =>
                {
                    b.Property<int>("DisciplineId");

                    b.Property<int>("JournalId");

                    b.Property<DateTime>("UpdatedTimestamp");

                    b.HasKey("DisciplineId", "JournalId");

                    b.HasIndex("DisciplineId");

                    b.HasIndex("JournalId");

                    b.ToTable("JournalDisciplines");
                });

            modelBuilder.Entity("Domain.Model.Mark", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<int>("DisciplineId");

                    b.Property<int>("JournalId");

                    b.Property<int>("Point");

                    b.Property<DateTime>("UpdatedTimestamp");

                    b.HasKey("Id");

                    b.HasIndex("DisciplineId");

                    b.HasIndex("JournalId");

                    b.ToTable("Marks");
                });

            modelBuilder.Entity("Domain.Model.Quarter", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("ActivatedDate");

                    b.Property<DateTime>("ClosedDate");

                    b.Property<int>("Number");

                    b.Property<DateTime>("UpdatedTimestamp");

                    b.HasKey("Id");

                    b.ToTable("Quarters");
                });

            modelBuilder.Entity("Domain.Model.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Body");

                    b.Property<int>("Seconds");

                    b.Property<int>("TestId");

                    b.Property<DateTime>("UpdatedTimestamp");

                    b.HasKey("Id");

                    b.HasIndex("TestId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("Domain.Model.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Domain.Model.TeacherDiscipline", b =>
                {
                    b.Property<int>("DisciplineId");

                    b.Property<int>("TeacherId");

                    b.Property<DateTime>("UpdatedTimestamp");

                    b.HasKey("DisciplineId", "TeacherId");

                    b.HasIndex("DisciplineId");

                    b.HasIndex("TeacherId");

                    b.ToTable("TeacheDisciplines");
                });

            modelBuilder.Entity("Domain.Model.Test", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("ClosedDate");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<int>("DisciplineId");

                    b.Property<bool>("IsLocked");

                    b.Property<string>("Name");

                    b.Property<DateTime?>("StartedDate");

                    b.Property<int>("TeacherId");

                    b.Property<DateTime>("UpdatedTimestamp");

                    b.HasKey("Id");

                    b.HasIndex("DisciplineId");

                    b.HasIndex("TeacherId");

                    b.ToTable("Tests");
                });

            modelBuilder.Entity("Domain.Model.TestResult", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Correct");

                    b.Property<int>("InComplete");

                    b.Property<int>("TestId");

                    b.Property<DateTime>("UpdatedTimestamp");

                    b.Property<int>("UserId");

                    b.Property<int>("Wrong");

                    b.HasKey("Id");

                    b.HasIndex("TestId");

                    b.HasIndex("UserId");

                    b.ToTable("TestResults");
                });

            modelBuilder.Entity("Domain.Model.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<bool>("IsEmailConfirmed");

                    b.Property<bool>("IsLocked");

                    b.Property<string>("LastName");

                    b.Property<int?>("ParentId");

                    b.Property<string>("Password");

                    b.Property<string>("PhoneNumber");

                    b.Property<byte[]>("Photo");

                    b.Property<string>("PhotoType");

                    b.Property<int?>("StudentId");

                    b.Property<int?>("TeacherId");

                    b.Property<DateTime>("UpdatedTimestamp");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.HasIndex("ParentId")
                        .IsUnique();

                    b.HasIndex("StudentId")
                        .IsUnique();

                    b.HasIndex("TeacherId")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Domain.Model.UserMark", b =>
                {
                    b.Property<int>("MarkId");

                    b.Property<int>("UserId");

                    b.HasKey("MarkId", "UserId");

                    b.HasIndex("MarkId");

                    b.HasIndex("UserId");

                    b.ToTable("UserMarks");
                });

            modelBuilder.Entity("Domain.Model.UserParent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.HasKey("Id");

                    b.ToTable("UserParents");
                });

            modelBuilder.Entity("Domain.Model.UserRole", b =>
                {
                    b.Property<int>("RoleId");

                    b.Property<int>("UserId");

                    b.HasKey("RoleId", "UserId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("Domain.Model.UserSetting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Key");

                    b.Property<DateTime>("UpdatedTimestamp");

                    b.Property<int>("UserId");

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserSettings");
                });

            modelBuilder.Entity("Domain.Model.UserStudent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("GroupId");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.ToTable("UserStudents");
                });

            modelBuilder.Entity("Domain.Model.UserTeacher", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.HasKey("Id");

                    b.ToTable("UserTeachers");
                });

            modelBuilder.Entity("Domain.Model.Answer", b =>
                {
                    b.HasOne("Domain.Model.Question", "Question")
                        .WithMany("Answers")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Domain.Model.ChildParent", b =>
                {
                    b.HasOne("Domain.Model.UserParent", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Domain.Model.UserStudent", "Student")
                        .WithMany("Parents")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Domain.Model.Comment", b =>
                {
                    b.HasOne("Domain.Model.Test", "Test")
                        .WithMany("Comments")
                        .HasForeignKey("TestId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Domain.Model.User", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Domain.Model.Consultation", b =>
                {
                    b.HasOne("Domain.Model.UserTeacher", "Teacher")
                        .WithMany("Consultations")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Domain.Model.Document", b =>
                {
                    b.HasOne("Domain.Model.UserTeacher", "Teacher")
                        .WithMany("Documents")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Domain.Model.Journal", b =>
                {
                    b.HasOne("Domain.Model.Group", "Group")
                        .WithMany("Journals")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Domain.Model.JournalDiscipline", b =>
                {
                    b.HasOne("Domain.Model.Discipline", "Discipline")
                        .WithMany("Journal")
                        .HasForeignKey("DisciplineId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Domain.Model.Journal", "Journal")
                        .WithMany("Disciplines")
                        .HasForeignKey("JournalId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Domain.Model.Mark", b =>
                {
                    b.HasOne("Domain.Model.Discipline", "Discipline")
                        .WithMany("Marks")
                        .HasForeignKey("DisciplineId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Domain.Model.Journal", "Journal")
                        .WithMany("Marks")
                        .HasForeignKey("JournalId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Domain.Model.Question", b =>
                {
                    b.HasOne("Domain.Model.Test", "Test")
                        .WithMany("Questions")
                        .HasForeignKey("TestId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Domain.Model.TeacherDiscipline", b =>
                {
                    b.HasOne("Domain.Model.Discipline", "Discipline")
                        .WithMany("Teachers")
                        .HasForeignKey("DisciplineId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Domain.Model.UserTeacher", "Teacher")
                        .WithMany("Disciplines")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Domain.Model.Test", b =>
                {
                    b.HasOne("Domain.Model.Discipline", "Discipline")
                        .WithMany("Tests")
                        .HasForeignKey("DisciplineId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Domain.Model.UserTeacher", "Teacher")
                        .WithMany("Tests")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Domain.Model.TestResult", b =>
                {
                    b.HasOne("Domain.Model.Test", "Test")
                        .WithMany("TestResults")
                        .HasForeignKey("TestId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Domain.Model.UserStudent", "User")
                        .WithMany("TestResults")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Domain.Model.User", b =>
                {
                    b.HasOne("Domain.Model.UserParent", "Parent")
                        .WithOne("User")
                        .HasForeignKey("Domain.Model.User", "ParentId");

                    b.HasOne("Domain.Model.UserStudent", "Student")
                        .WithOne("User")
                        .HasForeignKey("Domain.Model.User", "StudentId");

                    b.HasOne("Domain.Model.UserTeacher", "Teacher")
                        .WithOne("User")
                        .HasForeignKey("Domain.Model.User", "TeacherId");
                });

            modelBuilder.Entity("Domain.Model.UserMark", b =>
                {
                    b.HasOne("Domain.Model.Mark", "Mark")
                        .WithMany("Users")
                        .HasForeignKey("MarkId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Domain.Model.User", "User")
                        .WithMany("Marks")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Domain.Model.UserRole", b =>
                {
                    b.HasOne("Domain.Model.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Domain.Model.User", "User")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Domain.Model.UserSetting", b =>
                {
                    b.HasOne("Domain.Model.User", "User")
                        .WithMany("UserSettings")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Domain.Model.UserStudent", b =>
                {
                    b.HasOne("Domain.Model.Group", "Group")
                        .WithMany("Students")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
