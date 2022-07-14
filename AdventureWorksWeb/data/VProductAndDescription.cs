﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AdventureWorksNS.Data
{
    /// <summary>
    /// Product names and descriptions. Product descriptions are provided in multiple languages.
    /// </summary>
    [Keyless]
    public partial class VProductAndDescription
    {
        [Column("ProductID")]
        public int ProductId { get; set; }
        [StringLength(50)]
        public string Name { get; set; } = null!;
        [StringLength(50)]
        public string ProductModel { get; set; } = null!;
        [StringLength(6)]
        public string Culture { get; set; } = null!;
        [StringLength(400)]
        public string Description { get; set; } = null!;
    }
}
