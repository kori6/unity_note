

using System.Security.Cryptography;
using UnityEngine;

namespace UIInterface
{
    internal interface IImage
    {
        void SetSprite(Sprite sprite);
        Sprite GetSprite();
        void SetImageColor(Color color);
        Color GetImagrColor();
    }
}
