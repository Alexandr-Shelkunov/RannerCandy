using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Alexander.RunnerCandy
{
    public class InitializeController : IInitializable
    {
        private readonly IReadOnlyList<IPrioritizedInitializable> initializables;
        private List<IPrioritizedInitializable> sorted;

        [Inject]
        public InitializeController(IReadOnlyList<IPrioritizedInitializable> initializables)
        {
            this.initializables = initializables;
        }

        public void Initialize()
        {
            sorted = new List<IPrioritizedInitializable>(initializables);
            // TODO: implement initialize priority 
            sorted.Sort((a, b) =>
            {
                return 0;
            });

            foreach (var initializable in sorted)
            {
                initializable.Initialize();
            }
        }
    }
}
