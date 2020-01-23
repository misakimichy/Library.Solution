using System;
using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class PatronCopy
    {
        public int PatronCopyId { get; set; }
        public int CopyId { get; set; }
        public int PatronId { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode=true)]
        [DataType(DataType.Date)]
        public DateTime CheckOutDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode=true)]
        [DataType(DataType.Date)]
        public DateTime DueDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode=true)]
        [DataType(DataType.Date)]
        public DateTime ReturnDate { get; set; }

        public Copy Copy { get; set; }
        public Patron Patron { get; set; }
    }
}