using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class HandLayoutController : MonoBehaviour
{
    [Header("카드 배치 설정")]
    [SerializeField] private float angleRange = 60f;         // 펼쳐질 총 각도
    [SerializeField] private float radius = 400f;            // 중심으로부터 거리
    [SerializeField] private float selectedYOffset = 40f;    // 선택된 카드 튀어나옴 정도
    [SerializeField] private float sidePushOffset = 40f;     // 선택 시 양 옆 카드 밀림 정도
    [SerializeField] private float posXStep = 120f;

    [Header("카드 리스트")]
    [SerializeField] private List<CardUI> cards = new List<CardUI>();

    [SerializeField, ReadOnly] private int selectedIndex = -1;
    
    private RectTransform _rectTransform;

    protected virtual void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    public void SelectCard(CardUI selectedCard)
    {
        if (selectedIndex != cards.IndexOf(selectedCard))
        {
            selectedIndex = cards.IndexOf(selectedCard);
        }
        else
        {
            selectedIndex = -1;
        }
        UpdateLayout();
    }

    public void CancelSelection()
    {
        selectedIndex = -1;
        UpdateLayout();
    }

    public void UseCard(CardUI usedCard)
    {
        if (cards.Contains(usedCard))
        {
        }
    }

    public void AddCard(CardUI newCard)
    {
        cards.Add(newCard);
        UpdateLayout();
    }

    public void UpdateLayout()
    {
        int count = cards.Count;
        if (count == 0) return;

        // 각도 계산 (시각적 회전을 위해서만 사용)
        float angleStep = (count > 1) ? angleRange / (count - 1) : 0f;
        float startAngle = -angleRange / 2f;

        // 카드 중심 위치 (RectTransform 중심 기준)
        Vector2 center = _rectTransform.rect.center;

        for (int i = 0; i < count; i++)
        {
            float angle = startAngle + angleStep * i;
            Quaternion rotation = Quaternion.Euler(0f, 0f, -angle);

            // 카드 사이 간격은 X 기준으로 정렬
            float offsetX = (i - (count - 1) / 2f) * posXStep;
            Vector2 anchoredPos = center + new Vector2(offsetX, Mathf.Cos(Mathf.Deg2Rad * angle) * radius);

            if (i == selectedIndex)
            {
                Vector2 forwardOffset = rotation * new Vector2(0f, selectedYOffset);
                anchoredPos += forwardOffset;
            }
            else if (selectedIndex >= 0)
            {
                int dir = (i < selectedIndex) ? -1 : 1;
                anchoredPos += new Vector2(dir * sidePushOffset, 0f);
            }

            cards[i].UpdatePosition(anchoredPos);
            cards[i].UpdateRotation(rotation);
        }
    }
}
