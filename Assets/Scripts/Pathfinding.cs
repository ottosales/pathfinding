using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class Pathfinding : MonoBehaviour {

	public Transform seeker, target;

	Grid grid;

	void Awake() {
		grid = GetComponent<Grid>();
	}

	void Update() {
		if (Input.GetButtonDown("Jump")) {
			FindPath(seeker.position, target.position);
		}
	}

	void FindPath(Vector3 startPos, Vector3 targetPos) {
		Stopwatch sw = new Stopwatch();
		sw.Start();
		Node startNode = grid.NodeFromWorldPoint(startPos);
		Node targetNode = grid.NodeFromWorldPoint(targetPos);

		Heap<Node> openSet = new Heap<Node>(grid.MaxSize);
		HashSet<Node> closedSet = new HashSet<Node>();
		openSet.Add(startNode);

		while (openSet.Count > 0) {
			Node currentNode = openSet.RemoveFirst();
			closedSet.Add(currentNode);

			if (currentNode == targetNode) {
				sw.Stop();
				print("Path found: " + sw.ElapsedMilliseconds + " ms");
				RetracePath(startNode, targetNode);
				return;
			}

			foreach (Node neighbour in grid.GetNeighbouts(currentNode)) {
				if (!neighbour.walkable || closedSet.Contains(neighbour)) {
					continue;
				}

				int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);
				if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour)) {
					neighbour.gCost = newMovementCostToNeighbour;
					neighbour.hCost = GetDistance(neighbour, targetNode);
					neighbour.parent = currentNode;

					if (!openSet.Contains(neighbour))
						openSet.Add(neighbour);
				}
			}
		}
	}

	void RetracePath(Node startNode, Node endNode) {
		List<Node> path = new List<Node>();
		Node currentNode = endNode;

		while (currentNode != startNode) {
			path.Add(currentNode);
			currentNode = currentNode.parent;
		}
		path.Reverse();

		grid.path = path;
	}

	int GetDistance(Node a, Node b) {
		int distanceX = Mathf.Abs(a.gridX - b.gridX);
		int distanceY = Mathf.Abs(a.gridY - b.gridY);

		return distanceX > distanceY ? 14 * distanceY + 10 * (distanceX - distanceY) : 14 * distanceX + 10 * (distanceY - distanceX);
	}
}
