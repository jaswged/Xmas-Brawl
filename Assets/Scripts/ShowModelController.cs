using System.Collections.Generic;
using UnityEngine;

public class ShowModelController : MonoBehaviour {
    private List<Transform> models;
    void Awake(){
        models = new List<Transform>();
        for (int i = 0; i < transform.childCount; i++) {
            var model = transform.GetChild(i);
            models.Add(model);
            
            model.gameObject.SetActive(i == 0);
        }
    }

    public void EnableModel(Transform modelTransform) {
        for (int i = 0; i < transform.childCount; i++) {
            var trasformToToggle = transform.GetChild(i);
            bool shouldBeActive = trasformToToggle == modelTransform;

            trasformToToggle.gameObject.SetActive(shouldBeActive);
        }
    }

    public void PlayGame() {
        Debug.Log("Play Game clicked");
        
        // Random model for player 2 if single player
//        player2Char = !is2Player ? models[Random.Range(0, 3)].gameObject : models[Random.Range(0, 3)].gameObject;
//        player1Char = controller.GetActiveGameObject();
//        
//        var controller = FindObjectOfType<ShowModelController>();
        var charInd = 0;
        for (var i = 0; i < models.Count; i++) {
            if (models[i].gameObject.activeInHierarchy) {
                charInd = i;
                break;
            }
        }
        GameManagement.Instance.BeginGame(charInd, Random.Range(0, 3));
    }

    public GameObject GetActiveGameObject() {
        GameObject go = null;
        foreach (var model in models) {
            if (!model.gameObject.activeInHierarchy) continue;
            go = model.gameObject;
            break;
        }

        return go;
    }

    public List<Transform> GetModels() {
        return new List<Transform>(models);
    }
}
