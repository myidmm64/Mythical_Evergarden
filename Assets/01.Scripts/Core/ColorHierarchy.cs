using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ColorHierarchy : MonoBehaviour
{
#if UNITY_EDITOR
    //��� ���̶�Ű �� �ִ� �� ��ũ��Ʈ�� �پ��ִ� ��� ������Ʈ�� ��ųʸ��� �����ϴ� ��
    private static Dictionary<Object, ColorHierarchy> coloredObjects = new Dictionary<Object, ColorHierarchy>();

    private void Reset()
    {
        OnValidate();
    }

    private void OnValidate()
    {
        if (coloredObjects.ContainsKey(this.gameObject) == false)
        {
            coloredObjects.Add(this.gameObject, this);
        }
    }

    static ColorHierarchy()
    {
        EditorApplication.hierarchyWindowItemOnGUI += HandleDraw;
    }

    private static void HandleDraw(int id, Rect selectionRect)
    {
        Object obj = EditorUtility.InstanceIDToObject(id);  //���� ���̵� ������ �ش� ������Ʈ�� �ҷ����� �Լ�

        if (obj != null && coloredObjects.ContainsKey(obj))
        {
            GameObject gObj = obj as GameObject;
            ColorHierarchy ch = gObj.GetComponent<ColorHierarchy>();
            if (ch != null)
            {
                PaintObject(obj, selectionRect, ch);
            }
            else
            {
                coloredObjects.Remove(obj);
            }
        }
    }

    private static void PaintObject(Object obj, Rect selectionRect, ColorHierarchy ch)
    {
        Rect bgRect = new Rect(selectionRect.x, selectionRect.y, selectionRect.width + 50, selectionRect.height);

        Color backColor = ch.backColor;
        Color fontColor = ch.fontColor;

        EditorGUI.DrawRect(bgRect, backColor);
        string name = $"{ch.prefix} {obj.name}";

        EditorGUI.LabelField(bgRect, name, new GUIStyle()
        {
            normal = new GUIStyleState() { textColor = fontColor },
            fontStyle = FontStyle.Bold
        });
    }

    public string prefix = "@";
    public Color backColor = Color.white;
    public Color fontColor = Color.black;
#endif
}