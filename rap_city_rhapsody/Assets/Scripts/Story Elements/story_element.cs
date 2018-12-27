using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* story_element:
 * The fundamental element of a story.
 * Can lead to one or more further story elements, or conclude the story.
 */
public abstract class story_element : ScriptableObject {

	// Whether or not the story concludes here.
	public abstract bool ending { get; }

	// The event description that will be presented to the player.
	public string description;

}

[System.Serializable]
public class String_StoryElement_Dict : SerializableDictionary<string, story_element> { }
