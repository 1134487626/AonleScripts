using UnityEngine;
using UnityEngine.UI;

namespace Anole
{
    /// <summary>
    /// 局部对象池单个对象类
    /// 支持面向对象扩展功能
    /// </summary>
    public sealed partial class ObjectPoolItem : ObjectPoolTags
    {
        /// <summary> 返回在对象池中的自身 </summary>
        public GameObject Obj { get; private set; }

        /// <summary> 返回自身的Transform </summary>
        public Transform Trans
        {
            get
            {
                if (Exists())
                {
                    return Obj.transform;
                }
                return null;
            }
        }

        //用以闲置时归类的同一父对象【方便Hierarchy查看】
        private Transform idleParent;

        private RectTransform rect;

        /// <summary> 返回自身的RectTransform </summary>
        public RectTransform Rect
        {
            get
            {
                if (rect == null && Exists())
                {
                    rect = Obj.GetComponent<RectTransform>();
                    if (rect == null)
                    {
                        DeBug.LogError("注意：该对象没有 RectTransform 组件！");
                    }
                }
                return rect;
            }
        }

        public string ResourcesPath { get; private set; }

        /// <summary> 是否为闲置状态 根据此状态来获取实例化对象相对安全 </summary>
        public bool Idle { get; set; }

        private ObjectPoolItem() { }

        /// <summary> 构造赋值对象池中的对象 </summary>
        public ObjectPoolItem(GameObject r_obj, Transform r_parent, string r_path)
        {
            if (r_obj == null) DeBug.LogError("注意：传入的对象为空!");
            else
            {
                Obj = r_obj;
                ResourcesPath = r_path;
                idleParent = r_parent;
            }
        }

        /// <summary> 该游戏对象是否为空，如果为空一定是在某个地方被删除了 </summary>
        public bool Exists()
        {
            return Obj != null;
        }

        /// <summary> 设置为闲置状态 </summary>
        public void SetTheIdle()
        {
            if (Exists())
            {
                if (!Idle)
                {
                    DeBug.LogError("注意：该对象未设置为闲置状态！");
                    Idle = true;
                }

                SetActive(false);
                Trans.SetParent(idleParent);
                LocalInit();
                ResetTags();
            }
        }

        /// <summary> 设置Transform组件初始默认参数 </summary>
        public void LocalInit()
        {
            if (Trans != null)
            {
                Trans.localScale = Vector3.one;
                Trans.localEulerAngles = Vector3.zero;
                Trans.localPosition = Vector3.zero;
            }
        }

        /// <summary> 设置Active </summary>
        public void SetActive(bool r_bool)
        {
            if (Exists())
            {
                Obj.SetActive(r_bool);
            }
        }

        /// <summary> 设置父节点 </summary>
        public void SetParent(Transform r_parent, bool worldPositionStays = false)
        {
            if (Exists())
            {
                Trans.SetParent(r_parent, worldPositionStays);
            }
        }

        protected override void ResetTags()
        {
            base.ResetTags();

            //
        }

        private Text txt;
        private Image image;
        private ExpandImage expandImage;

        public Text Text
        {
            get
            {
                if (txt == null)
                    txt = Obj.GetComponent<Text>();
                return txt;
            }
        }

        public Image Image
        {
            get
            {
                if (image == null)
                    image = Obj.GetComponent<Image>();
                return image;
            }
        }

        public ExpandImage ExpandImage
        {
            get
            {
                if (expandImage == null)
                    expandImage = Obj.GetComponent<ExpandImage>();
                return expandImage;
            }
        }
    }
}
