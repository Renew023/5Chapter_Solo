using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarStat : MonoBehaviour
{
	[Header("πŸ Ω∫≈»")]
	[SerializeField]
	private float curValue;
	[SerializeField]
	private float maxValue;
	[SerializeField]
	private float startValue;

	public float CurValue { get { return curValue; } }
    

	[SerializeField]
    private Image uiBar;

	void Start()
	{
		curValue = startValue;
	}

	void Update()
	{
		ReloadBar();
	}

	void ReloadBar()
	{
		uiBar.fillAmount = curValue / maxValue;
	}

	public void Add(float value)
	{
		curValue = Mathf.Min(curValue + value, maxValue);
	}

	public void Sub(float value)
	{
		curValue = Mathf.Max(curValue - value, 0);
	}
}
