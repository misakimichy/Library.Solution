using System.Collections.Generic;

namespace Library.Models
{
  public class Copy
  {
    public Copy()
    {
      this.Patrons = new HashSet<PatronCopy>();
    }
    public int CopyId { get; set; }
    public int BookId { get; set; }
    
    public Book Book { get; set; }
    public Patron Patron { get; set; }
    public virtual ICollection<PatronCopy> Patrons { get; set; }
  }
}