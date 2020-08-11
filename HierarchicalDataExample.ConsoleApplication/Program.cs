using Dapper;
using HierarchicalDataExample.ConsoleApplication.Data;
using HierarchicalDataExample.ConsoleApplication.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace HierarchicalDataExample.ConsoleApplication
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=.; Initial Catalog=HierarchicalData; Integrated Security=True;"))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("CommentId", 1, DbType.Int32);
                string query = @"
                            WITH Hierachy(Id, ParentId,UserId,Text) AS (
                                              SELECT Id, ParentId,UserId,Text
                                                  FROM Comments
                                              UNION ALL
                                              SELECT h.Id, comment.ParentId,h.UserId,h.Text
                                              FROM Comments comment
                                              INNER JOIN Hierachy h ON comment.Id = h.ParentId
                                      )
			                 Select * from Comments where Id in 
			                 ( SELECT Id
			                 	 FROM Hierachy h
			                 	 WHERE h.ParentId = @CommentId )";
                var affectedRows = (await connection.QueryAsync<Comments>(query, parameters)).AsEnumerable();
                foreach (var comment in affectedRows)
                {
                    Console.WriteLine($"{comment.Id}\t{comment.Text}");
                }
                Console.ReadLine();
            }
        }
    }
}
