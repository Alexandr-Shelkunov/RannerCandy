using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alexander.RunnerCandy
{
    public static class UpdatePriorityList
    {
        public const int INPUT = 0;
        public const int LEVEL_GENERATOR = INPUT + 1;
        public const int PLAYER_MOVEMENT = LEVEL_GENERATOR + 1;
        public const int CANDY_PICKUP_CHECKER = PLAYER_MOVEMENT + 1;
        public const int OBSTACLE_COLLISION_CHECKER = CANDY_PICKUP_CHECKER + 1;
    }
}
