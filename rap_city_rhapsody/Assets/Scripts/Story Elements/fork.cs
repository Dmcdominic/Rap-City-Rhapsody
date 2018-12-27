using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Story/fork")]
public class fork : story_element {
	
	public override bool ending {
		get {
			return false;
		}
	}

	public String_StoryElement_Dict options;

}
