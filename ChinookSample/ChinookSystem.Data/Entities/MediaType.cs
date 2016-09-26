using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespace
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#endregion

namespace ChinookSystem.Data.Entities
{
    [Table("MediaType")]
    public class MediaType
    {

        public virtual ICollection<Track> Tracks { get; set; }
        //public virtual Album Albums { get; set; }
    }
}
