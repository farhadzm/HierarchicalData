using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HierarchicalDataExample.ConsoleApplication.Models
{
    public class Users
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string FullName { get; set; }
        public ICollection<Comments> ChildComments { get; set; }
        public ICollection<Posts> Posts { get; set; }
    }
}
