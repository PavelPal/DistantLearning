﻿using System;

namespace Domain.Model
{
    public class Consultation
    {
        public int Id { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public TimeSpan Time { get; set; }

        public UserTeacher Teacher { get; set; }
        public int TeacherId { get; set; }
    }
}