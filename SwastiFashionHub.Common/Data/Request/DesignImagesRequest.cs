﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwastiFashionHub.Common.Data.Request
{
    public class DesignImagesRequest
    {
        public Guid Id { get; set; }
        public Guid DesignId { get; set; }
        public string DesignImage { get; set; }

    }
}
