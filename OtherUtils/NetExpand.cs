using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
using System.Text;
using System.Security.Cryptography;

/// <summary>
/// C# 扩展静态类
/// </summary>
public static class NetExpand
{
    /* [0,1,2,3,4,5]
     * 要分队的情况如： 0和1，2和3，4和5
     * 算法：
     *      for (int i = 0; i < length; i++)
            {
                int temp = i % 2 == 0 ? i % 2 : i - 1 % 2;
            }
     */

    /// <summary>
    /// 
    /// </summary>
    /// <param name="r_Norm"></param>
    /// <returns></returns>
    public static string NumberHanzi(string r_Norm)
    {
        string str = string.Empty;

        foreach (char item in r_Norm)
        {
            int code = item;
            switch (code)
            {
                case 48: str += "零"; break;
                case 49: str += "一"; break;
                case 50: str += "二"; break;
                case 51: str += "三"; break;
                case 52: str += "四"; break;
                case 53: str += "五"; break;
                case 54: str += "六"; break;
                case 55: str += "七"; break;
                case 56: str += "八"; break;
                case 57: str += "九"; break;
                case 190: str += "点"; break;
                default: str += item; break;
            }
        }
        return str;
    }

    /// <summary>
    /// 只Parse 0-9的数字，忽略其他文字，包括忽略小数点后面的
    /// </summary>
    /// <param name="r_str"></param>
    /// <returns></returns>
    public static string ParseNumber(string r_str)
    {
        string str = string.Empty;
        char[] cc = r_str.ToCharArray();

        for (int i = 0; i < cc.Length; i++)
        {
            if (cc[i] == '.') break;
            else
            {
                int t = cc[i];
                if (t >= 48 && t <= 57)
                {
                    str += cc[i];
                }
            }
        }
        return str;
    }

    /// <summary>
    /// 判断数组字符串是否多为空或者空字符
    /// </summary>
    /// <param name="ary"></param>
    /// <returns></returns>
    public static bool IsNullOrEmpty(params string[] ary)
    {
        if (ary?.Length > 0)
        {
            for (int i = 0; i < ary.Length; i++)
            {
                if (!string.IsNullOrEmpty(ary[i])) return false;
            }
        }
        return true;
    }

    /// <summary>
    /// 判断这个数组的这个索引是否越界
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="ary"></param>
    /// <param name="index"></param>
    /// <returns></returns>
    public static bool IsOverflow<T>(this T[] ary, int index)
    {
        if (ary != null)
        {
            if (index < ary.Length && index >= 0) return false;
            else
            {
                Debug.LogError($"传入的数组索引越界：Length {ary.Length}，index {index} \n {Environment.StackTrace}");
            }
        }
        return true;
    }

    /// <summary>
    /// 判断这个数组的这个索引是否越界
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="ary"></param>
    /// <param name="index"></param>
    /// <returns></returns>
    public static bool IsOverflow<T>(this List<T> ary, int index)
    {
        if (ary != null)
        {
            if (index < ary.Count && index >= 0) return false;
            else
            {
                Debug.LogError($"传入的List索引越界：Count {ary.Count}，index {index} \n {Environment.StackTrace}");
            }
        }
        return true;
    }

    /// <summary>
    /// 查找列表是否有这个元素，没有就直接添加进去
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="item"></param>
    public static void InAddValue<T>(this List<T> list, T item)
    {
        if (list == null)
        {
            Debug.LogError(Environment.StackTrace);
            return;
        }
        if (list.Contains(item) == false) list.Add(item);
    }

    /// <summary>
    /// 查找队列是否有这个元素，没有就直接添加进去
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="queue"></param>
    /// <param name="item"></param>
    public static void InAddValue<T>(this Queue<T> queue, T item)
    {
        if (queue == null)
        {
            Debug.LogError(Environment.StackTrace);
            return;
        }

        if (queue.Contains(item) == false) queue.Enqueue(item);
    }

    /// <summary>
    /// 查询是否有该文件目录，否则创建目录
    /// </summary>
    /// <param name="path"></param>
    public static void ExistsOfDirectory(string path)
    {
        if (string.IsNullOrEmpty(path))
        {
            Debug.LogError(Environment.StackTrace);
            return;
        }
        if (!Directory.Exists(path)) Directory.CreateDirectory(path);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="path"></param>
    public static void ExistsOfFileText(string path)
    {
        if (string.IsNullOrEmpty(path))
        {
            Debug.LogError(Environment.StackTrace);
            return;
        }
        if (!File.Exists(path)) File.CreateText(path);
    }


    /// <summary>
    /// 内容加密
    /// </summary>
    /// <param name="content">要加密内容</param>
    /// <param name="strkey">key值</param>
    /// <returns></returns>
    public static string EncryptionContent(string content, string strkey)
    {
        byte[] keyArray = Encoding.UTF8.GetBytes(strkey);
        RijndaelManaged rijndael = new RijndaelManaged
        {
            Key = keyArray,
            Mode = CipherMode.ECB,
            Padding = PaddingMode.PKCS7
        };
        ICryptoTransform crypto = rijndael.CreateEncryptor();
        byte[] base64 = Encoding.UTF8.GetBytes(content);
        byte[] results = crypto.TransformFinalBlock(base64, 0, base64.Length);
        return Convert.ToBase64String(results, 0, results.Length);
    }

    /// <summary>
    /// 内容解密
    /// </summary>
    /// <param name="content">被加密内容</param>
    /// <param name="strKey">key值</param>
    /// <returns></returns>
    public static string DecipheringContent(string content, string strKey)
    {
        byte[] keyArray = Encoding.UTF8.GetBytes(strKey);
        RijndaelManaged rijndael = new RijndaelManaged
        {
            Key = keyArray,
            Mode = CipherMode.ECB,
            Padding = PaddingMode.PKCS7
        };
        ICryptoTransform crypto = rijndael.CreateDecryptor();
        byte[] base64 = Convert.FromBase64String(content);
        byte[] results = crypto.TransformFinalBlock(base64, 0, base64.Length);
        return Encoding.UTF8.GetString(results);
    }

}

