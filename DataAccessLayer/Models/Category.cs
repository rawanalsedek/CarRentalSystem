using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Models
{
    public class Category
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public List<Car>? Cars { get; set; }
    }
}
