﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingDiary.ViewModels
{
    public class CreateRequestViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
