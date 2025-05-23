using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
	[Header("플랫폼 무빙")]
	[SerializeField]
	private Rigidbody player;
	private Vector3 startTriggerPosition;

	[Header("플랫폼 타이밍")]
	[SerializeField]
	private float angleY = 45f;
	[SerializeField]
	private float distance = 10f;
	[SerializeField]
	private Vector3 direction;

	public void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.CompareTag("Platform"))
		{
			Debug.Log("받아옴");
			startTriggerPosition = other.transform.position;
			StartCoroutine("TimeOut");
		}
	}

	public void OnCollisionStay(Collision other)
	{
		if (other.gameObject.CompareTag("Platform"))
		{
			Debug.Log("이동 중~");
			//플레이어의 현 위치에 = 현 플랫폼이 좌표에서 처음 밟았을 때 좌표를 빼고 그 값을 더해라
			player.transform.position += other.transform.position - startTriggerPosition;
			startTriggerPosition = other.transform.position;
		}
	}

	public void OnCollisionExit(Collision collision)
	{
		StopCoroutine("TimeOut");
	}

	IEnumerator TimeOut()
	{
		yield return new WaitForSeconds(1.0f);

		Debug.Log("많이 해묵었다 아이가");
		TimeOutJump();
	}

	public void TimeOutJump()
	{
		player.GetComponent<PlayerKey>().isControllStop  = true;
		direction = Quaternion.AngleAxis(angleY, Vector3.right) * Vector3.up;
		direction = Quaternion.AngleAxis(player.transform.eulerAngles.y, Vector3.up) * direction;

		player.AddForce(direction.normalized * distance *player.mass, ForceMode.Impulse);
	}
}
