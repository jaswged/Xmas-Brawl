using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowModelButton : MonoBehaviour {
    private Transform _objectToShow;
    private Action<Transform> _clickAction;

    public void Initialize(Transform objectToShow, Action<Transform> clickAction) {
        this._objectToShow = objectToShow;
        this._clickAction = clickAction;
        GetComponentInChildren<Text>().text = _objectToShow.gameObject.name;
    }
    
    private void Start(){
        GetComponent<Button>().onClick.AddListener(HandleButtonClick);
    }

    private void HandleButtonClick() {
        _clickAction(_objectToShow);
    }
}
