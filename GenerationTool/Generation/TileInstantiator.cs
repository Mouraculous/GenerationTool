using IGenerationTool.Generation;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace GenerationTool.Generation
{
    public class TileInstantiator : ITileInstantiator
    {
        public void InstantiateFromArray(IList<GameObject> prefabs, float xCoord, float yCoord, Transform parentTransform)
        {
            var randomIndex = Random.Range(0, prefabs.Count);
            var position = new Vector3(xCoord, yCoord, 0f);
            var tileInstance = Object.Instantiate(prefabs[randomIndex], position, Quaternion.identity);
            tileInstance.transform.parent = parentTransform;
        }
    }
}
