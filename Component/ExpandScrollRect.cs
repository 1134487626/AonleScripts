using System;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
    [AddComponentMenu("UI/ExpandScrollRect")]
    public class ExpandScrollRect : ScrollRect
    {
        public Action<PointerEventData> ToOnScrollBeginDrag;
        public Action<PointerEventData> ToOnScrollDrag;
        public Action<PointerEventData> ToOnScrollEndDrag;

        public override void OnBeginDrag(PointerEventData eventData)
        {
            base.OnBeginDrag(eventData);
            ToOnScrollBeginDrag?.Invoke(eventData);
        }

        public override void OnDrag(PointerEventData eventData)
        {
            base.OnDrag(eventData);
            ToOnScrollDrag?.Invoke(eventData);
        }

        public override void OnEndDrag(PointerEventData eventData)
        {
            base.OnEndDrag(eventData);
            ToOnScrollEndDrag?.Invoke(eventData);
        }

    }
}
