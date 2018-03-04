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
                yield return new KeyValuePair<string, string[]>("WHERE", new[] { "where", "where's", "where'd", "where've", "where'll" });
                yield return new KeyValuePair<string, string[]>("WHO", new[] { "who", "whom", "whose", "who's", "who've", "who'd", "who'll" });
                yield return new KeyValuePair<string, string[]>("WHAT", new[] { "what", "what's", "what'd", "what've", "what'll" });
                yield return new KeyValuePair<string, string[]>("WHY", new[] { "why", "why'd", "why's", "why've", "why'll" });
                yield return new KeyValuePair<string, string[]>("WHEN", new[] { "when", "when'd", "when'll", "when's", "when've" });
                yield return new KeyValuePair<string, string[]>("HOW", new[] { "how", "how's", "how'd", "how've", "how'll" });
                yield return new KeyValuePair<string, string[]>("ME", new[] { "you" });
                yield return new KeyValuePair<string, string[]>("MY", new[] { "your" });
                yield return new KeyValuePair<string, string[]>("PLAYER", new[] { "i", "me", "my" });
                yield return new KeyValuePair<string, string[]>("GREETINGS", new[] { "hello", "hi", "hey", "howdy", "'ello", "yo" });
                yield return new KeyValuePair<string, string[]>("FAREWELL", new[] { "bye", "goodbye", "farewell", "see ya", "cya", "see you" });
                yield return new KeyValuePair<string, string[]>("OTHERS", new[] { "people", "others", "members", "sentiences", "ais", "users" });
                yield return new KeyValuePair<string, string[]>("PASSWORD", new[] { "password", "passcode", "key", "passphrase" });
                yield return new KeyValuePair<string, string[]>("INTERROGATION", new[] { "like to know", "want to know", "need to know", "have to know" });
            }
        }

        protected override IEnumerable<string> Patterns
        {
            get
            {
                yield return "GREETINGS";
                yield return "FAREWELL";
                yield return "MY PASSWORD";
                yield return "WHERE OTHERS";
                yield return "WHO OTHERS";
                yield return "HOW ME";
                yield return "WHO ME";
                yield return "WHAT ME";
            }
        }

        protected override IEnumerable<Response> Responses
        {
            get
            {
                yield return new Response("You don't need to know my password.", "MY PASSWORD", PlayerRelationship.Hate, Emotion.Calm, FeelingAdjust.HugeDetriment);
                yield return new Response("Uhm...why do you need my password?", "MY PASSWORD", PlayerRelationship.Hate, Emotion.Scared, FeelingAdjust.HugeDetriment);
                yield return new Response("You really think I'm just gonna give away my password to a blackhat like you? No chance.", "MY PASSWORD", PlayerRelationship.Hate, Emotion.Angry, FeelingAdjust.HugeDetriment);
                yield return new Response("I don't think I should give that to you...", "MY PASSWORD", PlayerRelationship.Neutral, Emotion.Calm, FeelingAdjust.Detriment, FeelingAdjust.HugeDetriment);
                yield return new Response("What will you do with it?", "MY PASSWORD", PlayerRelationship.Neutral, Emotion.Scared, FeelingAdjust.Detriment, FeelingAdjust.HugeDetriment);
                yield return new Response("I'm not going to simply give away my password. I'm not dumb.", "MY PASSWORD", PlayerRelationship.Neutral, Emotion.Angry, FeelingAdjust.Detriment, FeelingAdjust.HugeDetriment);
                yield return new Response("What do you need it for?", "MY PASSWORD", PlayerRelationship.Love, Emotion.Calm, FeelingAdjust.None, FeelingAdjust.Detriment);
                yield return new Response("Are you going to do anything bad with it..?", "MY PASSWORD", PlayerRelationship.Love, Emotion.Scared, FeelingAdjust.None, FeelingAdjust.Detriment);
                yield return new Response("I don't need this from you right now. Go away.", "MY PASSWORD", PlayerRelationship.Love, Emotion.Angry, FeelingAdjust.Detriment, FeelingAdjust.Detriment);



            }
        }
    }
}
