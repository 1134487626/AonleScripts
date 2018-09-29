using UnityEngine;

namespace UnityEngine.UI
{
    /// <summary>
    /// 
    /// </summary>
    public class UICanvas : MonoBehaviour
    {
        new Camera camera;
        RectTransform rect;

        /* 注意点：
         * 1.UI物体localPosition直接赋值的话，需要父级的布局至少一致
         * 2.
         *
         */

        private void Awake()
        {
            Canvas canvas = GetComponent<Canvas>();
            camera = canvas.worldCamera;
            rect = GetComponent<RectTransform>();
        }

        /// <summary>
        /// 世界坐标装换矩形坐标
        /// </summary>
        /// <param name="r_World"></param>
        /// <returns></returns>
        public Vector2 UIRectPos(Vector3 r_World)
        {
            Vector2 rectPos = Vector2.zero;
            Vector3 screenPos = camera.WorldToScreenPoint(r_World);
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rect, screenPos, camera, out rectPos);
            return rectPos;
        }

        /// <summary>
        /// 游戏对象的世界坐标装换矩形坐标
        /// </summary>
        /// <param name="r_form"></param>
        /// <returns></returns>
        public Vector2 UIRectPos(Transform r_form)
        {
            return UIRectPos(r_form.position);
        }

        /// <summary>
        /// 世界坐标装换UI的世界坐标
        /// </summary>
        /// <param name="r_World"></param>
        /// <returns></returns>
        public Vector3 UIWorldPos(Vector3 r_World)
        {
            Vector3 worldPos = Vector3.zero;
            Vector3 screenPos = camera.WorldToScreenPoint(r_World);
            RectTransformUtility.ScreenPointToWorldPointInRectangle(rect, screenPos, camera, out worldPos);
            return worldPos;
        }

        /// <summary>
        /// 游戏对象的世界坐标装换UI的世界坐标
        /// </summary>
        /// <param name="r_World"></param>
        /// <returns></returns>
        public Vector3 UIWorldPos(Transform r_form)
        {
            return UIWorldPos(r_form.position);
        }
    }
}