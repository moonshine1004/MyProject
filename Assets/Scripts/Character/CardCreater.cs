#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System.IO;

public class CardCreater : MonoBehaviour
{
    //��ǲ �ƿ�ǲ ����
    public TextAsset csvFile;
    public string outputFolder = "Assets/Cards";
    //�ν����� â���� ������ �� �ֵ��� ��
    [ContextMenu("Import Cards From CSV")]

    public void ImportCards()
    {
        var cards = CardParser.ParseCSVToCards(csvFile);//ī���ļ� Ŭ������ �о�� csv������ ���ڿ� ����Ʈ�� �ٲ㼭 cards�� �ʱ�ȭ

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
