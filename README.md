# `thelma`

`thelma` is the name of a character within The Peacenet who plays a very 
important role in the game's storyline. As are all other characters in 
The Peacenet, she is an AI.

This code is what powers her ability to naturally respond to you based on 
what you say, and how she feels about you. This code also powers all 
other NPCs in The Peacenet, in both Multiplayer and Singleplayer modes.

This algorithm is based off of the algorithm used by Kaizen from 
event[0]. You can see how it works in this short video.

https://www.youtube.com/watch?v=bCJw4hQkPj4	

### Things already working

 - Tag recognition: input text is parsed into a series of tags, in order 
of where they appear in the input string.
 - Pattern recognition: When the input tags are retrieved, a Levenshtein 
Distance algorithm is applied to find the closest valid tag pattern 
understood by the AI. If the closest distance is above the length of the 
input tag list, the AI won't understand you.

### To-do

 - Dynamic emotions: right now the AI's emotion is hard-coded to Calm. 
The emotion should change based on what the player says, and the emotion 
should affect how the AI responds.
 - Integration with Peacenet: so we can actually grab the player's 
reputation.
 - Synonyms: being able to alter the output message replacing certain 
words with random synonyms so that the AI doesn't repeat the same stuff.
