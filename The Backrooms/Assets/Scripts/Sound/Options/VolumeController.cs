using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeController : MonoBehaviour
{
	[SerializeField] private AudioMixer mainMixer;
	[Range(.0001f, 1f)]
	[SerializeField] private float volume;
	private readonly string IMPORTANT = "Use a slider value range of 0.0001 – 1";
	private void Update()
	{
		SetVolume(volume);
	}
	public void SetVolume(float _volume)
	{
		//We need the log or slider will be virtually silent ~ halfway https://gamedevbeginner.com/the-right-way-to-make-a-volume-slider-in-unity-using-logarithmic-conversion/
		mainMixer.SetFloat("Volume", Mathf.Log10(_volume) * 20);
	}
}
