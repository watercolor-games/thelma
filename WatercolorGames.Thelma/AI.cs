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

            // WORDINWORD
            //           

            var pattern = tags.Keys.OrderBy(x => tags[x]).ToArray();

            var matching = Patterns.FirstOrDefault(x => x.SequenceEqual(pattern));
            if(matching!=null)
            {
                return GetResponse(matching, PlayerReputation.Grayhat, Emotion.Calm);
            }
            return "Error.";

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
