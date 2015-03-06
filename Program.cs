using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;

namespace SearchTags
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> all_tags = new List<string>();
            List<string> find_tags = new List<string>();
            #region ReadTags
            using (SQLiteConnection connection = new SQLiteConnection("data source=\"C:\\utils\\Erza\\erza.sqlite\""))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand())
                {
                    command.CommandText = "select tags from hash_tags where tags IS NOT NULL";
                    command.Connection = connection;
                    SQLiteDataReader reader = command.ExecuteReader();
                    int count = 0;
                    while (reader.Read())
                    {
                        all_tags.AddRange(((string)reader[0]).Split(' '));
                        count++;
                        Console.Write("\r" + count.ToString("#######"));
                    }
                    reader.Close();
                    Console.WriteLine("\rВсего: " + (count++).ToString());
                }
            }
            #endregion
            all_tags = all_tags.Distinct().ToList();
            all_tags.Sort();
            foreach (string tag in all_tags)
            {
                if (tag.IndexOf(args[0]) >= 0)
                {
                    find_tags.Add(tag);
                }
            }
            Console.WriteLine("Найдено тегов: {0}", find_tags.Count);
            foreach(string s in find_tags) { Console.WriteLine(s); }
        }
    }
}
