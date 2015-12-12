using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace LKAnimation
{
	public class AnimationFrames : MonoBehaviour 
	{
		public UITexture MainTexture;

		public List<Texture> Frames;

		// Change frame interval
		public float Interval = 1.0f;

		// Stop
		public bool IsStopped = false;

		// Play
		void Play()
		{
			if (this.Frames == null || this.Frames.Count <= 0)
				return;

			int index = (int)((Time.time * (1.0f / this.Interval)) % this.Frames.Count);
			index = Mathf.Clamp(index, 0, this.Frames.Count - 1);
			this.MainTexture.mainTexture = this.Frames[index];
		}

		void Update() 
		{
			if (this.IsStopped)
				return;

			this.Play();
		}
	}
}
