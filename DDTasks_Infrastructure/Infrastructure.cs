using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DDTasks_Infrastructure
{
    public class Infrastructure
    {
        private Dictionary<string, int> FileAnalyzer(string text)
        {
            char[] separators = new char[] { ' ', ',', '.', '!', '?', ';', ':', '-', '\n', '\r', '\t', '[', ']', '(', ')', '"' };
            string[] words = text.Split(separators, StringSplitOptions.RemoveEmptyEntries).Distinct().ToArray();
            
            Dictionary<string, int> dict = new Dictionary<string, int>();
            foreach (var word in words)
            {
                int count = words.Where(ex => ex.Equals(word)).Count();
                dict.Add(word, count);
            }
            return dict;
        }
        public Dictionary<string, int> ParallelFileAnalyzer(string text)
        {
            object locker = new object();
            char[] separators = new char[] { ' ', ',', '.', '!', '?', ';', ':', '-', '\n', '\r', '\t', '[', ']', '(', ')', '"' };
            string[] words = text.Split(separators, StringSplitOptions.RemoveEmptyEntries).Distinct().ToArray();
            Dictionary<string, int> dict = new Dictionary<string, int>();
            Parallel.ForEach(words, word =>
            {
                int count = words.Where(ex => ex.Equals(word)).Count();
                lock (locker) { dict.Add(word, count); }
            });
            return dict;
        }


    }
}
