﻿using System.Collections.Generic;
using UnityEngine;

namespace IGenerationTool.Generation
{
    public interface IObjectInstantiator
    {
        void InstantiateObject(IList<GameObject> prefabs, GameObject parent);
    }
}
