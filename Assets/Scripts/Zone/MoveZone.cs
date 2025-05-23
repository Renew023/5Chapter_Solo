using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public struct Point
{
	public Vector3 movePoint;
	public float moveDelay;
}

public class MoveZone : MonoBehaviour
{
	[SerializeField]
	private Rigidbody rb;
	[SerializeField]
	private float timer = 0f;
	[SerializeField]
	private int count = 0;

	[SerializeField]
	private Point[] point;

	void Awake()
    {
		rb = GetComponent<Rigidbody>();
        StartCoroutine("MoveTrans");
	}

    IEnumerator MoveTrans()
    {
		while (true)
		{
			if (count >= point.Length)
			{
				count = 0;
			}

			Vector3 startPos = transform.position;
			//* Time.deltaTime / point[count].moveDelay;
			while (timer < point[count].moveDelay)
			{
				timer += Time.deltaTime;
				rb.MovePosition(Vector3.Lerp(startPos, point[count].movePoint, 1*(timer/point[count].moveDelay)));
				//transform.position = Vector3.Lerp(startPos, 
				//	point[count].movePoint, 1 * (timer / point[count].moveDelay));
				yield return null;
			}
			Debug.Log(transform.position);
			timer = 0;
			count++;

			yield return new WaitForSeconds(2f);
		}
    }
}
