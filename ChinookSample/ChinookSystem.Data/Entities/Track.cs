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
    [Table("Track")]
    public class Track
    {
        //entity validation
        //this is validation that kicks in when the 
        //.SaveChange() command is executed

        //[Required(ErrorMassage="...")]
        //[StringLength(int maximum,[int minimum],ErrorMessage="...")]
        //[Range(double minimum, double maximum,ErrorMesage="...")]
        //[RegularExpression("Expression",ErrorMessage="...")]

        [Key]
        public int TrackId { get; set; }
        [Required(ErrorMessage ="Name is required field")]
        [StringLength(200, ErrorMessage ="Name is to long max charaters is 200")]
        public string Name { get; set; }
        [Range(1.0,double.MaxValue,ErrorMessage ="Invalid Album. Try selection again")]
        public int? AlbumId { get; set; }
        [Required(ErrorMessage = "Media Type is required field")]
        [Range(1.0, double.MaxValue, ErrorMessage = "Invalid media type. Try selection again")]
        public int MediatypeId { get; set; }
        [Range(1.0, double.MaxValue, ErrorMessage = "Invalid genre. Try selection again")]
        public int? GenreId { get; set; }
        [Range(220, double.MaxValue, ErrorMessage = "Invalid Composer. Try selection again")]
        public string Composer { get; set; }
        [Required(ErrorMessage = "MSec is required")]
        [Range(1.0, double.MaxValue, ErrorMessage = "Invalid MSec. Must be greater then 0")]
        public int Milliseconds { get; set; }
        [Range(1.0, double.MaxValue, ErrorMessage = "Invalid Byte. Must be greater then 0")]
        public int? Bytes { get; set; }
        [Range(1.0, double.MaxValue, ErrorMessage = "Invalid price. Must be greater then 0")]
        public decimal? UnitPrice { get; set; }

        public virtual MediaType MediaTypes { get; set; }
        public virtual Album Albums { get; set; }
    }

}
