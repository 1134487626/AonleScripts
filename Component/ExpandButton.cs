using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
    /// <summary>
    /// 自定义的Button
    /// </summary>
    [AddComponentMenu("UI/ExpandButton")]
    public class ExpandButton : Button
    {
        public enum UserClickType
        {
            Normal,
            HandoverSprite,
        }
        /// <summary> 点击时切换使用的图片 </summary>
        [SerializeField] Sprite m_HandoverSprite;//

        [SerializeField] UserClickType m_UserClick;

        /// <summary> 点击时响应的其他对象组件 </summary>
        GameObject handoverGameobj;
        Sprite m_FirstSprite;

        private bool isFirst = true;

        public UserClickType ClickType { get { return m_UserClick; } set { m_UserClick = value; } }

        protected override void Start()
        {
            base.Start();
            m_FirstSprite = image.sprite;
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);
            OnClickFollowUp();
        }

        /// <summary> 点击后的后续回调【实现各种需求】 </summary>
        private void OnClickFollowUp()
        {
            switch (ClickType)
            {
                case UserClickType.Normal: break;
                case UserClickType.HandoverSprite: ChangTargetSprite(!isFirst); break;
            }
        }

        /// <summary> 切换Button显示的图片 true 为 默认原始的图片，false 为 HandoverSprite </summary>
        public void ChangTargetSprite(bool r_bool)
        {
            if (IsBeImage)
            {
                isFirst = r_bool;
                image.sprite = r_bool ? m_FirstSprite : m_HandoverSprite;
            }
        }

        /// <summary> 点击切换图片的时候是否设置图片的默认大小 </summary>
        public bool SetNattiveSize { get; set; } = true;

        private bool IsBeImage => image != null && m_HandoverSprite != null;
    }
}
