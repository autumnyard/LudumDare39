using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Variables
    #endregion

    #region Monobehaviour
    void Awake()
    {
        Director.Instance.gameManager = this;
    }

    private void Start()
    {
        Director.Instance.EverythingBeginsHere();
    }
    #endregion
}
