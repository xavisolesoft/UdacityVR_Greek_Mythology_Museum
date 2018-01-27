using UnityEngine;
using System.Collections;

/* Final
 * This script represents what your GameLogic.cs script might look like at the end of the course.
 * 
 * Do not use this script directly as the file and class names are incorrect.
 */
public class GameLogic : MonoBehaviour
{
	public float movementSpeed = 2;
	public float rotationTime = 5;

	public GameObject player;
	public GameObject eventSystem;
	public GameObject nextPointCanvas;
	public GameObject startUI;
	public GameObject restartUI;
	public GameObject startPoint;
	public GameObject[] showPoint;
	public GameObject restartPoint;

	private uint showPointIndex = 0;

	void Start()
	{
		player.transform.position = startPoint.transform.position;
	}

	public void MoveToNextPoint()
	{ 
		//startUI.SetActive(false);

		if (showPointIndex < showPoint.Length) {
			nextPointCanvas.SetActive (false);
			Vector3 nextPoint = showPoint [showPointIndex].transform.position;
			float distance = Vector3.Distance (player.transform.position, nextPoint);


			iTween.MoveTo(player, 
				iTween.Hash(
					"position", showPoint[showPointIndex].transform.position,
					"orienttopath", true,
					"lookTime", rotationTime,
					"time", distance/movementSpeed, 
					"easetype", "linear",
					"oncomplete", "MoveToNextPointComplete",
					"oncompletetarget", this.gameObject
				)
			);

			showPointIndex++;
		}
	}

	public void MoveToNextPointComplete()
	{
		if (showPointIndex >= showPoint.Length) {
			//TODO: open restart dialog.
		}else if (showPoint[showPointIndex-1].tag == "ShowPoint") {
			nextPointCanvas.SetActive (true);
		} else {
			MoveToNextPoint ();
		}
	}

	// Reset the puzzle sequence.
	public void ResetPuzzle()
	{
		// Enable the start UI.
		startUI.SetActive(true);

		// Disable the restart UI.
		restartUI.SetActive(false);

		// Move the player to the start position.
		player.transform.position = startPoint.transform.position;
	}

	// Do this when the player solves the puzzle.
	public void PuzzleSuccess()
	{
		// Enable the restart UI.
		restartUI.SetActive(true);

		// Move the player to the restart position.
		iTween.MoveTo(player, 
			iTween.Hash(
				"position", restartPoint.transform.position, 
				"time", 4, 
				"easetype", "linear"
			)
		);
	}
}
