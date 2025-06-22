#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System.IO;

public class CardCreater : MonoBehaviour
{
    //인풋 아웃풋 설정
    public TextAsset csvFile;
    public string outputFolder = "Assets/Cards";
    //인스펙터 창에서 실행할 수 있도록 함
    [ContextMenu("Import Cards From CSV")]

    public void ImportCards()
    {
        var cards = CardParser.ParseCSVToCards(csvFile);//카드파서 클래스로 읽어온 csv파일을 문자열 리스트로 바꿔서 cards에 초기화

        if (!AssetDatabase.IsValidFolder(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        foreach (var card in cards)
        {
            AssetDatabase.CreateAsset(card, $"{outputFolder}/Card_{card.CardID}.asset");
        }

        AssetDatabase.SaveAssets();
    }
}
#endif
