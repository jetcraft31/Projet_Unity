using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

	public Gradient gradient;
	private Slider slider;
	public Image fill;
	private bool immortalityOn = false;

    void Awake()
    {
		slider = this.gameObject.GetComponent<Slider>();
    }

    public void SetMaxHealth(int health)
	{
		slider.maxValue = health;
		slider.value = health;

		fill.color = gradient.Evaluate(1f);
	}

    public void SetHealth(int health)
	{
		slider.value = health;
		if(!immortalityOn)
			fill.color = gradient.Evaluate(slider.normalizedValue);
	}

	public void Immortality(float time)
	{
		immortalityOn = true;
		StartCoroutine(immortality(time));
	}

	IEnumerator immortality(float time)
	{
        while (time > 0) 
		{
			fill.color = Color.gray;
			yield return new WaitForSeconds(0.5f);

			fill.color = gradient.Evaluate(slider.normalizedValue);
			yield return new WaitForSeconds(0.5f);
			time -= 1f;
		}
		immortalityOn = false;
	}

}
