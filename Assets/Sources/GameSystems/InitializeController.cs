using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Alexander.RunnerCandy
{
    public class InitializeController : IInitializable
    {
        [Inject]
        public InitializeController(IReadOnlyList<IPrioritizedInitializable> initializables)
        {

        }

        public void Initialize()
        {
        }
    }
}
