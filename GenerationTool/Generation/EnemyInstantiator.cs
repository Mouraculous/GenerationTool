using IGenerationTool.Generation;
using IGenerationTool.Utilities;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GenerationTool.Generation
{
    public class EnemyInstantiator : IObjectInstantiator
    {
        public IntRange EnemiesNumber;
        private readonly ITileInstantiator _tileInstantiator;

        public EnemyInstantiator(ITileInstantiator tileInstantiator)
        {
            _tileInstantiator = tileInstantiator;
        }

        public void InstantiateObject(IList<GameObject> prefabs, GameObject parent)
        {
            var enemies = EnemiesNumber.Random;
            var floors = GameObject.FindGameObjectsWithTag("Floor");

            for (var i = 0; i < enemies; i++)
            {
                var idx = Random.Range(0, floors.Length);
                var floor = floors[idx];
                var pos = floor.transform.position;

                _tileInstantiator.InstantiateFromArray(prefabs, pos.x, pos.y, parent.transform);
                floors = floors.Where(w => w != floor).ToArray();
            }
        }
    }
}
