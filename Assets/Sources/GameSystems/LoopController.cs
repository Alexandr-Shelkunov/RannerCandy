using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Alexander.RunnerCandy
{
    public class LoopController : ITickable, IInitializable
    {
        private readonly IReadOnlyList<IUpdatable> updatables;
        private List<IUpdatable> sorted;

        [Inject]
        public LoopController(IReadOnlyList<IUpdatable> updatables)
        {
            this.updatables = updatables;
        }

        public void Initialize()
        {
            sorted = new List<IUpdatable>(updatables);
            sorted.Sort((a, b) =>
            {
                return a.Priority - b.Priority;
            });
        }

        // Вызов обновления для зарегистрированных объектов
        public void Tick()
        {
            foreach (var updatable in sorted)
            {
                updatable.DoUpdate();
            }
        }
    }
}