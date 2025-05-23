using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
	[Header("�÷��� ����")]
	[SerializeField]
	private Rigidbody player;
	private Vector3 startTriggerPosition;

	[Header("�÷��� Ÿ�̹�")]
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
			Debug.Log("�޾ƿ�");
			startTriggerPosition = other.transform.position;
			StartCoroutine("TimeOut");
		}
	}

	public void OnCollisionStay(Collision other)
	{
		if (other.gameObject.CompareTag("Platform"))
		{
			Debug.Log("�̵� ��~");
			//�÷��̾��� �� ��ġ�� = �� �÷����� ��ǥ���� ó�� ����� �� ��ǥ�� ���� �� ���� ���ض�
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

		Debug.Log("���� �ع����� ���̰�");
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
