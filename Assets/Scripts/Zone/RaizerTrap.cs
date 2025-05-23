using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;

public class RaizerTrap : MonoBehaviour
{
    [SerializeField]
    private LayerMask layerMask;
    private bool isUseTrap = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!isUseTrap) TrapSencer();
        
    }

    void TrapSencer()
    {
        Ray[] rays = new Ray[10]
            {
                new Ray(new Vector3(7, 1, 10.5f), Vector3.forward),
                new Ray(new Vector3(8, 1, 10.5f), Vector3.forward),
                new Ray(new Vector3(9, 1, 10.5f), Vector3.forward),
                new Ray(new Vector3(10, 1, 10.5f), Vector3.forward),
                new Ray(new Vector3(11, 1, 10.5f), Vector3.forward),
                new Ray(new Vector3(12, 1, 10.5f), Vector3.forward),
                new Ray(new Vector3(13, 1, 10.5f), Vector3.forward),
                new Ray(new Vector3(14, 1, 10.5f), Vector3.forward),
                new Ray(new Vector3(15, 1, 10.5f), Vector3.forward),
                new Ray(new Vector3(16, 1, 10.5f), Vector3.forward)
            };

		for (int i = 0; i < rays.Length; i++)
		{
			GameObject lineObj = new GameObject("RayLine_" + i);
			lineObj.transform.parent = this.transform; // 계층 정리용
			LineRenderer lr = lineObj.AddComponent<LineRenderer>();

			lr.positionCount = 2;
			lr.SetPosition(0, rays[i].origin);
			lr.SetPosition(1, rays[i].origin + Vector3.forward * 10f);
			lr.startWidth = 0.05f;
			lr.endWidth = 0.05f;
			lr.material = new Material(Shader.Find("Sprites/Default"));
			lr.startColor = Color.red;
			lr.endColor = Color.red;

			if (Physics.Raycast(rays[i].origin, Vector3.forward, 10f, layerMask))
			{
                Debug.Log("사망이요");
                isUseTrap = true;
                Invoke("TrapReset", 3f);
                break;
			}
		}
	}

    void TrapReset()
    {
        isUseTrap = false;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

		for (int i = 7; i <= 16; i++)
		{
			Vector3 origin = new Vector3(i, 1, 10.5f);
			Vector3 direction = Vector3.forward;
			Gizmos.DrawRay(origin, direction * 10f); // *10f는 Ray 길이
		}
	}

    void OnDrawGizmosSelected()
    {
		Gizmos.color = Color.blue;

		for (int i = 7; i <= 16; i++)
		{
			Vector3 origin = new Vector3(i, 1, 10.5f);
			Vector3 direction = Vector3.forward;
			Gizmos.DrawRay(origin, direction * 10f); // *10f는 Ray 길이
		}
	}
}
