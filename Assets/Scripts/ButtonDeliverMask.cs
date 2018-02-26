using jamtasticvol3;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonDeliverMask : MonoBehaviour
{
    Button _btn;

    private void Start()
    {
        _btn = GetComponent<Button>();
        _btn.onClick.AddListener(() => GameManager.Instance.DeliverMask());

        GameManager.Instance.OnNewMask += () => _btn.interactable = false;
        GameManager.Instance.OnItemAdded += () => _btn.interactable = true;
        GameManager.Instance.OnItemRemoved += () =>
        {
            if(GameManager.Instance.GetMaskFeatures().Count < 1)
                _btn.interactable = false;
        };
    }


}