using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterAnimator : MonoBehaviour {

	const float locomotionAnimationSmoothTime = .1f;

	Animator animator;
	Unit agent;

	void Start() {
		agent = GetComponent<Unit>();
		animator = GetComponentInChildren<Animator>();
	}

	void Update() {
		bool isMoving = agent.isMoving;
		animator.SetBool("isMoving", isMoving);
	}

}
