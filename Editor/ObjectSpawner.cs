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
    private Vector2 _objectUISize = new Vector2(100,100);
    private float _objectScale;
    private int _selected = 0;
    private string[] _typeOfObject = new string[]
    {
        "Normal", "UI", 
    };
    
    
    [MenuItem("GBP/ObjectSpawner")]
    public static void ShowWindow()
    {
        GetWindow(typeof(ObjectSpawner));
    }

    private void OnGUI()
    {
        GUILayout.Label("GBP Object Spawner", EditorStyles.boldLabel);
        EditorGUILayout.Space();
        _objectId = EditorGUILayout.IntField("Object Id", _objectId);
        _objectName = EditorGUILayout.TextField("Object Name", _objectName);
        _object = (GameObject)EditorGUILayout.ObjectField("GameObject", _object, typeof(GameObject), true);
        _objectParent = (GameObject)EditorGUILayout.ObjectField("Parent GameObject", _objectParent, typeof(GameObject), true);
        _selected = EditorGUILayout.Popup("Type of Object", _selected, _typeOfObject);
        EditorGUILayout.Space();
        if (EditorGUILayout.BeginFadeGroup(_selected))
        {
            _objectPosition = EditorGUILayout.Vector3Field("Object Position", _objectPosition);
            _objectRotation = EditorGUILayout.Vector3Field("Object Rotation", _objectRotation); 
            _objectUISize = EditorGUILayout.Vector2Field("Object Size", _objectUISize);
        }
        else
        {
            _objectPosition = EditorGUILayout.Vector3Field("Object Position", _objectPosition);
            _objectRotation = EditorGUILayout.Vector3Field("Object Rotation", _objectRotation);   
            _objectScale = EditorGUILayout.Slider("Object Scale", _objectScale, 0.5f, 10f);
            
        }

        EditorGUILayout.EndFadeGroup();
        if (GUILayout.Button("Spawn Object"))
        {
            SpawnObject();
        }

    }

    private void SpawnObject()
    {
        
        if (_object == null)
        {
            Debug.LogError("No valid GameObject is assigned");
            return;
        }
        
        if (_objectName == "")
        {
            Debug.LogError("Valid name of the GameObject is required");
            return;
        }
        
        var newObject = new GameObject();
        if (_selected == 0)
        {
            newObject = Instantiate(_object, _objectPosition, Quaternion.Euler(_objectRotation));
            newObject.transform.localScale = Vector3.one * _objectScale;
            newObject.name = _objectName + _objectId;
            _objectId++;
        }

        if (_objectParent == null && _selected == 1)
        {
            Debug.LogError("Please assign a valid parent to the UI Element");
            return;
        }
        else if (_selected == 1)
        {
            newObject = Instantiate(_object, _objectPosition, Quaternion.Euler(_objectRotation),_objectParent.transform);
            newObject.GetComponent<RectTransform>().anchoredPosition = _objectPosition;
            newObject.GetComponent<RectTransform>().sizeDelta = _objectUISize;
            newObject.name = _objectName + _objectId;
            _objectId++;
            Debug.Log("UI");
        }
        
        if (_objectParent != null && _selected == 0)
        {
            newObject.transform.SetParent(_objectParent.transform);
        }
    }
}