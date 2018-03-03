using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatercolorGames.Thelma.Demo
{
    public class TestAI : AI
    {
        protected override IEnumerable<KeyValuePair<string, string[]>> InputTags
        {
            get
            {
                yield return new KeyValuePair<string, string[]>("PLAYER", new string[] { "i", "me", "my", "i've", "i'd", "i'll"} );
                yield return new KeyValuePair<string, string[]>("GREETINGS", new string[] { "hey", "hello", "hi", "howdy", "'ello", "aye", "yo" });
                yield return new KeyValuePair<string, string[]>("GOODBYE", new string[] { "bye", "cya", "see ya", "see you", "gtg", "brb", "g2g" });
                yield return new KeyValuePair<string, string[]>("ME", new string[] { "you", "your", "you'll", "you'd", "you've", "you're" });
                yield return new KeyValuePair<string, string[]>("INTERROGATION", new string[] { "like to know", "need to know", "have to know", "where", "who", "what", "when", "why", "how" });
            }
        }

        protected override IEnumerable<string[]> Patterns
        {
            get
            {
                yield return new string[] { "GREETINGS" };
                yield return new string[] { "INTERROGATION", "ME" };
                yield return new string[] { "GOODBYE" };

            }
        }

        protected override string GetResponse(string[] pattern, PlayerReputation playerRep, Emotion emotion)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Right now, I am {emotion}. And you are a {playerRep}. I recognize what you said as the following pattern:");
            foreach (var tag in pattern)
                sb.Append(tag + " ");
            return sb.ToString();
        }
    }
}
