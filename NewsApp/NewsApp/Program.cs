using DbUp;
using NewsApp.DataAccess;
using NewsApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;

namespace NewsApp
{
    class Program
    {
        static readonly string connectionString = ConfigurationManager
                            .ConnectionStrings["appConnection"].
                            ConnectionString;

        static void Main(string[] args)
        {
            DoMigration();

            while (true)
            {
                Console.WriteLine("1. Добавить статью\n2.Добавить комментарий\n3.Показать новости\n4.Выход");
                Console.Write("Выберите действие: ");

                if (!int.TryParse(Console.ReadLine(), out int action))
                {
                    Console.WriteLine("Ошибка! Введите цифры!");
                }
                else if (action < 0 || action > 5)
                {
                    Console.WriteLine("Не существует такого действия!");
                }
                
                if (action == 1)
                {
                    var article = InitArticle();

                    using (var articleRepository = new ArticlesRepository(connectionString))
                    {
                        articleRepository.Add(article);

                        Console.WriteLine("Статья успешно добавлена!");
                    }
                }
                else if (action == 2)
                {
                    var comment = InitComment();

                    using (var commentRepository = new CommentsRepository(connectionString))
                    {
                        commentRepository.Add(comment);

                        Console.WriteLine("Коммент успешно добавлен!");
                    }
                }
                else if (action == 3)
                {
                    List<Article> articles;

                    using (var articlesRepository = new ArticlesRepository(connectionString))
                    {
                        articles = articlesRepository.GetAll().ToList();
                    }

                    foreach (var article in articles)
                    {
                        Console.WriteLine($"{article.Name}");
                    }
                }
                else if (action == 4)
                {
                    break;
                }

                Console.ReadLine();
                Console.Clear();
            }
        }
        
        private static Comment InitComment()
        {
            string text;

            while (true)
            {
                Console.Write("Введите текст комментария: ");
                text = Console.ReadLine();

                if (text.Length == 0)
                    Console.WriteLine("Ошибка! Попробуйте снова!");
                else
                    break;

                Console.ReadLine();
                Console.Clear();
            }

            List<Article> articles;

            using(var articlesRepository = new ArticlesRepository(connectionString))
            {
                articles = articlesRepository.GetAll().ToList();
            }

            foreach(var article in articles)
            {
                Console.WriteLine($"{article.Name}");
            }

            Console.Write("Введите полное название статьи:");
            string nameArticle = Console.ReadLine();

            Article tempArticle = articles.Where(a => a.Name.ToLower() == nameArticle.ToLower()).FirstOrDefault();

            var comment = new Comment
            {
                Text = text,
                ArticleId = tempArticle.Id
            };

            return comment;
        }

        private static Article InitArticle()
        {
            string name;
            string text;

            while (true)
            {
                Console.Write("Введите заголовок: ");
                name = Console.ReadLine();

                Console.Write("Введите текст: ");
                text = Console.ReadLine();

                if (name.Length == 0 || text.Length == 0)
                    Console.WriteLine("Ошибка! Попробуйте снова!");
                else
                    break;

                Console.ReadLine();
                Console.Clear();
            }

            var article = new Article
            {
                Name = name,
                Text = text
            };

            return article;
        }

        private static void DoMigration()
        {
            EnsureDatabase.For.SqlDatabase(connectionString);

            var upgrader = DeployChanges.To
            .SqlDatabase(connectionString)
            .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
            .LogToConsole()
            .Build();

            var result = upgrader.PerformUpgrade();

            if (!result.Successful) throw new Exception("Проблемы с базой данных!");
        }
    }
}
