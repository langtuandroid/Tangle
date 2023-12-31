using System.Collections.Generic;
using UnityEngine;

namespace Tangle.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Rodd Games/Container/LevelContainer", fileName = "Level Container")]
    public class LevelContainer : ScriptableObject
    {
        [SerializeField] List<GameObject> _levelObjects;

        public GameObject GetLevel(int index)
        {
            return _levelObjects[index];
        }

        public int GetLevelCount()
        {
            return _levelObjects.Count;
        }
    }
}