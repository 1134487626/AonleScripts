using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Anole
{
    /// <summary>
    /// 挂载公用的Update单例类
    /// 监测某一些参数
    /// </summary>
    public partial class MountUpdates : Sington<MountUpdates>
    {
        void Update()
        {
            UpdateAllMount();
        }

        private void UpdateAllMount()
        {
            UpdateTimeLimitList();

        }

        #region IPoneNotch




        #endregion

        #region TimeLimit

        List<TimeLimit> timeList = new List<TimeLimit>();

        /// <summary>
        /// 每帧监测所有倒计时类
        /// </summary>
        private void UpdateTimeLimitList()
        {
            if (timeList?.Count > 0)
            {
                for (int i = 0; i < timeList.Count; i++)
                {
                    timeList[i]?.OnUpdate();
                }
            }
        }

        /// <summary>
        /// 添加某个倒计时类
        /// </summary>
        /// <param name="r_time"></param>
        /// <returns></returns>
        public void AddTimeLimit(TimeLimit r_item)
        {
            if (r_item != null) timeList.Add(r_item);
            else
            {
                DeBug.LogError("注意：传入TimeLimit类为空！");
            }
        }

        /// <summary>
        /// 添加某个倒计时类
        /// </summary>
        /// <param name="r_time"></param>
        /// <returns></returns>
        public void AddTimeLimit(params TimeLimit[] r_items)
        {
            if (r_items?.Length > 0)
            {
                for (int i = 0; i < r_items.Length; i++)
                {
                    AddTimeLimit(r_items[i]);
                }
            }
            else
            {
                DeBug.LogError("注意：传入TimeLimit类为空！");
            }
        }

        /// <summary>
        /// 移除某个倒计时类
        /// </summary>
        public void RomveTimeLimit(TimeLimit r_item)
        {
            if (r_item != null && timeList.Contains(r_item))
            {
                timeList.Remove(r_item);
            }
        }

        /// <summary>
        /// 移除所有倒计时类
        /// </summary>
        public void ClearTimeLimit()
        {
            if (timeList?.Count > 0)
            {
                timeList.Clear();
            }
        }

        #endregion
    }
}