using System.Collections.Generic;
using UnityEngine;

namespace Anole
{
    /// <summary>
    /// 克隆每一个对象池对象的基础【缓存】
    /// </summary>
    public sealed class ObjectPoolResources
    {
        //保存Resources同一路径下的所有实例化对象
        List<ObjectPoolItem> m_AllItem = new List<ObjectPoolItem>();

        private ObjectPoolResources() { }

        public Transform Parent { get; private set; }

        public GameObject Obj { get; private set; }

        public string Name { get; private set; }

        public ObjectPoolResources(GameObject r_obj, Transform r_parent)
        {
            if (r_obj == null) DeBug.LogError(">>>>>>  传入的对象为空!");
            else
            {
                Obj = r_obj;
                Name = r_obj.name;
                Parent = r_parent;
            }
        }

        /// <summary>
        /// 查找有无空闲的对象
        /// </summary>
        public ObjectPoolItem FindIdle
        {
            get
            {
                for (int i = m_AllItem.Count - 1; i >= 0; i--)
                {
                    var item = m_AllItem[i];
                    if (item != null && item.Idle)
                    {
                        if (item.Exists()) return item;
                        else
                        {
                            m_AllItem.RemoveAt(i);
                        }
                    }
                }
                return null;
            }
        }

        /// <summary>
        /// 保存每次实例化的对象
        /// </summary>
        /// <param name="item"></param>
        public void AddPoolItem(ObjectPoolItem item)
        {
            if (item != null && item.Exists() && !m_AllItem.Contains(item))
            {
                m_AllItem.Add(item);
            }
        }

        /// <summary>
        /// 将统一路径下的所有实例化的对象统一设置为休闲
        /// 自行考虑有任一有在使用过程的对象
        /// 是否一起设置为空闲状态
        /// </summary>
        public void IdleAllItem()
        {
            if (m_AllItem?.Count > 0)
            {
                for (int i = m_AllItem.Count - 1; i >= 0; i--)
                {
                    var t = m_AllItem[i];
                    if (t != null)
                    {
                        if (!t.Exists())
                        {
                            m_AllItem.RemoveAt(i);
                            continue;
                        }
                        t.Idle = true;
                        t.SetTheIdle();
                    }
                }
            }
        }

        /// <summary> 查找某个资源集合中是否有任意一个正在使用中 </summary>
        public bool FindSomeNotIdle
        {
            get
            {
                if (m_AllItem != null)
                {
                    for (int i = m_AllItem.Count - 1; i >= 0; i--)
                    {
                        var tPool = m_AllItem[i];
                        if (tPool != null && !tPool.Idle)
                        {
                            if (tPool.Exists()) return true;
                            else
                            {
                                m_AllItem.RemoveAt(i);
                            }
                        }
                    }
                }
                return false;
            }
        }

        /// <summary> 销毁某一个对象 </summary>
        public void DestroyItem(ObjectPoolItem item, bool immediate)
        {
            if (m_AllItem?.Count > 0)
            {
                if (m_AllItem.Contains(item))
                {
                    if (item.Exists())
                    {
                        var game = item.Obj;

                        if (immediate)
                        {
                            Object.DestroyImmediate(game);
                        }
                        else
                        {
                            Object.Destroy(game);
                        }
                    }
                    m_AllItem.Remove(item);
                }
            }
        }

        /// <summary> 销毁所有保存的对象 </summary>
        public void DestroyAll(bool immediate)
        {
            for (int i = m_AllItem.Count - 1; i >= 0; i--)
            {
                var tPool = m_AllItem[i];
                if (tPool != null && tPool.Exists())
                {
                    var game = tPool.Obj;
                    if (immediate)
                    {
                        Object.DestroyImmediate(game);
                    }
                    else
                    {
                        Object.Destroy(game);
                    }
                }
                m_AllItem.RemoveAt(i);
            }
            m_AllItem.Clear();
        }
    }
}
