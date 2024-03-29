using System.Collections;
using UnityEngine;

public class Node : IHeapItem<Node> {
	public bool walkable;
	public Vector3 worldPosition;
	public int gridX;
	public int gridY;

	public int gCost;
	public int hCost;
	public Node parent;
	int heapIndex;

	public Node(bool _walkable, Vector3 _worldPosition, int _gridX, int _gridY) {
		walkable = _walkable;
		worldPosition = _worldPosition;
		gridX = _gridX;
		gridY = _gridY;
	}

	public int fCost {
		get {
			return gCost + hCost;
		}
	}

	public int HeapIndex {
		get {
			return heapIndex;
		}
		set {
			heapIndex = value;
		}
	}

	public int CompareTo(Node nodeToCompare) {
		int compare = fCost.CompareTo(nodeToCompare.fCost);
		if (compare == 0) {
			compare = hCost.CompareTo(nodeToCompare.hCost);

		}
		return -compare;
	}

	public static int GetDistance(Node a, Node b) {
		int distanceX = Mathf.Abs(a.gridX - b.gridX);
		int distanceY = Mathf.Abs(a.gridY - b.gridY);

		return distanceX > distanceY ? 14 * distanceY + 10 * (distanceX - distanceY) : 14 * distanceX + 10 * (distanceY - distanceX);
	}
}
