using Anole;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections.Generic;
using System;
using System.IO;

/// <summary>
/// Unity扩展类
/// </summary>
public static class UnityExpand
{
    public readonly static Color alpha1 = new Color(1, 1, 1, 0);
    public readonly static Color32 alpha32 = new Color32(255, 255, 255, 0);

//    public static T GetTextPrefs<T>() where T : class
//    {
//        T temp = default(T);
//        StreamReader reader = null;
//        try
//        {
//            string txtPath = SetPrefsTxtPath(typeof(T));
//            if (File.Exists(txtPath))
//            {
//                reader = new StreamReader(txtPath);
//                string t = reader.ReadToEnd();
//                DeBug.LogOrange($"获取文本流信息：{t}");
//                temp = JsonUtility.FromJson<T>(t);
//            }
//        }
//        catch (Exception e)
//        {
//            Debug.LogError(e.StackTrace);
//        }
//        finally
//        {
//            if (reader != null) reader.Close();
//        }
//        return temp;
//    }

//    public static void SetTextPrefs<T>(T t) where T : class
//    {
//        StreamWriter writer = null;
//        try
//        {
//            string txt = JsonUtility.ToJson(t);
//            if (!string.IsNullOrEmpty(txt))
//            {
//                string txtPath = SetPrefsTxtPath(typeof(T));
//                if (File.Exists(txtPath))
//                {
//                    writer = new StreamWriter(txtPath, false);
//                }
//                else
//                {
//                    writer = File.CreateText(txtPath);
//                }
//                writer.Write(txt);
//                DeBug.LogGreen("保存文本流成功：" + txt);
//            }
//        }
//        catch (Exception e)
//        {
//            Debug.LogError("保存文本流失败：" + e.StackTrace);
//        }
//        finally
//        {
//            if (writer != null) writer.Close();
//        }
//    }

//    private static string SetPrefsTxtPath(Type type)
//    {
//#if UNITY_EDITOR
//        string prefix = $"{nameof(UnityExpand)}/{type.Namespace}";
//        NetExpand.ExistsOfDirectory(prefix);
//        return $"{prefix}/{type.Name}.txt";
//#elif UNITY_ANDROID || UNITY_IPHONE
//        string t = type.FullName.Replace(".", "_");
//        DeBug.Log(t);
//        return $"{Application.persistentDataPath}/{t}.txt";
//#endif

//    }


    /// <summary>
    /// 获取持久化本地数据类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T GetPrefs<T>() where T : class
    {
        T temp = default(T);
        string key = typeof(T).GetType().FullName;

        if (PlayerPrefs.HasKey(key))
        {
            try
            {
                string value = PlayerPrefs.GetString(key);
                DeBug.LogGolden(value);
                temp = JsonUtility.FromJson<T>(value);
            }
            catch (Exception e)
            {
                DeBug.LogError(e.ToString());
            }
        }
        return temp;
    }

    public static bool HasPrefs<T>() where T : class
    {
        string key = typeof(T).GetType().FullName;
        return PlayerPrefs.HasKey(key);
    }

    /// <summary>
    /// 保存持久化本地数据类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static bool SetPrefs<T>(T prefs) where T : class
    {
        try
        {
            string key = typeof(T).GetType().FullName;
            string value = JsonUtility.ToJson(prefs);
            PlayerPrefs.SetString(key, value);
            //PlayerPrefs.Save();
            return true;
        }
        catch (Exception e)
        {
            DeBug.LogError(e.ToString());
        }
        return false;
    }

    public static void SetActive2(this Component r_Component, bool r_bool)
    {
        if (r_Component != null)
        {
            r_Component.gameObject?.SetActive(r_bool);
        }
    }

    public static bool activeSelf2(this Component r_Component)
    {
        if (r_Component != null)
        {
            return r_Component.gameObject.activeSelf;
        }
        return false;
    }

