﻿using Mealmate.Application.Models.Base;

using System;
using System.Text.Json.Serialization;

namespace Mealmate.Application.Models
{
    public class QRCodeModel : BaseModel
    {
        public DateTimeOffset Created { get; set; }

        public int TableId { get; set; }
    }
}