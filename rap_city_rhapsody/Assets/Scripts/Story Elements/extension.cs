using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Story/extension")]
public class extension : story_element {

	public override bool ending {
		get {
			return false;
		}
	}

	public story_element continue_to;

}
