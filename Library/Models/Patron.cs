using System.Collections.Generic;

namespace Library.Models
{
  public class Patron
  {
    public Patron()
    {
      this.Copies = new HashSet<PatronCopy>();
    }
    public int PatronId { get; set; }
    public int Name { get; set; }
    public virtual ICollection<PatronCopy> Copies { get; set; }
  }
}