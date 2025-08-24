using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardUI : MonoBehaviour
{
    public Image ImageBackground;
    public Image ImagePattern;
    public TextMeshProUGUI TextNum;
    public AnimationCurve CardMoveCurve;
    
    protected HandLayoutController _handLayoutController;
    
    protected RectTransform _rectTransform;

    protected Vector3 _desiredMovePosition;
    protected Vector3 _startMovePosition;
    
    protected Quaternion _desiredRotation;
    protected Quaternion _startRotation;

    protected Vector3 _desiredScale;
    protected Vector3 _currentScale;

    public float MoveTotalTime = 0.5f;
    protected float _moveElapsedTime = 0f;
    
    protected virtual void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _handLayoutController = GetComponentInParent<HandLayoutController>();
    }

    protected virtual void Start()
    {
        _handLayoutController.AddCard(this);
    }
    protected virtual void Update()
    {
        if (_moveElapsedTime < MoveTotalTime)
        {
            _moveElapsedTime += Time.deltaTime;
            float curveT = CardMoveCurve.Evaluate(_moveElapsedTime / MoveTotalTime);
            MoveTo(curveT);
            RotateTo(curveT);
            ScaleTo(curveT);
        }
    }

    protected virtual void MoveTo(float curveT)
    {
        _rectTransform.anchoredPosition = Vector3.Lerp(_startMovePosition, _desiredMovePosition, curveT);
    }

    protected virtual void RotateTo(float curveT)
    {
        _rectTransform.rotation = Quaternion.Lerp(_startRotation, _desiredRotation, curveT);
    }

    protected virtual void ScaleTo(float curveT)
    {
        
    }
    
    public virtual void UpdatePosition(Vector3 position)
    {
        _moveElapsedTime = 0f;
        _desiredMovePosition = position;
        _startMovePosition = _rectTransform.anchoredPosition;
    }
    
    public virtual void UpdateRotation(Quaternion rotation)
    {
        _moveElapsedTime = 0f;
        _desiredRotation = rotation;
        _startRotation = _rectTransform.rotation;
    }
    public virtual void UpdateScale(Vector3 scale)
    {
        
    }

    public void OnSelected()
    {
        Debug.Log("OnSelected");
        _handLayoutController.SelectCard(this);
    }
}
