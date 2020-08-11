using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HierarchicalDataExample.ConsoleApplication.Models
{
    public class Comments
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string Text { get; set; }
        public int? ParentId { get; set; }
        public Users User { get; set; }
        public Posts Post { get; set; }
        public Comments Parent { get; set; }
        public ICollection<Comments> ChildComments { get; set; }
    }
}
