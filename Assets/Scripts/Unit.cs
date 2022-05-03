using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour {
	public Transform target;
	public bool isMoving = false;
	public float speed = 25;
	int rotationSpeed = 7;
	Vector3[] path;
	int targetIndex;

	void Start() {

	}

	void Update() {
		if (Input.GetButtonDown("Fire2")) {
			Vector3 mousePosition = Input.mousePosition;
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(mousePosition);
			if (Physics.Raycast(ray, out hit)) {
				Vector3 targetPoint = hit.point;
				PathRequestManager.RequestPath(transform.position, targetPoint + new Vector3(0, 3.6f, 0), OnPathFound);
			}
		}
	}

	public void OnPathFound(Vector3[] newPath, bool pathSuccessful) {
		if (pathSuccessful) {
			path = newPath;
			targetIndex = 0;
			StopCoroutine("FollowPath");
			StartCoroutine("FollowPath");
			isMoving = true;
		}
	}

	IEnumerator FollowPath() {
		Vector3 currentWaypoint = path[0];
		while (true) {
			if (transform.position == currentWaypoint) {
				targetIndex++;
				if (targetIndex >= path.Length) {
					isMoving = false;
					yield break;
				}
				currentWaypoint = path[targetIndex];
			}
			Vector3 pointVector = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
			transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
			Quaternion targetRotation = Quaternion.LookRotation(currentWaypoint - transform.position);
			transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

			yield return null;
		}
	}

	public void OnDrawGizmos() {
		if (path != null) {
			for (int i = targetIndex; i < path.Length; i++) {
				Gizmos.color = Color.black;
				Gizmos.DrawCube(path[i], Vector3.one);

				if (i == targetIndex) {
					Gizmos.DrawLine(transform.position, path[i]);
				} else {
					Gizmos.DrawLine(path[i - 1], path[i]);
				}
			}
		}
	}
}