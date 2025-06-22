using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public static class CardParser
{
    //
    public static List<Card> ParseCSVToCards(TextAsset csvFile)
    {
        var result = new List<Card>();
        var rows = ParseCSV(csvFile.text);

        if (rows.Count < 4)
        {
            Debug.LogError("[CardParser] CSV �� ���� �ʹ� �����ϴ�.");
            return result;
        }

        string[] headers = rows[1];

        for (int i = 3; i < rows.Count; i++) // ������ ���� 3��°���� ����
        {
            string[] row = rows[i];

            if (row.Length == 0 || string.IsNullOrWhiteSpace(row[0]))
            {
                Debug.LogWarning($"[CardParser] �� �� ��ŵ: row {i}");
                continue;
            }

            Card card = ScriptableObject.CreateInstance<Card>();

            for (int j = 0; j < headers.Length && j < row.Length; j++)
            {
                string header = headers[j].Trim();
                string value = row[j].Trim();

                if (string.IsNullOrEmpty(value)) continue; // �� �� ��ŵ

                try
                {
                    switch (header)
                    {
                        case "CardID":
                            if (int.TryParse(value, out var id))
                                card.CardID = id;
                            break;
                        case "Cost":
                            if (int.TryParse(value, out var cost))
                                card._Cost = cost;
                            break;
                        case "Damage":
                            if (float.TryParse(value, out var dmg))
                                card._Damage = dmg;
                            break;
                        case "Element":
                            if (int.TryParse(value, out var elem))
                                card._element = (Enum.Element)elem;
                            break;
                        
                        case "TargetType":
                            if (int.TryParse(value, out var target))
                                card._TargetType = (Enum.TargetType)target;
                            break;
                        
                    }
                }
                catch (System.Exception ex)
                {
                    Debug.LogWarning($"[CardParser] '{header}' �� '{value}' ó�� ���� (row {i}): {ex.Message}");
                }
            }

            result.Add(card);
        }

        return result;
    }


    private static List<string[]> ParseCSV(string csvText)
    {
        List<string[]> result = new List<string[]>(); //���ڿ� ����Ʈ result����
        var lines = csvText.Split(new[] { "\r\n", "\n" }, System.StringSplitOptions.RemoveEmptyEntries);//�� ���ڿ��� �����ϰ� Ư�� ������("\r\n", "\n"-->�ٹٲ�) ������ �߶� ��(line)�� ����

        foreach (var line in lines)//�� ���� �ٽ� �ڸ� lines���� ��ȸ�Ͽ� ���� ���� line�� ����
        {
            result.Add(ParseCSVLine(line));//result ����Ʈ�� ParseCSVLine�޼��� �����Ͽ� ���� ���� ����
        }

        return result;
    }

    private static string[] ParseCSVLine(string line)
    {
        List<string> values = new List<string>(); //values��� ���ڿ� �迭 ����
        bool inQuotes = false;
        string current = "";

        for (int i = 0; i < line.Length; i++)
        {
            char c = line[i];

            if (c == '"')
            {
                inQuotes = !inQuotes;
            }
            else if (c == ',' && !inQuotes)
            {
                values.Add(current.Trim());
                current = "";
            }
            else
            {
                current += c;
            }
        }

        values.Add(current.Trim());
        return values.ToArray();
    }

    /* ���� ���: ���� ������ ���̺��� �Ľ��Ͽ� ��ũ���ͺ� ������Ʈ�� �������� ������ �� �ֵ��� �� --> �̴� ���� Ư�� �� �ݺ����� �뷱�̰� ���� ���� ������Ʈ�� �ٷ뿡 �־� ū ������ ����
     * ��� ���: ����Ƽ ��ũ���ͺ� ������Ʈ, using System.IO, AssetDatabase, [ContextMenu]
     *  1)����Ƽ ��ũ���ͺ� ������Ʈ: ����Ƽ�� ��ũ���ͺ� ������Ʈ�� ������ ���� �뷮�� ���� �ٸ� ������Ʈ�� �ٷ�µ��� ������ ����Ƽ�� ������ �����̳��̴�. 
     *                                �� ����� ����ϸ� ������ �����͸� ������ �������� ����� �����Ͽ� ����� �� �ִٴ� ������ �ִ�.
     *                                �� ������Ʈ�� �̸� �̿��Ͽ� ������Ʈ�� �ٽ� �ý����� 'ī�� �ý���'�� �ʿ��� ī�带 �����ϰ� ������ �����ߴ�.
     *                                ��ũ���ͺ� ������Ʈ ����� ��ȹ�ڿ� �����Ͽ� ������ ���̺��� ���� ����ϸ� ���� ȿ������ �������, �� ������Ʈ ���� ��ȹ�ڰ� ������ ī�� ������ ���̺��� ���� ī�� ��ũ���ͺ� ������Ʈ�� �����ߴ�.
     *  2)System.IO: System.IO�� .NET ���̺귯���� �����ϴ� ���� �����̽��� ����, ��Ʈ���� ������� ���´�. �� ������Ʈ������ System.IO�� Directory�� �̿��Ͽ� AssetDatabase�� ������ �ٷ����.
     *  3)AssetDatabase: 
     * ������Ȳ �� �ذ����
     *  1)�Ľ��� �� �����Ϳ� ���� ������ Ÿ�� ���ǿ� �־�� ������
     *   -->�Ľ��� �� �����͸� ������ �����ϴ� ������ �־� ��� ���� ���ڿ��� �����Ա� ������ ���ڿ��� �ƴ� �����͸� �޾Ƶ帮�� ���ϴ� ���� �߻�
     *   -->�̸� �����ϰ��� ����� ����ȯ�� �̿��Ͽ� �����͸� �޾ƿ����� ��ȿ���� ���� enum�� �޾ƿ��� ������ Ȯ����
     *   -->�̸� �ذ��ϱ� ���� int.Parse(), enum.Parse()�� Parse�� ����Ͽ����� Parse�� �������� �ʴ� enum��, ���� ���� ���� ���� ��Ȳ�� ���� ��ó�� �����
     *   -->���� TryParse()�� ����Ͽ� �̷��� ���� ��Ȳ�� ������
     * 
     * 
     * �����
     *  1)������ �־�� �ʿ伺 �� �߿伺: ���ó��� ������ �ϵ���� ������ ����� ��°� �Բ� ���� �� �������� ���� �ִ�. �̿� ���� ��ȹ�ڿ� ���α׷��Ӱ� �ٷ���ϴ� �������� �� ���� �þ�� �ִ�.
     *                                      �� ������Ʈ���� ����� ī��(������Ʈ)�� ������ �� 30���������� ī���� �̸��� �����ϰ� �̸� Ȱ���ϴµ��� �־� ���� ������� �޾���.
     *                                      ��, ������������ ��ȭ�� ���� ���� ���ӿ����� ������Ʈ�� �ٷ�� ���� �̺��� ���� ���������� ��� ������, �̷��� ���� ���� ���� ��������� ������ ���̺��� 
     *                                      ���� ���ۿ� �ʼ� ��Ұ� �Ǿ�����, ���α׷��ӿ��� �־�� �̸� �����ϴ� ��ȹ�ڿ��� ������ ���� �߿������ٰ� �� �� �ִ�.
     *                                      �� ������Ʈ�� �����Կ� �־ �̷��� ������ �ʿ伺�� ��������, ���α׷��ӿ� ��ȹ���� ����� �̷��� ������ �ٸ��� �Ǿ��ִ� ������ �߿伺 ���� �������� �ǰ��� �� �־���.
     *  
     * ������ �κ�: 
     * 
     * 
     * ���� ������ ��
     *  1)����� ��� ���� String���� �Ľ��� �� �� ����� ����ȯ�ϴ� ����� ����Ͽ�����, �̴� ����� �ڷḦ �ٷ� ���� ��� ������ �� �����͸� �������ϴ� �ڵ带 ���� �ۼ��ؾ� �Ǵ� ��ȿ������ �־� ������ �䱸��
     *      ���� ���Ŀ��� ������ ���̺��� ����� �ν��Ͽ� �����͸� �Ľ��� ������ �ش� �ڷ������� �����͸� �޾ƿ� �� �ֵ��� ������ ������
     * 
     * 
     * 
     * 
     * 
     */
}
