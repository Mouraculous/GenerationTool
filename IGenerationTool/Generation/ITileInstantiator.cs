using System.Collections.Generic;
using UnityEngine;

namespace IGenerationTool.Generation
{
    public interface ITileInstantiator
    {
        void InstantiateFromArray(IList<GameObject> prefabs, float xCoord, float yCoord, Transform parentTransform);
    }
}
