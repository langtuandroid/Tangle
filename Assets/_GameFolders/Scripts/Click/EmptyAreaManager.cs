using UnityEngine;

namespace Tangle.ClickManager
{
    public class EmptyAreaManager : MonoBehaviour
    {
        public void ResetPickObject()
        {
            ClickManager.Instance.ResetFirstPick();
        }
    }
}