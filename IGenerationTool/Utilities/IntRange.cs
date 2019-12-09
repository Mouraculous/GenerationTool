using System;

namespace IGenerationTool.Utilities
{
    [Serializable]
    public class IntRange
    {
        public int MinValue;
        public int MaxValue;

        public IntRange(int min, int max)
        {
            MinValue = min;
            MaxValue = max;
        }

        public int Random
        {
            get { return UnityEngine.Random.Range(MinValue, MaxValue); }
        }

    }
}
