using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("╫╨еха╬╥Ы")]
    public BarStat health;
	public BarStat hunger;
	public BarStat stamina;

    void Start()
    {
        EventPlayer.jump += UseJump;
    }
    // Update is called once per frame
    void Update()
    {
        health.Sub(1*Time.deltaTime);
        hunger.Sub(1*Time.deltaTime);
        stamina.Add(1*Time.deltaTime);
    }
	public bool UseJump()
	{
        if (stamina.CurValue - 10 < 0)
        {
            return false;
        }

		stamina.Sub(10);
        return true;
	}
}
