using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoUniversity.Models
{
    public class Parking
    {

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ParkingID { get; set; }

        public int? OccupantID { get; set; }
    }
}