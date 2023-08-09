using System.Collections;
using DG.Tweening;
using Tangle.Levels;
using Tangle.ScriptableObjects;
using Tangle.Uis;
using UnityEngine;
using UnityEngine.UI;

namespace Tangle.Line
{
    public class LineDrawerTest : MonoBehaviour
    {
        [SerializeField] LineRenderer _lineRenderer;
        [SerializeField] PolygonCollider2D _polygonCollider;
        [SerializeField] Image _selectedDotImage, _selectedAnimationImage, _dotImage;
        [SerializeField] ImageContainer _solvedImageContainer, _unsolvedImageContainer;
        [SerializeField] LineDrawerTest _pairedLineDraver;
        RandomImagePicker _randomImagePicker;
        Tween _rotateTween, _scaleTween;
        public int triggerThreshold = 3; // Tetikleme için üst üste gelme eşiği
        int triggerCounter = 0; // Tetikleme için sayaç
        [field: SerializeField] public bool IsRed { get; private set; }
        public bool IsPingObject { get; set; }
        public bool _isMoving;

        void Awake()
        {
            _randomImagePicker = new RandomImagePicker(_unsolvedImageContainer, _solvedImageContainer);
        }

        void Start()
        {
            SetDotImage();
        }

        void Update()
        {
            if (_isMoving)
                UpdateLine();
            //Debug.Log(gameObject.transform.parent.name + " counter : " + triggerCounter);
        }

        public void UpdateLine()
        {
            DrawToNext();
            AddPolygonCollider();
        }

        void DrawToNext()
        {
            if (IsLastObject())
            {
                var firstLineDrawer = FindFirstLineDrawer();
                if (firstLineDrawer != null)
                {
                    Vector3[] positions = { transform.position, firstLineDrawer.transform.position };
                    _lineRenderer.positionCount = 2;
                    _lineRenderer.SetPositions(positions);
                }
            }
            else
            {
                var nextChild = FindNextChildWithLineDrawer(transform);
                if (nextChild != null)
                {
                    Vector3[] positions = { transform.position, nextChild.transform.position };
                    _lineRenderer.positionCount = 2;
                    _lineRenderer.SetPositions(positions);
                }
            }
        }


        LineDrawerTest FindNextChildWithLineDrawer(Transform current)
        {
            var grandparent = current.parent.parent; // Assuming the grandparent is the common parent of LineDrawerTest objects.
            var siblingIndex = current.parent.GetSiblingIndex();
            var nextIndex = (siblingIndex + 1) % grandparent.childCount;
            var nextChild = grandparent.GetChild(nextIndex);

            var lineDrawer = nextChild.GetComponentInChildren<LineDrawerTest>();
            if (lineDrawer != null)
            {
                lineDrawer._pairedLineDraver = this;
                // Next child has LineDrawerTest component, return it.
                return lineDrawer;
            }
            else
                // Next child does not have LineDrawerTest component, continue searching.
            {
                return FindNextChildWithLineDrawer(nextChild);
            }
        }

        LineDrawerTest FindFirstLineDrawer()
        {
            var grandParent = transform.parent.parent;
            var grandParentChildCount = grandParent.childCount;

            // Döngü grandparent'in child objelerini yukarıdan aşağı doğru kontrol eder.
            for (var i = 0; i < grandParentChildCount; i++)
            {
                var parent = grandParent.GetChild(i);
                var parentChildCount = parent.childCount;

                // Parent'in child objelerini yukarıdan aşağı doğru kontrol eder.
                for (var j = 0; j < parentChildCount; j++)
                {
                    var child = parent.GetChild(j);
                    if (child.TryGetComponent(out LineDrawerTest lineDrawer))
                    {
                        lineDrawer._pairedLineDraver = this;
                        return lineDrawer;
                    }
                }
            }

            return null;
        }


        bool IsLastObject()
        {
            var grandparent = transform.parent.parent; // Assuming the grandparent is the common parent of LineDrawerTest objects.
            var siblingIndex = transform.parent.GetSiblingIndex();
            var nextIndex = (siblingIndex + 1) % grandparent.childCount;
            return nextIndex == 0;
        }

        void AddPolygonCollider()
        {
            if (!_polygonCollider.enabled) return;
            var startPoint = _lineRenderer.GetPosition(0);
            var endPoint = _lineRenderer.GetPosition(_lineRenderer.positionCount - 1);

            var direction = endPoint - startPoint;
            var perpendicular = new Vector3(-direction.y, direction.x, 0f).normalized;

            var topLeft = startPoint + perpendicular * (_lineRenderer.startWidth / 2f);
            var bottomLeft = startPoint - perpendicular * (_lineRenderer.startWidth / 2f);
            var topRight = endPoint + perpendicular * (_lineRenderer.endWidth / 2f);
            var bottomRight = endPoint - perpendicular * (_lineRenderer.endWidth / 2f);

            var colliderPoints = new Vector2[4];
            colliderPoints[0] = _lineRenderer.transform.InverseTransformPoint(topLeft);
            colliderPoints[1] = _lineRenderer.transform.InverseTransformPoint(bottomLeft);
            colliderPoints[2] = _lineRenderer.transform.InverseTransformPoint(bottomRight);
            colliderPoints[3] = _lineRenderer.transform.InverseTransformPoint(topRight);

            _polygonCollider.points = colliderPoints;
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            triggerCounter++;
            SetDotImage();
            if (triggerCounter >= triggerThreshold)
            {
                IsRed = true;
                _lineRenderer.startColor = Color.white; // Line Renderer'ın başlangıç rengini kırmızı yap
                _lineRenderer.endColor = Color.white;
            }
        }

