using UnityEngine;
using UnityEditor;
//����Ʈ�� ����ϱ� ���� ���ӽ����̽�
using System.Collections.Generic;

//���� Ŭ����-->�ν��Ͻ� ���� ȣ�� ����
public static class CardParser
{
    //CSV������ �޾� ī�� Ÿ�� ��ü�� ����Ʈ�� ��ȯ�ϴ� ��ũ��Ʈ
    public static List<Card> ParseCSVToCards(TextAsset csvFile)
    {
        var result = new List<Card>(); //ī�� ����Ʈ ����
        var rows = ParseCSV(csvFile.text); //CSV������ �ؽ�Ʈ�� ParseCSV�޼��忡 �־� ���ڿ� �迭�� ����Ʈ�� ����� ����-->�ε����� �ٲ� ������ ���� �ٲ�
        //�����Ͱ� ���� �ǹ� ���� ��� ����
        if (rows.Count < 4)
        {
            Debug.LogError("[CardParser] CSV �� ���� �ʹ� �����ϴ�.");
            return result;
        }

        string[] headers = rows[1]; //���ڿ� �迭 ����Ʈ�� 2��° ���(�������� 2��° ��)�� ���ڿ� �迭�� ����
        //�ٺ��� �Ľ�
        for (int i = 3; i < rows.Count; i++) //������ ���� 4��°���� �����ؼ� ������ �����
        {
            string[] row = rows[i]; //i��° ��(���� ��)�� �����͸� ������
            //���� ���� ���̰� 0�̰ų� ���� 0��° ��Ұ� null�̸� ���� ��ŵ��(�Ѿ)
            if (row.Length == 0 || string.IsNullOrWhiteSpace(row[0]))
            {
                Debug.LogWarning($"[CardParser] �� �� ��ŵ: row {i}");
                continue;
            }
            //ī�� ��ũ���ͺ� ������Ʈ(�ν��Ͻ�)�� �޸𸮿� ����
            Card card = ScriptableObject.CreateInstance<Card>();
            //�� ���� �Ľ��ϴ� ���� ����
            //�̰� �� &&?
            for (int j = 0; j < headers.Length && j < row.Length; j++)
            {
                string header = headers[j].Trim(); //.Trim(): �յ� ���� ���� �� ��� ������ ����, ù��° ���� �ۿ� �ִ� headers�迭�� ���������μ� ���� �������� �� �̵� ���� ���� �̵�
                string value = row[j].Trim(); //ù ���� ���� rows�迭�� ���� �޾� ������ �ݺ� �� ������ ���� �������� ���� ������ �̵��Ͽ� �� ���� j��° ���� ���� ���� ���� �� �� ������ ����

                if (string.IsNullOrEmpty(value)) continue; // �� �� ��ŵ

                try //���ܸ� Ȯ��
                {
                    switch (header) //���ڿ� ������ ����� ���� ���� ����ġ��
                    {
                        case "CardID":
                            if (int.TryParse(value, out var id)) //value���ڿ��� int�� ��ȯ �õ� ���� �� id�� ���Ե�, ���� �� false��ȯ
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
                catch (System.Exception ex) //try�� ���ܸ� Ȯ���ϸ� ����
                {
                    Debug.LogWarning($"[CardParser] '{header}' �� '{value}' ó�� ���� (row {i}): {ex.Message}");
                }
            }

            result.Add(card); //ī�� ����Ʈ�� �߰�
        }

        return result; //ī�� ����Ʈ ��ȯ
    }

    //CSV������ �޾ƿ� ���ڿ� �迭 ����Ʈ�� ��ȯ�ϴ� ��ũ��Ʈ
    //�����ڸ� �������� �ؽ�Ʈ�� �߶� ���ڿ� �迭 ����Ʈ�� ����
    private static List<string[]> ParseCSV(string csvText)
    {
        List<string[]> result = new List<string[]>(); //���ڿ� �迭 ����Ʈ result����
        //csvText.Split: ���ڿ��� Ư�� �����ڷ� ������ ���ڿ��� �и��ϴ� C# ǥ�� ���̺귯���� ���Ե� �޼���
        var lines = csvText.Split(new[] { "\r\n", "\n" }, System.StringSplitOptions.RemoveEmptyEntries); //�� ���ڿ��� �����ϰ� Ư�� ������("\r\n", "\n"-->�ٹٲ�) ������ �߶� ��(line)�� ����

        foreach (var line in lines)//������ ���� lines���� ��Ҹ� �ϳ��� ������ ParseCSVLine�޼��带 ���� ���ڿ� �迭�� �ٲ� (i.e.���ڿ��� �߶� ���ڿ� �迭�� �ٲ�)
        {
            result.Add(ParseCSVLine(line));//result ����Ʈ�� line�� �ŰԺ����� �޴� ParseCSVLine�޼��� �����Ͽ� ���� ���� ����
        }

        return result;
    }
    //���ڿ��� �޾ƿ� ���ڿ� �迭�� ��ȯ�ϴ� �޼���
    //ParseCSV���� �����ڸ� �������� �ڸ� �� �ܾ 
    private static string[] ParseCSVLine(string line)
    {
        List<string> values = new List<string>(); //values��� ���ڿ� ����Ʈ ����
        bool inQuotes = false;
        string current = ""; //���ڸ� ��Ƶ� ���ڿ� 

        for (int i = 0; i < line.Length; i++) //���ڿ��� ���� ��ŭ �ݺ�
        {
            char c = line[i]; //���ڿ��� i��° ���� �����ϴ� ���� ����

            if (c == '"') //i��° ���ڰ� �����̸� 
            {
                inQuotes = !inQuotes; //inQuotes�� Ʈ���
            }
            else if (c == ',' && !inQuotes) //i��° ���簡 �޸��ų� inQuotes�� Ʈ���
            {
                values.Add(current.Trim()); //values �迭�� ���ڵ��� �����ص� current�� �յ� ���� ������ ����
                current = ""; //current�� �ٽ� �������� �ʱ�ȭ
            }
            else
            {
                current += c; //���ڿ��� i��° ���� ����
            }
        }

        values.Add(current.Trim()); 
        return values.ToArray(); //����Ʈ�� �迭�� �ٲپ� ��ȯ
    }

    /* ���� ���: ���� ������ ���̺��� �Ľ��Ͽ� ��ũ���ͺ� ������Ʈ�� �������� ������ �� �ִ� ���������� ���� --> �̴� ���� Ư�� �� �ݺ����� �뷱�̰� ���� ���� ������Ʈ�� �ٷ뿡 �־� ū ������ ����
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
