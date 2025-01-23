using System;
using UnityEngine;
using VContainer;

namespace Alexander.RunnerCandy
{
    public class CandyPickupChecker : IUpdatable
    {
        private readonly Transform playerTransform;
        private readonly PlayerModel playerModel;
        private readonly WeightBar weightBar;

        public LayerMask candyLayer;
        public int Priority => UpdatePriorityList.CANDY_PICKUP_CHECKER;

        public CandyPickupChecker(Transform playerTransform, PlayerModel playerModel)
        {
            this.playerTransform = playerTransform;
            this.playerModel = playerModel;
        }

        public void DoUpdate()
        {
            CheckCandyPickup();
        }

        private void CheckCandyPickup()
        {
            candyLayer = LayerMask.GetMask("Candy");
            float pickupRadius = 1.0f;
            Collider[] hitColliders = Physics.OverlapSphere(playerTransform.position, pickupRadius, candyLayer);

            foreach (var collider in hitColliders)
            {
                if (collider.CompareTag("Candy"))
                {
                    playerModel.CandyCount++;
                    UnityEngine.Object.Destroy(collider.gameObject);
                }
            }
        }
    }
}