﻿// <auto-generated />
using System;
using ITIAttendanceSystem.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ITIAttendanceSystem.Migrations
{
    [DbContext(typeof(ITICOMPSYSDB2Context))]
    [Migration("20220505221558_student_section")]
    partial class student_section
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ITIAttendanceSystem.Models.Attendance", b =>
                {
                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.Property<DateTime>("AttendanceDate")
                        .HasColumnType("datetime2");

                    b.Property<TimeSpan?>("ArrivalTime")
                        .HasColumnType("time");

                    b.Property<TimeSpan?>("LeaveTime")
                        .HasColumnType("time");

                    b.HasKey("StudentId", "AttendanceDate");

                    b.HasIndex(new[] { "AttendanceDate", "StudentId" }, "AK_Attendances_AttendanceDate_StudentId")
                        .IsUnique();

                    b.ToTable("Attendances");
                });

            modelBuilder.Entity("ITIAttendanceSystem.Models.buildingAffairsAttendance", b =>
                {
                    b.Property<int>("StaffId")
                        .HasColumnType("int");

                    b.Property<DateTime>("AttendanceDate")
                        .HasColumnType("datetime2");

                    b.Property<TimeSpan>("ArrivalTime")
                        .HasColumnType("time");

                    b.Property<TimeSpan?>("LeaveTime")
                        .HasColumnType("time");

                    b.HasKey("StaffId", "AttendanceDate", "ArrivalTime");

                    b.HasIndex(new[] { "ArrivalTime", "AttendanceDate", "StaffId" }, "AK_buildingAffairsAttendances_ArrivalTime_AttendanceDate_StaffId")
                        .IsUnique();

                    b.ToTable("buildingAffairsAttendances");
                });

            modelBuilder.Entity("ITIAttendanceSystem.Models.BuildingAffairsStaff", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("BuildingAffairsType")
                        .HasColumnType("int");

                    b.Property<string>("FullNameAr")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullNameEn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<string>("HomePhone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MilitaryStatusName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Mobile")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("nvarchar(14)");

                    b.HasKey("Id");

                    b.ToTable("BuildingAffairsStaffs");
                });

            modelBuilder.Entity("ITIAttendanceSystem.Models.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Body")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CommentDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("CommentType")
                        .HasColumnType("int");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "StudentId" }, "IX_Comments_StudentId");

                    b.HasIndex(new[] { "UserId" }, "IX_Comments_UserId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("ITIAttendanceSystem.Models.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime?>("AttendanceEndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("IntakeId")
                        .HasColumnType("int");

                    b.Property<string>("ShortName")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<int?>("SupervisorId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "IntakeId" }, "IX_Departments_IntakeId");

                    b.HasIndex(new[] { "SupervisorId" }, "IX_Departments_SupervisorId");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("ITIAttendanceSystem.Models.Document", b =>
                {
                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.Property<bool>("BirthDate")
                        .HasColumnType("bit");

                    b.Property<bool>("Contract")
                        .HasColumnType("bit");

                    b.Property<bool>("Graduation")
                        .HasColumnType("bit");

                    b.Property<bool>("IdImg")
                        .HasColumnType("bit");

                    b.Property<bool?>("Military")
                        .HasColumnType("bit");

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PolicePaper")
                        .HasColumnType("bit");

                    b.HasKey("StudentId");

                    b.ToTable("Documents");
                });

            modelBuilder.Entity("ITIAttendanceSystem.Models.Instructor", b =>
                {
                    b.Property<int>("InstructorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("InstructorId"), 1L, 1);

                    b.Property<int?>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("InstName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("InstructorId");

                    b.HasIndex("DepartmentId");

                    b.HasIndex(new[] { "Id" }, "IX_Instructors_Id")
                        .IsUnique()
                        .HasFilter("([Id] IS NOT NULL)");

                    b.ToTable("Instructors");
                });

            modelBuilder.Entity("ITIAttendanceSystem.Models.Schedule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<int>("LectPeriod")
                        .HasColumnType("int");

                    b.Property<DateTime>("ScheduleDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "DepartmentId" }, "IX_Schedules_DepartmentId");

                    b.ToTable("Schedules");
                });

            modelBuilder.Entity("ITIAttendanceSystem.Models.Student", b =>
                {
                    b.Property<int>("StudentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StudentId"), 1L, 1);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<string>("Faculty")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GraduationGrade")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GraduationYear")
                        .HasColumnType("int");

                    b.Property<string>("HomePhone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("MilitaryStatusName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Mobile")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SecNo")
                        .HasColumnType("int");

                    b.Property<string>("Specialization")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StudentName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StudentStatus")
                        .HasColumnType("int");

                    b.Property<string>("University")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StudentId");

                    b.HasIndex(new[] { "DepartmentId" }, "IX_Students_DepartmentId");

                    b.HasIndex(new[] { "Id" }, "IX_Students_Id")
                        .IsUnique()
                        .HasFilter("([Id] IS NOT NULL)");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("ITIAttendanceSystem.Models.studentPermission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("InstructorId")
                        .HasColumnType("int");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("PermissionDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("PermissionState")
                        .HasColumnType("int");

                    b.Property<int>("PermissionType")
                        .HasColumnType("int");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "InstructorId" }, "IX_studentPermissions_InstructorId");

                    b.HasIndex(new[] { "StudentId" }, "IX_studentPermissions_StudentId");

                    b.ToTable("studentPermissions");
                });

            modelBuilder.Entity("ITIAttendanceSystem.Models.Subject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<int>("LabHours")
                        .HasColumnType("int");

                    b.Property<int>("LectHours")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ShortName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "DepartmentId" }, "IX_Subjects_DepartmentId");

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("ITIAttendanceSystem.Models.Attendance", b =>
                {
                    b.HasOne("ITIAttendanceSystem.Models.Student", "Student")
                        .WithMany("Attendances")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Student");
                });

            modelBuilder.Entity("ITIAttendanceSystem.Models.buildingAffairsAttendance", b =>
                {
                    b.HasOne("ITIAttendanceSystem.Models.BuildingAffairsStaff", "Staff")
                        .WithMany("buildingAffairsAttendances")
                        .HasForeignKey("StaffId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Staff");
                });

            modelBuilder.Entity("ITIAttendanceSystem.Models.Comment", b =>
                {
                    b.HasOne("ITIAttendanceSystem.Models.Student", "Student")
                        .WithMany("Comments")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Student");
                });

            modelBuilder.Entity("ITIAttendanceSystem.Models.Department", b =>
                {
                    b.HasOne("ITIAttendanceSystem.Models.Instructor", "Supervisor")
                        .WithMany("Departments")
                        .HasForeignKey("SupervisorId");

                    b.Navigation("Supervisor");
                });

            modelBuilder.Entity("ITIAttendanceSystem.Models.Document", b =>
                {
                    b.HasOne("ITIAttendanceSystem.Models.Student", "Student")
                        .WithOne("Document")
                        .HasForeignKey("ITIAttendanceSystem.Models.Document", "StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Student");
                });

            modelBuilder.Entity("ITIAttendanceSystem.Models.Instructor", b =>
                {
                    b.HasOne("ITIAttendanceSystem.Models.Department", "Department")
                        .WithMany("Instructors")
                        .HasForeignKey("DepartmentId");

                    b.Navigation("Department");
                });

            modelBuilder.Entity("ITIAttendanceSystem.Models.Schedule", b =>
                {
                    b.HasOne("ITIAttendanceSystem.Models.Department", "Department")
                        .WithMany("Schedules")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");
                });

            modelBuilder.Entity("ITIAttendanceSystem.Models.Student", b =>
                {
                    b.HasOne("ITIAttendanceSystem.Models.Department", "Department")
                        .WithMany("Students")
                        .HasForeignKey("DepartmentId");

                    b.Navigation("Department");
                });

            modelBuilder.Entity("ITIAttendanceSystem.Models.studentPermission", b =>
                {
                    b.HasOne("ITIAttendanceSystem.Models.Instructor", "Instructor")
                        .WithMany("studentPermissions")
                        .HasForeignKey("InstructorId");

                    b.HasOne("ITIAttendanceSystem.Models.Student", "Student")
                        .WithMany("studentPermissions")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Instructor");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("ITIAttendanceSystem.Models.Subject", b =>
                {
                    b.HasOne("ITIAttendanceSystem.Models.Department", "Department")
                        .WithMany("Subjects")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");
                });

            modelBuilder.Entity("ITIAttendanceSystem.Models.BuildingAffairsStaff", b =>
                {
                    b.Navigation("buildingAffairsAttendances");
                });

            modelBuilder.Entity("ITIAttendanceSystem.Models.Department", b =>
                {
                    b.Navigation("Instructors");

                    b.Navigation("Schedules");

                    b.Navigation("Students");

                    b.Navigation("Subjects");
                });

            modelBuilder.Entity("ITIAttendanceSystem.Models.Instructor", b =>
                {
                    b.Navigation("Departments");

                    b.Navigation("studentPermissions");
                });

            modelBuilder.Entity("ITIAttendanceSystem.Models.Student", b =>
                {
                    b.Navigation("Attendances");

                    b.Navigation("Comments");

                    b.Navigation("Document");

                    b.Navigation("studentPermissions");
                });
#pragma warning restore 612, 618
        }
    }
}
