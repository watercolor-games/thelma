using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatercolorGames.Thelma
{
    public abstract class AI
    {
        private float _rawEmotion = 2f;
        private float _rawRelationship = 1f;

        protected abstract IEnumerable<KeyValuePair<string, string[]>> InputTags
        {
            get;
        }

        protected abstract IEnumerable<string> Patterns
        {
            get;
        }

        protected abstract IEnumerable<Response> Responses
        {
            get;
        }
        
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

        private int GetFeeling()
        {
            return (((int)Math.Round(_rawRelationship))*3) + (int)Math.Round(_rawEmotion);
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
            var closestOrder = Patterns.OrderBy(x => Levenshtein(joined, x));
            if (Levenshtein(joined, closestOrder.First()) >= joined.Length)
                return "I don't understand.";
            var response = Responses.FirstOrDefault(x => x.Pattern == closestOrder.First() && x.Feeling == GetFeeling());
            if (string.IsNullOrWhiteSpace(response.Text))
                return "I don't understand.";
            _rawEmotion += response.EmotionIncrease;
            _rawRelationship += response.RelationshipIncrease;
            if (_rawEmotion < 0)
                _rawEmotion = 0;
            if (_rawEmotion > 2)
                _rawEmotion = 2;
            if (_rawRelationship < 0)
                _rawRelationship = 0;
            if (_rawRelationship > 2)
                _rawRelationship = 2;

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("raw emotion: {0}\nraw relationship: {1}", _rawEmotion, _rawRelationship);
            Console.ForegroundColor = ConsoleColor.White;

            return response.Text;
            
        }

    }

    public enum Emotion
    {
        Angry = 0,
        Scared = 1,
        Calm = 2
    }

    public enum PlayerRelationship
    {
        Hate = 0,
        Neutral = 1,
        Love = 2
    }

    public enum FeelingAdjust
    {
        HugeDetriment = 0,
        Detriment = 1,
        None = 2,
        Increase = 3,
        HugeIncrease = 4
    }

    public struct Response
    {
        public string Text { get; private set; }
        public int Feeling { get; private set; }
        public string Pattern { get; private set; }
        public float EmotionIncrease { get; private set; }
        public float RelationshipIncrease { get; private set; }


        public Response(string text, string pattern, PlayerRelationship rep, Emotion emotion, FeelingAdjust emotionAdjust = FeelingAdjust.None, FeelingAdjust repAdjust = FeelingAdjust.None)
        {
            Text = text;
            Pattern = pattern;
            Feeling = ((int)rep * 3) + (int)emotion;

            EmotionIncrease = 0;
            RelationshipIncrease = 0;

            switch (repAdjust)
            {
                case FeelingAdjust.HugeDetriment:
                    RelationshipIncrease = -0.25F;
                    break;
                case FeelingAdjust.Detriment:
                    RelationshipIncrease = -0.1f;
                    break;
                case FeelingAdjust.None:
                    RelationshipIncrease = 0;
                    break;
                case FeelingAdjust.Increase:
                    RelationshipIncrease = 0.1f;
                    break;
                case FeelingAdjust.HugeIncrease:
                    RelationshipIncrease = 0.25F;
                    break;
            }

            switch (emotionAdjust)
            {
                case FeelingAdjust.HugeDetriment:
                    EmotionIncrease = -0.25F;
                    break;
                case FeelingAdjust.Detriment:
                    EmotionIncrease = -0.1f;
                    break;
                case FeelingAdjust.None:
                    EmotionIncrease = 0;
                    break;
                case FeelingAdjust.Increase:
                    EmotionIncrease = 0.1f;
                    break;
                case FeelingAdjust.HugeIncrease:
                    EmotionIncrease = 0.25F;
                    break;
            }
        }
    }
}
