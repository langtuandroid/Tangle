using Tangle.ScriptableObjects;
using UnityEngine;


namespace Tangle.Uis
{
    public class RandomImagePicker
    {
        ImageContainer _unSolvedImageContainer, _solvedImageContainer;

        public RandomImagePicker(ImageContainer unSolvedImageContainer, ImageContainer solvedImageContainer)
        {
            _unSolvedImageContainer = unSolvedImageContainer;
            _solvedImageContainer = solvedImageContainer;
        }

        public Sprite PickRandomImage(bool value)
        {
            if (!value)
                return _unSolvedImageContainer.GetRandomImage();
            return _solvedImageContainer.GetRandomImage();
        }
    }
}