    /// <summary>
    /// 查找某个子对象的Buttom，并添加点击事件
    /// </summary>
    /// <param name="r_form"></param>
    /// <param name="r_path"></param>
    /// <param name="action"></param>
    /// <returns></returns>
    public static Button FindOnButton(this Transform r_form, string r_path, UnityAction action)
    {
        Button button = null;
        if (r_form == null) DeBug.LogError($">>>>>>>>>>> 注意：传入 Transform 为空");
        else
        {
            button = r_form.Find(r_path).GetComponent<Button>();

            if (button == null) Debug.LogError($">>>>>>>>>>> 注意：查找的子对象没有Buttom组件");
            else
            {
                button.onClick.AddListener(action);
            }
        }
        return button;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="r_form"></param>
    /// <param name="r_path"></param>
    /// <param name="action"></param>
    /// <returns></returns>
    public static Transform FindOnButton2(this Transform r_form, string r_path, UnityAction action)
    {
        Button button = r_form.FindOnButton(r_path, action);
        if (button == null) return null;
        else
        {
            return button.transform;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="r_form"></param>
    /// <param name="r_path"></param>
    /// <param name="action"></param>
    public static ExpandButton FindUserButton(this Transform r_form, string r_path, UnityAction action)
    {
        ExpandButton button = null;
        if (r_form == null) DeBug.LogError($">>>>>>>>>>> 注意：传入 Transform 为空");
        else
        {
            button = r_form.Find(r_path).GetComponent<ExpandButton>();

            if (button == null) Debug.LogError($">>>>>>>>>>> 注意：查找的子对象没有 UserButton 组件");
            else
            {
                button?.onClick.AddListener(action);
            }
        }
        return button;
    }

    /// <summary> 将RectTransform设置成 UIPanel</summary>
    public static void RectSetPanel(this RectTransform rect)
    {
        if (rect != null)
        {
            rect.localPosition = Vector3.zero;
            rect.localScale = Vector3.one;
            rect.localEulerAngles = Vector3.zero;
            rect.anchorMax = Vector2.one;
            rect.anchorMin = Vector2.zero;
            rect.offsetMax = Vector2.zero;
            rect.offsetMin = Vector2.zero;
        }
    }

    public static float GetClipLength(this Animator anime, string clipName)
    {
        if (anime != null && !string.IsNullOrEmpty(clipName) && anime.runtimeAnimatorController != null)
        {
            RuntimeAnimatorController ac = anime.runtimeAnimatorController;
            AnimationClip[] _Clips = ac.animationClips;
            if (_Clips?.Length > 0)
            {
                for (int i = 0; i < _Clips.Length; i++)
                {
                    AnimationClip clip = ac.animationClips[i];
                    if (clip != null && clip.name == clipName) return clip.length;
                }
            }
        }

        return 0F;
    }

    public static void Kill2(this Tween tween)
    {
        tween.fullPosition = 0;
        tween.Kill(true);
    }

    /// <summary>
    /// 清除当前正在运行的Tween动画
    /// </summary>
    /// <param name="isComleat">清除前是否让其瞬间过度完动画</param>
    public static void KillPlayingTweens(bool isComleat)
    {
        List<Tween> list = DOTween.PlayingTweens();

        if (list?.Count > 0)
        {
            for (int i = list.Count - 1; i >= 0; i--)
            {
                if (isComleat)
                {
                    list[i].fullPosition = 0f;
                }
                list[i].Kill(isComleat);
            }
        }
    }

    /// <summary>
    /// 过滤清除当前正在运行的Tween动画
    ///【默认为布尔值，请注意设置Tween.id 为 true】
    /// </summary>
    /// <param name="isComleat">清除前是否让其瞬间过度完动画</param>
    public static void KillPlayingFilterTweens(bool isComleat)
    {
        List<Tween> list = DOTween.PlayingTweens();

        if (list?.Count > 0)
        {
            for (int i = list.Count - 1; i >= 0; i--)
            {
                if (list[i] != null)
                {
                    if (list[i].id is Boolean && (Boolean)list[i].id) break;

                    if (isComleat)
                    {
                        list[i].fullPosition = 0f;
                    }
                    list[i].Kill(isComleat);
                }
            }
        }
    }

    /// <summary>
    /// 三次方公式
    /// </summary>
    /// <param name="P0">起点</param>
    /// <param name="P1"></param>
    /// <param name="P2"></param>
    /// <param name="P3">结束点</param>
    /// <param name="t">贝塞尔线的宽度</param>
    /// <returns></returns>
    public static Vector3 BezierCurve(Vector3 P0, Vector3 P1, Vector3 P2, Vector3 P3, float t)
    {
        Vector3 B = Vector3.zero;
        float t1 = (1 - t) * (1 - t) * (1 - t);
        float t2 = (1 - t) * (1 - t) * t;
        float t3 = t * t * (1 - t);
        float t4 = t * t * t;
        B = P0 * t1 + 3 * t2 * P1 + 3 * t3 * P2 + P3 * t4;
        //B.y = P0.y*t1 + 3*t2*P1.y + 3*t3*P2.y + P3.y*t4;
        //B.z = P0.z*t1 + 3*t2*P1.z + 3*t3*P2.z + P3.z*t4;
        return B;
    }

    /// <summary>
    /// 三次方公式
    /// </summary>
    /// <param name="P0">起点</param>
    /// <param name="P1"></param>
    /// <param name="P2"></param>
    /// <param name="P3">结束点</param>
    /// <param name="t">贝塞尔线的宽度</param>
    /// <returns></returns>
    public static Vector2 BezierCurve(Vector2 P0, Vector2 P1, Vector2 P2, Vector2 P3, float t)
    {
        Vector2 B = Vector2.zero;
        float t1 = (1 - t) * (1 - t) * (1 - t);
        float t2 = (1 - t) * (1 - t) * t;
        float t3 = t * t * (1 - t);
        float t4 = t * t * t;
        B = P0 * t1 + 3 * t2 * P1 + 3 * t3 * P2 + P3 * t4;
        //B.y = P0.y*t1 + 3*t2*P1.y + 3*t3*P2.y + P3.y*t4;
        //B.z = P0.z*t1 + 3*t2*P1.z + 3*t3*P2.z + P3.z*t4;
        return B;
    }

}
