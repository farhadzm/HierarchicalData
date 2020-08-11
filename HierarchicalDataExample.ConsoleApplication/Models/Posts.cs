using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HierarchicalDataExample.ConsoleApplication.Models
{
    public class Posts
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Descriptions { get; set; }
        public int UserId { get; set; }
        public Users User { get; set; }
        public ICollection<Comments> ChildComments { get; set; }
    }
}
