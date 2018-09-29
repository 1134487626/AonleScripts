using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{

    public class RandomShow : MonoBehaviour
    {

        [SerializeField]
        private List<GameObject> lstObjs = new List<GameObject>();

        private void Awake()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                lstObjs.Add(transform.GetChild(i).gameObject);
            }
        }
        private void OnEnable()
        {
            Random random = new Random();
            for (int i = 0; i < lstObjs.Count; i++)
            {
                lstObjs[i].gameObject.SetActive(Random.Range(0, 2) < 1);
            }
        }

    }

}
