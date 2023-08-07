using Tangle.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;


namespace Tangle.Uis
{
    public class RandomImagePicker : MonoBehaviour
    {
        [SerializeField] ImageContainer _imageContainer;
        [SerializeField] Image _image;

        void Awake()
        {
            GetReference();
        }

        void Start()
        {
            _image.sprite = PickRandomImage();
            _image.SetNativeSize();
        }

        Sprite PickRandomImage()
        {
            return _imageContainer.GetRandomImage();
        }

        void GetReference()
        {
            if (_image == null)
                _image = GetComponent<Image>();
        }

        void OnValidate()
        {
            GetReference();
        }
    }
}