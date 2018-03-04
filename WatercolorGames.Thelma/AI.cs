using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatercolorGames.Thelma
{
    public abstract class AI
    {
        protected abstract IEnumerable<KeyValuePair<string, string[]>> InputTags
        {
            get;
        }

        protected abstract IEnumerable<string[]> Patterns
        {
            get;
        }

        protected abstract string GetResponse(string[] pattern, PlayerReputation playerRep, Emotion emotion);

        private int Levenshtein(string s, string t)
        {
            if (s == null || t == null)
                return 0;
            int n = s.Length;
            int m = t.Length;
            if (n == 0)
                return m;
            if (m == 0)
                return n;
            int[,] matrix = new int[n+1, m+1];
            for (int i = 0; i <= n; i++)
                matrix[i, 0] = i;
            for (int i = 0; i <= m; i++)
                matrix[0, i] = i;

            for(int i = 0; i < n; i++)
            {
                for(int j = 0; j < m; j++)
                {
                    int cost = 0;
                    if (s[i] != t[j])
                        cost = 1;
                    int above = matrix[i + 1, j] + 1;
                    int left = matrix[i, j + 1] + 1;
                    int aboveleft = matrix[i, j] + cost;
                    matrix[i + 1, j + 1] = new[] { above, left, aboveleft }.OrderBy(x => x).First();
                }
            }

            return matrix[n, m];
        }

        public string Respond(string input)
        {
            Dictionary<string, int> tags = new Dictionary<string, int>();
            foreach(var tag in InputTags)
            {
                var word = tag.Value.FirstOrDefault(x => input.ToLower().Contains(x));
                if(!string.IsNullOrWhiteSpace(word))
                {
                    int index = input.ToLower().IndexOf(word);
                    if (index > 0)
                        if (char.IsLetterOrDigit(input[index - 1]))
                            continue;
                    if((index+word.Length) < input.Length)
                        if (char.IsLetterOrDigit(input[(index + word.Length)]))
                            continue;
                    tags.Add(tag.Key, index);
                }
            }

            var pattern = tags.Keys.OrderBy(x => tags[x]).ToArray();

            if (pattern.Length == 0)
                return "I don't understand.";

            string joined = string.Join(" ", pattern).Trim();
            var closestOrder = Patterns.OrderBy(x => Levenshtein(joined, string.Join(" ", x).Trim()));
            if (Levenshtein(joined, string.Join(" ", closestOrder.First()).Trim()) >= joined.Length)
                return "I don't understand.";
            return GetResponse(closestOrder.First(), PlayerReputation.Grayhat, Emotion.Calm);
        }

    }

    public enum Emotion
    {
        Angry = 0,
        Scared = 1,
        Calm = 2
    }

    public enum PlayerReputation
    {
        Blackhat = 0,
        Grayhat = 1,
        Whitehat = 2
    }
}