        void OnTriggerExit2D(Collider2D other)
        {
            triggerCounter--;
            SetDotImage();
            triggerCounter = Mathf.Max(triggerCounter, 0);
            if (triggerCounter < triggerThreshold)
            {
                IsRed = false;
                _lineRenderer.startColor = Color.green; // Line Renderer'ın başlangıç rengini kırmızı yap
                _lineRenderer.endColor = Color.green;
                //SetDotImage(true);
            }
        }

        void SetDotImage()
        {
            // if (triggerCounter <= 2)
            //     _dotImage.sprite = _randomImagePicker.PickRandomImage(true);
            // else
            //     _dotImage.sprite = _randomImagePicker.PickRandomImage(false);
            _dotImage.sprite = _randomImagePicker.PickRandomImage(false);
        }

        public void ClickEvent()
        {
            ClickManager.ClickManager.Instance.SetDotObject(this);
        }

        public void CloseLine()
        {
            if (IsPingObject)
                LevelManager.Instance.UpdateAllActieveLines(true);
            triggerCounter = 0;
            //_polygonCollider.enabled = false;
            //_lineRenderer.enabled = false;
        }

        void OpenLine()
        {
            ClickManager.ClickManager.Instance.CanClickAble = true;
            UpdateLine();
            _polygonCollider.enabled = !_polygonCollider.enabled;
            _polygonCollider.enabled = !_polygonCollider.enabled;
            if (IsPingObject)
            {
                PingLevelManager();
                LevelManager.Instance.UpdateAllActieveLines(false);
                IsPingObject = false;
            }

            SetIsMoving(false);
        }

        public void HandleOnSelect()
        {
            _selectedDotImage.enabled = true;
            _selectedAnimationImage.enabled = true;
            if (!_rotateTween.IsActive() && !_scaleTween.IsActive())
            {
                _rotateTween = _selectedAnimationImage.transform
                    .DORotate(new Vector3(0, 0, 360), 4f, RotateMode.FastBeyond360)
                    .SetLoops(-1, LoopType.Restart)
                    .SetEase(Ease.Linear);
                _scaleTween = _selectedDotImage.transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), .5f)
                    .SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
            }

            else
            {
                _rotateTween.Play();
                _scaleTween.Play();
            }
        }

        public void HandleOnDeselect()
        {
            if (IsPingObject && !_isMoving)
                IsPingObject = false;
            _selectedDotImage.enabled = false;
            _selectedAnimationImage.enabled = false;
            _rotateTween.Pause();
            _scaleTween.Pause();
        }

        public void StartMovement(Transform newTransform)
        {
            /*if (_selectedAnimationImage.enabled)
                _selectedDotImage.enabled = false;
            var sequence = DOTween.Sequence();
            sequence.Append(transform.parent.DOMove(newTransform.position, .5f))
                .OnComplete(() =>
                {
                    var delayDuration = .2f;
                    DOTween.Sequence()
                        .AppendInterval(delayDuration)
                        .AppendCallback(() => PingLevelManager());
                });
            sequence.AppendCallback(OpenLine);*/
            if (_selectedAnimationImage.enabled)
                _selectedDotImage.enabled = false;

            var sequence = DOTween.Sequence();
            sequence.AppendCallback(CloseLine)
                .Append(transform.parent.DOMove(newTransform.position, .3f))
                .AppendCallback(OpenLine);
            /*sequence.Append(transform.parent.DOMove(newTransform.position, 5f)).AppendCallback(AppendEndTest);
            sequence.Append(AppendStartTest)*/
        }

        void AppendEndTest()
        {
            Debug.Log("End of movement");
        }

        void AppendStartTest()
        {
            Debug.Log("Start of movement");
        }

        void PingLevelManager()
        {
            if (!IsPingObject) return;
            StartCoroutine(PingLevelManagerRoute());
            IsPingObject = false;
        }

        IEnumerator PingLevelManagerRoute()
        {
            yield return new WaitForSeconds(.5f);
            LevelManager.Instance.CheckLevelComplete();
        }

        void OnDisable()
        {
            _rotateTween.Kill();
            _scaleTween.Kill();
        }

        public void SetIsMoving(bool value)
        {
            _isMoving = value;
        }
    }
}