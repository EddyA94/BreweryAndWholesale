using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreweryWholesale.Domain.Models.DBO
{
    [Table(nameof(Brewery))]
    [PrimaryKey(nameof(BrewerID))]
    public class Brewery
    {
        public int BrewerID { get; set; }
        public string Name { get; set; }
        public ICollection<Beer> Beers { get; set; }
    }
}
