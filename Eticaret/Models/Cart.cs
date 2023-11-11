using System.ComponentModel.DataAnnotations;

namespace Eticaret.Models
{
    public class Cart
    {
       
            [Key]
            public int RecordId { get; set; }
            public string CartId { get; set; } //eğer loginsek login olan kişinin mailini, değil ise Guid bilgisini
            public int AlbumId { get; set; }
            public int Count { get; set; }
            public System.DateTime DateCreated { get; set; }
            public virtual Album Album { get; set; }

        }
    }

