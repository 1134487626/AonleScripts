
namespace UnityEngine.UI
{
    /// <summary>
    /// 自定义UI
    /// </summary>
    [AddComponentMenu("UI/ExpandImage")]
    public class ExpandImage : Image
    {
        [SerializeField] Sprite[] m_HandoverSprite;

        /// <summary>
        /// 设置切换Image组件图片
        /// </summary>
        /// <param name="r_index"></param>
        public void SetHandoverSprite(int r_index)
        {
            if (m_HandoverSprite?.Length > 0)
            {
                if (r_index >= 0 && r_index < m_HandoverSprite.Length)
                {
                    sprite = m_HandoverSprite[r_index];
                }
                else
                {
                    Debug.LogError($"ExpandImage 传入的索引出错:{r_index}");
                }
            }
        }

    }
}
