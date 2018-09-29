using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Anole
{
    /// <summary>
    /// 对象的临时标签
    /// </summary>
    public abstract class ObjectPoolTags
    {
        /// <summary>  </summary>
        public char tagChar;
        /// <summary>  </summary>
        public string tagString;
        /// <summary>  </summary>
        public int tagInt;
        /// <summary>  </summary>
        public uint tagUint;
        /// <summary>  </summary>
        public bool tagBool;
        /// <summary>  </summary>
        public float tagFloat;
        /// <summary>  </summary>
        public Vector2 tagVector2;
        /// <summary>  </summary>
        public Vector3 tagVector3;
        /// <summary>  </summary>
        public Vector4 tagVector4;

        /// <summary>
        /// 重置标签数据
        /// </summary>
        protected virtual void ResetTags()
        {
            tagInt = 0;
            tagUint = 0;
            tagFloat = 0f;
            tagBool = false;
            tagString = string.Empty;
            tagVector2 = Vector2.zero;
            tagVector3 = Vector3.zero;
            tagVector4 = Vector4.zero;
        }
    }
}
