﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ITIAttendanceSystem.Models
{
    public partial class Document
    {
        [Key]
        public int StudentId { get; set; }
        public bool IdImg { get; set; }
        public bool BirthDate { get; set; }
        public bool? Military { get; set; }
        public bool Graduation { get; set; }
        public bool Contract { get; set; }
        public bool PolicePaper { get; set; }
        public string Notes { get; set; }

        [ForeignKey("StudentId")]
        [InverseProperty("Document")]
        public virtual Student Student { get; set; }
    }
}