﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ITIAttendanceSystem.Models
{
    [Index(nameof(IntakeId), Name = "IX_Departments_IntakeId")]
    [Index(nameof(SupervisorId), Name = "IX_Departments_SupervisorId")]
    public partial class Department
    {
        public Department()
        {
            Instructors = new HashSet<Instructor>();
            Schedules = new HashSet<Schedule>();
            Students = new HashSet<Student>();
            Subjects = new HashSet<Subject>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(15)]
        public string ShortName { get; set; }
        [StringLength(100)]
        public string FullName { get; set; }
        public DateTime? AttendanceEndDate { get; set; }
        public int? SupervisorId { get; set; }
        public int? IntakeId { get; set; }
        public bool Status { get; set; }
        public string Code { get; set; }

        [ForeignKey(nameof(SupervisorId))]
        [InverseProperty(nameof(Instructor.Departments))]
        public virtual Instructor Supervisor { get; set; }
        [InverseProperty(nameof(Instructor.Department))]
        public virtual ICollection<Instructor> Instructors { get; set; }
        [InverseProperty(nameof(Schedule.Department))]
        public virtual ICollection<Schedule> Schedules { get; set; }
        [InverseProperty(nameof(Student.Department))]
        public virtual ICollection<Student> Students { get; set; }
        [InverseProperty(nameof(Subject.Department))]
        public virtual ICollection<Subject> Subjects { get; set; }
    }
}