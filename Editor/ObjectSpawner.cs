using UnityEditor;
using UnityEngine;

public class ObjectSpawner : EditorWindow
{
    private string _objectName = "";
    private int _objectId = 1;
    private GameObject _object;
    private GameObject _objectParent;
    private Vector3 _objectPosition;
    private Vector3 _objectRotation;
    private float _objectScale;

    [MenuItem("GBP/ObjectSpawner")]
    public static void ShowWindow()
    {
        GetWindow(typeof(ObjectSpawner));
    }

    private void OnGUI()
    {
        GUILayout.Label("GBP Object Spawner", EditorStyles.boldLabel);
        _objectId = EditorGUILayout.IntField("Object Id", _objectId);
        _objectName = EditorGUILayout.TextField("Object Name", _objectName);
        _objectScale = EditorGUILayout.Slider("Object Scale", _objectScale, 0.5f, 10f);
        _objectPosition = EditorGUILayout.Vector3Field("Object Position", _objectPosition);
        _objectRotation = EditorGUILayout.Vector3Field("Object Rotation", _objectRotation);
        _object = (GameObject)EditorGUILayout.ObjectField("GameObject", _object, typeof(GameObject), true);
        _objectParent = (GameObject)EditorGUILayout.ObjectField("Parent GameObject", _objectParent, typeof(GameObject), true);
        if (GUILayout.Button("Spawn Object"))
        {
            SpawnObject();
        }
    }

    private void SpawnObject()
    {
        if (_object == null)
        {
            Debug.LogError("No valid gameObject is assigned");
            return;
        }
        if (_objectName == null)
        {
            Debug.LogError("Valid Name or Id of the gameObject is mandatory");
            return;
        }

        var newObject = Instantiate(_object, _objectPosition, Quaternion.Euler(_objectRotation));
        newObject.name = _objectName + _objectId;
        newObject.transform.localScale = Vector3.one * _objectScale;
        _objectId++;
        if (_objectParent != null)
        {
            newObject.transform.SetParent(_objectParent.transform);
        }
    }
}