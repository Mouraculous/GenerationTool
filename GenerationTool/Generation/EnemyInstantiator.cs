using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GenerationTool.Utilities;
using IGenerationTool.Generation;
using IGenerationTool.Utilities;
using UnityEngine;

namespace GenerationTool.Generation
{
    class EnemyInstantiator : IObjectInstantiator
    {
        public IntRange EnemiesNumber;
        private readonly ITileInstantiator _tileInstantiator;

        public EnemyInstantiator(ITileInstantiator tileInstantiator)
        {
            _tileInstantiator = tileInstantiator;
        }

        public void InstantiateObject(IList<GameObject> prefabs, float xCoord, float yCoord, GameObject parent)
        {
            var enemies = EnemiesNumber.Random;
            var floors = GameObject.FindGameObjectsWithTag("Floor");

            for (var i = 0; i < enemies; i++)
            {
                var idx = UnityEngine.Random.Range(0, floors.Length);
                var floor = floors[idx];
                var pos = floor.transform.position;

                _tileInstantiator.InstantiateFromArray(prefabs, pos.x, pos.y, parent.transform);
                floors = floors.Where(w => w != floor).ToArray();
            }
        }
    }
}
