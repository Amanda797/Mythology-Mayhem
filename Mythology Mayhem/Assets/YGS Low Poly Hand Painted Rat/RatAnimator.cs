using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatAnimator : MonoBehaviour
{
	[SerializeField] Animator anim;
	public void SetMoveSpeed(float speed)
	{
		anim.SetFloat("Speed", speed);
	}
}