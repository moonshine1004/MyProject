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
            Debug.LogError("[CardParser] CSV 줄 수가 너무 적습니다.");
            return result;
        }

        string[] headers = rows[1];

        for (int i = 3; i < rows.Count; i++) // 데이터 줄은 3번째부터 시작
        {
            string[] row = rows[i];

            if (row.Length == 0 || string.IsNullOrWhiteSpace(row[0]))
            {
                Debug.LogWarning($"[CardParser] 빈 줄 스킵: row {i}");
                continue;
            }

            Card card = ScriptableObject.CreateInstance<Card>();

            for (int j = 0; j < headers.Length && j < row.Length; j++)
            {
                string header = headers[j].Trim();
                string value = row[j].Trim();

                if (string.IsNullOrEmpty(value)) continue; // 빈 값 스킵

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
                    Debug.LogWarning($"[CardParser] '{header}' 값 '{value}' 처리 실패 (row {i}): {ex.Message}");
                }
            }

            result.Add(card);
        }

        return result;
    }


    private static List<string[]> ParseCSV(string csvText)
    {
        List<string[]> result = new List<string[]>(); //문자열 리스트 result선언
        var lines = csvText.Split(new[] { "\r\n", "\n" }, System.StringSplitOptions.RemoveEmptyEntries);//빈 문자열을 제외하고 특정 구분자("\r\n", "\n"-->줄바꿈) 단위로 잘라 열(line)에 저장

        foreach (var line in lines)//각 열을 다시 자름 lines들을 순회하여 얻은 것을 line에 저장
        {
            result.Add(ParseCSVLine(line));//result 리스트에 ParseCSVLine메서드 실행하여 얻은 값을 대입
        }

        return result;
    }

    private static string[] ParseCSVLine(string line)
    {
        List<string> values = new List<string>(); //values라는 문자열 배열 선언
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

    /* 개발 기능: 엑셀 데이터 테이블을 파싱하여 스크립터블 오브젝트를 동적으로 생성할 수 있도록 함 --> 이는 게임 특성 상 반복적인 밸런싱과 많은 양의 오브젝트를 다룸에 있어 큰 이점을 가짐
     * 사용 기술: 유니티 스크립터블 오브젝트, using System.IO, AssetDatabase, [ContextMenu]
     *  1)유니티 스크립터블 오브젝트: 유니티의 스크립터블 오브젝트는 구조가 같은 대량의 서로 다른 오브젝트를 다루는데에 용이한 유니티의 데이터 컨테이너이다. 
     *                                이 기능을 사용하면 복수의 데이터를 빠르게 에셋으로 만드어 저장하여 사용할 수 있다는 이점이 있다.
     *                                본 프로젝트는 이를 이용하여 프로젝트의 핵심 시스템인 '카드 시스템'에 필요한 카드를 간편하고 빠르게 제작했다.
     *                                스크립터블 오브젝트 기능은 기획자와 협업하여 데이터 테이블을 통해 사용하면 더욱 효과적인 기능으로, 본 프로젝트 또한 기획자가 제작한 카드 데이터 테이블을 통해 카드 스크립터블 오브젝트를 제작했다.
     *  2)System.IO: System.IO은 .NET 라이브러리가 제공하는 네임 스페이스로 파일, 스트림의 입출력을 돕는다. 본 프로젝트에서는 System.IO의 Directory를 이용하여 AssetDatabase의 폴더를 다루었다.
     *  3)AssetDatabase: 
     * 문제상황 및 해결과정
     *  1)파싱해 온 데이터에 대한 데이터 타입 정의에 있어서의 문제점
     *   -->파싱해 온 데이터를 변수에 대입하는 과정에 있어 모든 값을 문자열로 가져왔기 때문에 문자열이 아닌 데이터를 받아드리지 못하는 문제 발생
     *   -->이를 수정하고자 명시적 형변환을 이용하여 데이터를 받아왔으나 유효하지 않은 enum을 받아오는 문제를 확인함
     *   -->이를 해결하기 위해 int.Parse(), enum.Parse()등 Parse를 사용하였지만 Parse는 존재하지 않는 enum값, 포맷 오류 등의 예외 상황에 대한 대처가 어려움
     *   -->따라서 TryParse()를 사용하여 이러한 문제 상황을 개선함
     * 
     * 
     * 배운점
     *  1)협업의 있어서의 필요성 및 중요성: 오늘날의 게임은 하드웨어 스펙의 비약적 상승과 함께 점점 더 복잡해져 가고 있다. 이에 따라 기획자와 프로그래머가 다뤄야하는 데이터의 양 또한 늘어가고 있다.
     *                                      본 프로젝트에서 사용한 카드(오브젝트)의 개수는 약 30개였음에도 카드의 이름을 정의하고 이를 활용하는데에 있어 많은 어려움을 겪었다.
     *                                      즉, 고차원적으로 진화해 가는 현대 게임에서의 오브젝트를 다루는 데는 이보다 더한 수고스러움이 들기 때문에, 이러한 수고를 덜기 위해 만들어지는 데이터 테이블은 
     *                                      게임 제작에 필수 요소가 되었으며, 프로그래머에게 있어서는 이를 제작하는 기획자와의 소통이 더욱 중요해졌다고 볼 수 있다.
     *                                      본 프로젝트를 제작함에 있어서 이러한 협업의 필요성을 느꼈으며, 프로그래머와 기획자의 소통과 이러한 소통의 다리가 되어주는 문서의 중요성 또한 뼈저리게 실감할 수 있었다.
     *  
     * 성장한 부분: 
     * 
     * 
     * 향후 보완할 점
     *  1)현재는 모든 값을 String으로 파싱해 온 후 명시적 형변환하는 방식을 사용하였지만, 이는 방대한 자료를 다룰 때의 비용 문제와 각 데이터를 재정의하는 코드를 따로 작성해야 되는 비효율성에 있어 개선이 요구됨
     *      따라서 향후에는 데이터 테이블의 헤더를 인식하여 데이터를 파싱할 때부터 해당 자료형으로 데이터를 받아올 수 있도록 개선할 예정임
     * 
     * 
     * 
     * 
     * 
     */
}
