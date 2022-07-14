﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AdventureWorksNS.Data
{
    /// <summary>
    /// Audit table tracking errors in the the AdventureWorks database that are caught by the CATCH block of a TRY...CATCH construct. Data is inserted by stored procedure dbo.uspLogError when it is executed from inside the CATCH block of a TRY...CATCH construct.
    /// </summary>
    [Table("ErrorLog")]
    public partial class ErrorLog
    {
        /// <summary>
        /// Primary key for ErrorLog records.
        /// </summary>
        [Key]
        [Column("ErrorLogID")]
        public int ErrorLogId { get; set; }
        /// <summary>
        /// The date and time at which the error occurred.
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime ErrorTime { get; set; }
        /// <summary>
        /// The user who executed the batch in which the error occurred.
        /// </summary>
        [StringLength(128)]
        public string UserName { get; set; } = null!;
        /// <summary>
        /// The error number of the error that occurred.
        /// </summary>
        public int ErrorNumber { get; set; }
        /// <summary>
        /// The severity of the error that occurred.
        /// </summary>
        public int? ErrorSeverity { get; set; }
        /// <summary>
        /// The state number of the error that occurred.
        /// </summary>
        public int? ErrorState { get; set; }
        /// <summary>
        /// The name of the stored procedure or trigger where the error occurred.
        /// </summary>
        [StringLength(126)]
        public string? ErrorProcedure { get; set; }
        /// <summary>
        /// The line number at which the error occurred.
        /// </summary>
        public int? ErrorLine { get; set; }
        /// <summary>
        /// The message text of the error that occurred.
        /// </summary>
        [StringLength(4000)]
        public string ErrorMessage { get; set; } = null!;
    }
}
