using System.Collections.Generic;
using UnityEngine;

namespace Anole
{
    /// <summary>
    ///  Resources对象池
    /// </summary>
    public sealed class ObjectPool : Sington<ObjectPool>
    {
        //保存Resources同一路径下的所有实例化对象等等数据
        Dictionary<string, ObjectPoolResources> m_AllPoolItem;

        //某一类对象的父对象，方便开发界面查看
        List<GameObject> m_ItemsParent;

        private void Awake()
        {
            gameObject.SetActive(false);
            m_AllPoolItem = new Dictionary<string, ObjectPoolResources>();
            m_ItemsParent = new List<GameObject>();
        }

        /// <summary>
        /// 克隆Resources路径下的目标
        /// </summary>
        /// <param name="r_path"></param>
        /// <returns></returns>
        public ObjectPoolItem Clone(string r_path)
        {
            if (InPathExists(r_path))
            {
                var tPool = m_AllPoolItem[r_path];
                var item = tPool.FindIdle;

                if (item == null)
                {
                    var game = Instantiate(tPool.Obj);

                    if (game != null)
                    {
                        game.name = tPool.Name;
                        item = new ObjectPoolItem(game, tPool.Parent, r_path);
                        tPool.AddPoolItem(item);
                    }
                    else
                    {
                        DeBug.LogError("注意：要实例化对象为空！");
                    }
                }
                item.Idle = false;
                return item;
            }

            DeBug.LogError("注意：调用对象池的路径出错！");
            return null;
        }

        /// <summary>
        /// 将某个对象池的对象设置为休闲状态
        /// </summary>
        /// <param name="r_item"></param>
        public void Idle(ObjectPoolItem r_item)
        {
            if (r_item != null)
            {
                r_item.Idle = true;
                r_item.SetTheIdle();
            }
        }

        /// <summary>
        /// 将某个对象池的对象设置为休闲状态
        /// </summary>
        /// <param name="r_item"></param>
        public void Idle(params ObjectPoolItem[] r_items)
        {
            if (r_items?.Length > 0)
            {
                for (int i = 0; i < r_items.Length; i++)
                {
                    Idle(r_items[i]);
                }
            }
        }

        /// <summary>
        /// 将某个相同路径下的所有对象设置为休闲状态
        /// </summary>
        /// <param name="r_item"></param>
        public void IdleKeys(string r_path)
        {
            if (string.IsNullOrEmpty(r_path)) return;

            if (m_AllPoolItem != null)
            {
                if (m_AllPoolItem.ContainsKey(r_path))
                {
                    var temp = m_AllPoolItem[r_path];
                    if (temp != null)
                    {
                        temp.IdleAllItem();
                    }
                }
            }
        }

        /// <summary>
        /// 查询是否有该对象的基础类,若没有则添加
        /// </summary>
        /// <param name="r_path"></param>
        /// <returns></returns>
        private bool InPathExists(string r_path)
        {
            if (string.IsNullOrEmpty(r_path))
            {
                Debug.LogError("注意：传入Resources路径为空！");
                return false;
            }

            if (m_AllPoolItem.ContainsKey(r_path)) return true;

            var game = Resources.Load<GameObject>(r_path);

            if (game == null)
            {
                DeBug.LogError($"注意：Resources.Load 加载出错！{r_path}");
                return false;
            }
            else
            {
                string name = "All" + game.name;
                var parent = new GameObject(name);
                parent.transform.parent = transform;
                parent.SetActive(false);
                m_ItemsParent.Add(parent);
                m_AllPoolItem.Add(r_path, new ObjectPoolResources(game, parent.transform));
                return true;
            }
        }

        /// <summary>
        /// 销毁某个对象池中的对象
        /// </summary>
        /// <param name="r_item"></param>
        public void DestroyItem(ObjectPoolItem r_item, bool r_Immediate = false)
        {
            if (r_item != null && m_AllPoolItem != null)
            {
                string path = r_item.ResourcesPath;

                if (m_AllPoolItem.ContainsKey(path))
                {
                    m_AllPoolItem[path].DestroyItem(r_item, r_Immediate);
                }
            }
        }

        /// <summary>
        /// 销毁同一路径下的所有实例化对象
        /// 注意时间段以及是否要先查询有无使用的对象
        /// </summary>
        /// <param name="r_path"></param>
        public void DestroyItems(string r_path, bool r_Immediate = false)
        {
            if (m_AllPoolItem != null)
            {
                if (m_AllPoolItem.ContainsKey(r_path))
                {
                    IdleKeys(r_path);
                    var pool = m_AllPoolItem[r_path];

                    if (pool != null)
                    {
                        //pool.DestroyAll(r_Immediate);
                        m_AllPoolItem.Remove(r_path);
                        var game = pool.Parent.gameObject;//并删除默认的父级对象

                        if (game != null)
                        {
                            if (m_ItemsParent.Contains(game)) m_ItemsParent.Remove(game);

                            if (r_Immediate)
                            {
                                DestroyImmediate(game);
                            }
                            else
                            {
                                Destroy(game);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 销毁所有对象池中的对象
        /// </summary>
        public void DestroyAllItem(bool r_Immediate = false)
        {
            if (m_AllPoolItem != null)
            {
                foreach (var item in m_AllPoolItem.Values)
                {
                    item?.IdleAllItem();
                }
                m_AllPoolItem.Clear();
            }

            if (m_ItemsParent != null)
            {
                for (int i = m_ItemsParent.Count - 1; i >= 0; i--)
                {
                    var game = m_ItemsParent[i];
                    if (game != null)
                    {
                        if (r_Immediate)
                            DestroyImmediate(game);
                        else
                            Destroy(game);
                    }
                }
                m_ItemsParent.Clear();
            }
        }

    }
}
