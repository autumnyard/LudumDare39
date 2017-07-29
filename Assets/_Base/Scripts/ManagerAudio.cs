using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerAudio : MonoBehaviour
{

	void Awake()
	{
		Director.Instance.managerAudio = this;
	}
}
