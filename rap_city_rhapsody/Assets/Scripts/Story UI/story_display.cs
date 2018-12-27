using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class story_display : MonoBehaviour {

	public master_config master_Config;

	public Text description;
	public GameObject options_parent;
	public Button option_prefab;
	public Image timer_image;

	public story_element the_beginning;

	private float timer;
	private bool timing_down = false;


	// Initialization
	private void Start() {
		init();
	}

	private void init() {
		display_element(the_beginning);
	}

	// Display a given story element
	public void display_element(story_element element) {
		// Check that the element exists
		if (element == null) {
			display_unimplemented("[unknown]");
			return;
		}

		// Reset the timer and clear options
		reset_timer();
		clear_options();

		// Display the description
		description.text = element.description;

		// Use helper function(s) to display class-specific components
		if (element is fork) {
			display_fork_helper((fork)element);
		} else if (element is extension) {
			display_extension_helper((extension)element);
		}
	}

	// Helper function to display the fork-specific components
	private void display_fork_helper(fork element) {
		foreach (string option in element.options.Keys) {
			create_button(option, element.options[option]);
		}
	}

	// Helper function to display the extension-specific components
	private void display_extension_helper(extension element) {
		// This can be improved to look better. Just a big ol' continue button right now.
		create_button("Continue...", element.continue_to);
	}

	private void create_button(string option, story_element element) {
		Button new_button = Instantiate(option_prefab);
		new_button.transform.SetParent(options_parent.transform);
		new_button.gameObject.SetActive(true);

		if (element) {
			new_button.GetComponentInChildren<Text>().text = option;
			new_button.onClick.AddListener(option_listener(option, element));
		} else {
			new_button.GetComponentInChildren<Text>().text = option + " [Not implemented]";
			new_button.interactable = false;
		}
	}

	// The UnityAction that will be triggered when an option is selected
	private UnityAction option_listener(string option, story_element next_element) {
		return () => option_selected_safe(option, next_element);
	}
	// Displays the next element, if it exists, and displays unimplemented otherwise
	private void option_selected_safe(string option, story_element next_element) {
		if (next_element) {
			display_element(next_element);
		} else {
			display_unimplemented(option);
		}
	}

	// Inform the user that a given option has not been implemented
	private void display_unimplemented(string option) {
		string unimplemented_text = "Story Error: Option \"" + option + "\" is unimplemented";
		Debug.LogWarning(unimplemented_text);
		description.text = unimplemented_text;
		clear_options();
	}

	// Clear all existing options from the display
	private void clear_options() {
		foreach (Transform child in options_parent.transform) {
			Destroy(child.gameObject);
		}
	}


	// ============ TIMER MANAGEMENT ============

	// Reset the timer to its appropriate time and start ticking down
	private void reset_timer() {
		timer = master_Config.time_per_element;
		update_timer_image();
		if (!master_Config.time_limited) {
			return;
		}
		timing_down = true;
	}

	private void Update() {
		if (timing_down) {
			timer -= Time.deltaTime;
			update_timer_image();

			if (timer <= 0) {
#if UNITY_EDITOR
				UnityEditor.EditorApplication.isPlaying = false;
#else
				Application.Quit();
#endif
			}
		}
	}

	private void update_timer_image() {
		timer_image.fillAmount = timer / master_Config.time_per_element;
	}

}
