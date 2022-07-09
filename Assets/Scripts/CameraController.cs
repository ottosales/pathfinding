using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
	public float scrollSpeed;

	public float topBarrier;
	public float bottomBarrier;
	public float leftBarrier;
	public float rightBarrier;

	public float heightTopLimit = 90;
	public float heightBottomLimit = 60;
	public float zoomSpeed = 3.0f;

	public Vector3 offset;

	public Transform player;

	void Update() {
		if (Input.mousePosition.y >= Screen.height * topBarrier) {
			transform.Translate(Vector3.forward * Time.deltaTime * scrollSpeed, Space.World);
		}

		if (Input.mousePosition.y <= Screen.height * bottomBarrier) {
			transform.Translate(Vector3.back * Time.deltaTime * scrollSpeed, Space.World);
		}

		if (Input.mousePosition.x <= Screen.width * leftBarrier) {
			transform.Translate(Vector3.left * Time.deltaTime * scrollSpeed, Space.World);
		}

		if (Input.mousePosition.x >= Screen.width * rightBarrier) {
			transform.Translate(Vector3.right * Time.deltaTime * scrollSpeed, Space.World);
		}

		if (Input.GetKey("space")) {
			transform.position = player.position + offset;
		}

		if (Input.mouseScrollDelta != Vector2.zero) {
			if (Input.mouseScrollDelta.y < 0 && transform.position.y < heightTopLimit) {
				transform.position = new Vector3(transform.position.x, transform.position.y + zoomSpeed, transform.position.z - zoomSpeed * 0.75f);
				offset = new Vector3(0, offset.y + zoomSpeed, offset.z - zoomSpeed * 0.75f);
			} else if (Input.mouseScrollDelta.y > 0 && transform.position.y > heightBottomLimit) {
				transform.position = new Vector3(transform.position.x, transform.position.y - zoomSpeed, transform.position.z + zoomSpeed * 0.75f);
				offset = new Vector3(0, offset.y - zoomSpeed, offset.z + zoomSpeed * 0.75f);
			}
		}
	}
}
