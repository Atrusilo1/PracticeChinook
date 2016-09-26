﻿using System;
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
    //point to the SQL table that this file maps
    [Table("Artists")]
    public class Artist
    {
        //Key notation is optional if the SQL pkey
        //ends in ID or Id
        //required if default of entity is NOT identity
        //required if pkey is compound

        //properties can be fully implemented or
        //auto implemented
        //property names should use SQL attritube name
        //properties should be listed in the same order
        //as SQL tables attributes for ease of maintenance
        [Key]
        public int ArtistsId { get; set; }
        public string Name { get; set; }

        //navigation properties for use by LINQ
        //these properties type vitural
        //there are two types of navigation properties
        //properties that point to "childern" use ICollection<T>
        //prorperties that point to "Parent" use ParentName as the datatype
        public virtual ICollection<Album> Albums { get; set; }
    }
}
