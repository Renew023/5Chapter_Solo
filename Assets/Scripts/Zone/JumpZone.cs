using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpZone : MonoBehaviour
{
	public void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			EventZone.JumpZone();
		}
	}
}
