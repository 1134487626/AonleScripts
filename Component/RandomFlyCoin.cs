using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Game
{
    public class RandomFlyCoin : MonoBehaviour
    {

        [Header("延迟时间")]
        public float delayTime = 0;
        [Header("飞行时间")]
        public float durationTime = 1;
        [Header("目标位置")]
        public Transform targetPos;

        [Header("是否随机")]
        public bool random=true;
        private Vector3 startPos = Vector3.zero;
        private void Awake()
        {
            startPos = transform.position;
        }

        private void OnEnable()
        {
            Image img = transform.GetComponent<Image>();
            transform.position = startPos;
            float randowm = 0;
            float randowm1 = 0;
            if (random==true)
            {
                 randowm = Random.Range(-5, 5);
                 randowm1 = Random.Range(-5, 5);
            }
            if (img!=null)
            {
                img.color = Color.white;
                img.DOFade(0, 0.2f).SetDelay(delayTime + durationTime - 0.05f);
                transform.DOMove(targetPos.position + new Vector3(randowm, randowm1, 0), durationTime).SetDelay(delayTime);
            }
            else
            {
                transform.gameObject.SetActive(true);

                transform.DOMove(targetPos.position + new Vector3(randowm, randowm1, 0), durationTime).SetDelay(delayTime);

            }

        }

    }

}