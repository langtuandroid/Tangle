using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Tangle.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Rodd Games/Container/Image", fileName = "ImageContainer")]
    public class ImageContainer : ScriptableObject
    {
        [SerializeField] List<Sprite> _images;

        public Sprite GetRandomImage()
        {
            return _images[Random.Range(0, _images.Count)];
        }
    }
}