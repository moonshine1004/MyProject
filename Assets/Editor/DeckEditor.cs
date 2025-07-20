// DeckEditor.cs
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Deck))]
public class DeckEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Deck deck = (Deck)target;

        SerializedProperty listProperty = serializedObject.FindProperty("cardDeck");
        EditorGUILayout.PropertyField(listProperty, new GUIContent("Card Deck"), false);

        if (listProperty.isExpanded)
        {
            EditorGUI.indentLevel++;
            for (int i = 0; i < listProperty.arraySize; i++)
            {
                var element = listProperty.GetArrayElementAtIndex(i);
                var card = element.objectReferenceValue as CardData;

                if (card != null)
                {
                    EditorGUILayout.BeginVertical("box");
                    EditorGUILayout.ObjectField("Card", card, typeof(CardData), false);
                    EditorGUILayout.LabelField("Damage", card._Damage.ToString());
                    //EditorGUILayout.LabelField("Cost", card._Cost.ToString());
                    //EditorGUILayout.LabelField("Element", card._element.ToString());
                    EditorGUILayout.EndVertical();
                }
                else
                {
                    EditorGUILayout.PropertyField(element);
                }
            }
            EditorGUI.indentLevel--;
        }

        serializedObject.ApplyModifiedProperties();
    }
}
