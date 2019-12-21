using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;

public class ShowModelUI : MonoBehaviour {
    [SerializeField] private ShowModelButton buttonPrefab;

    private void Start() {
        var models = FindObjectOfType<ShowModelController>().GetModels();
        var offset = 100f;
        for(var i = 0; i < models.Count; i++){
            CreateButtonForModel(models[i], offset * i);
        }
    }

    private void CreateButtonForModel(Transform model, float offset) {
        var button = Instantiate(buttonPrefab);
        button.transform.SetParent(this.transform);
        button.transform.position = new Vector2(120 + offset, 60);
        button.transform.localScale = Vector2.one;
        button.transform.localRotation = Quaternion.identity;

        button.Initialize(model, FindObjectOfType<ShowModelController>().EnableModel);
    }
}
