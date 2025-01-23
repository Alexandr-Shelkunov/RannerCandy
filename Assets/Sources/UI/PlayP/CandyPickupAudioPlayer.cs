using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alexander.RunnerCandy
{
    public class CandyPickupAudioPlayer : IUpdatable
    {
        private readonly AudioSource audioSource;
        private readonly AudioClip candyPickupSound;
        private readonly Transform playerTransform;

        private const float PickupRadius = 1.0f;

        public int Priority => 0;

        public CandyPickupAudioPlayer(AudioSource audioSource, AudioClip candyPickupSound, Transform playerTransform)
        {
            this.audioSource = audioSource;
            this.candyPickupSound = candyPickupSound;
            this.playerTransform = playerTransform;
        }

        public void DoUpdate()
        {
            CheckCandyPickup();
            PlayPickupSound();
        }

        public void PlayPickupSound()
        {
            if (audioSource != null && candyPickupSound != null)
            {
                audioSource.PlayOneShot(candyPickupSound);
            }
        }
        private void CheckCandyPickup()
        {
            LayerMask candyLayer = LayerMask.GetMask("Candy");
            Collider[] hitColliders = Physics.OverlapSphere(playerTransform.position, PickupRadius, candyLayer);

            foreach (var collider in hitColliders)
            {
                if (collider.CompareTag("Candy"))
                {
                    PlayPickupSound();
                }
            }
        }

    }
}
