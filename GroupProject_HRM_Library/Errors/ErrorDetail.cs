﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject_HRM_Library.Errors
{
    public class ErrorDetail
    {
        public string? FieldNameError { get; set; }
        public List<string>? DescriptionError { get; set; }
    }
}
