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
	public GameObject startUI;
	public GameObject restartUI;
	public GameObject startPoint;
	public GameObject[] showPoints;

	private uint showPointIndex = 0;

	void Start()
	{
		showPointIndex = 0;
		startUI.SetActive(true);
		restartUI.SetActive(false);
		player.transform.position = startPoint.transform.position;
		player.transform.eulerAngles = startPoint.transform.eulerAngles;

		foreach (GameObject showPoint in showPoints) {
			Transform godCanvasTransform = showPoint.transform.Find ("GodCanvas");
			if (godCanvasTransform) {
				godCanvasTransform.Find ("Panel").gameObject.SetActive (false);
			}
		}
	}

	public void StartButtonClicked()
	{
		startUI.SetActive(false);
		MoveToNextPoint ();
	}
		

	public void MoveToNextPoint()
	{
		if (showPointIndex < showPoints.Length) {
			if (showPointIndex > 0) {
				GameObject godCanvas = showPoints [showPointIndex - 1].transform.Find ("GodCanvas").Find ("Panel").gameObject;
				godCanvas.SetActive (false);
			}

			Vector3 nextPoint = showPoints [showPointIndex].transform.position;
			float distance = Vector3.Distance (player.transform.position, nextPoint);
			iTween.MoveTo(player, 
				iTween.Hash(
					"position", showPoints[showPointIndex].transform.position,
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
		if (showPointIndex >= showPoints.Length) {
			restartUI.SetActive (true);
		} else {
			GameObject godCanvas = showPoints [showPointIndex - 1].transform.Find ("GodCanvas").Find ("Panel").gameObject;
			godCanvas.SetActive (true);
		}
	}

	public void Restart()
	{
		Start ();
	}
}
