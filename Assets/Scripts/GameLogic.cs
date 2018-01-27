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

	private uint showPointIndex = 0;

	void Start()
	{
		startUI.SetActive(true);
		restartUI.SetActive(false);
		nextPointCanvas.SetActive (false);
		player.transform.position = startPoint.transform.position;
		player.transform.rotation = startPoint.transform.rotation;
	}

	public void StartButtonClicked()
	{
		startUI.SetActive(false);
		MoveToNextPoint ();
	}

	public void MoveToNextPoint()
	{
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
			restartUI.SetActive(true);
		}else if (showPoint[showPointIndex-1].tag == "ShowPoint") {
			nextPointCanvas.SetActive (true);
		} else {
			MoveToNextPoint ();
		}
	}

	public void Restart()
	{
		Start ();
	}
}
