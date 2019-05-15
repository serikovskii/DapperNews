using Dapper;
using NewsApp.DataAccess.Abstract;
using NewsApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;

namespace NewsApp.DataAccess
{
    public class ArticlesRepository : IRepository<Article>
    {
        private DbConnection _connection;

        public ArticlesRepository(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
        }

        public void Add(Article item)
        {
            var sqlQuery = "insert into Articles values (@Id, @Name, @Text, @CreationDate, @DeletedDate)";
            var result = _connection.Execute(sqlQuery, item);

            if (result < 1) throw new Exception();
        }

        public void Dispose()
        {
            _connection.Close();
        }

        public ICollection<Article> GetAll()
        {
            var sql = "select * from Articles";

            return _connection.Query<Article>(sql).AsList();
        }
    }
}
