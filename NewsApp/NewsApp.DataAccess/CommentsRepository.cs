using Dapper;
using NewsApp.DataAccess.Abstract;
using NewsApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;

namespace NewsApp.DataAccess
{
    public class CommentsRepository : IRepository<Comment>
    {
        private DbConnection _connection;

        public CommentsRepository(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
        }

        public void Add(Comment item)
        {
            var sql = "insert into Comments values (@Id, @Text, @ArticleId, @CreationDate, @DeletedDate)";
            var result = _connection.Execute(sql, item);

            if (result < 1) throw new Exception();
        }

        public void Dispose()
        {
            _connection.Close();
        }

        public ICollection<Comment> GetAll()
        {
            var sql = "select * from Comments";

            return _connection.Query<Comment>(sql).AsList();
        }
    }
}
