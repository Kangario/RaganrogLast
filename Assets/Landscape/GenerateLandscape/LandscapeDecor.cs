using System;

namespace RLandscape
{
    [Serializable]
    public class LandscapeDecorArray : ILandscapeDecor
    {
        public LandscapeElementsDecor[] landscapeDecor;
    }
}