using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Description
{
    public class DescriptionMain : MonoBehaviour
    {
        private void Awake()
        {
            Debug.Log(SceneData.sceneNames.ToString());
        }
    }
}