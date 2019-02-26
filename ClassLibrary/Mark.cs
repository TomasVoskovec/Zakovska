using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary
{
    public class Mark
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public float Value { get; set; }
        public int Weight { get; set; }

        public int SubjectId { get; set; }
    }
}
