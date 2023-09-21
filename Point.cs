using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yotto
{
    internal class Point
    {
        /*ポイント
         *  ポイント表表示メソッド
         *  指定した役に代入するメソッド
         *  ポイント代入メソッド
         *  合計点計算
         */
        // プロパティ
        public int[] Points { get; set; }
        public string[] Items { get; set; }
        // コンストラクタ
        public Point()
        {
            Points = new int[14]; // エース~シックス,ボーナス,チョイス,役５つ,合計
            Items = new string[14] {"エース","デュース","トレイ","フォー","ファイブ","シックス"
            ,"ボーナス","チョイス","S・ストレート","B・ストレート","フルハウス","フォーダイス","ヨット","合計"};
        }
        // メソッド

        /// <summary>
        /// ポイント表を表示するメソッド
        /// </summary>
        public void PointsDisplay()
        {
            for (var i = 0; i < 14; i++)
            {
                Console.WriteLine("{0}  \t{1} ", Items[i], Points[i]);
            }
        }

        /// <summary>
        /// 指定した役に代入するメソッド
        /// </summary>
        /// <param name="role">指定した役</param>
        /// <param name="rolls">出目</param>
        /// <returns></returns>
        public int[] InputRole(string role, int[] rolls)
        {
            var sum1to6 = 0;
            var sum = 0;
            var dice = new Dice();

            // 役に加算
            switch (role)
            {

                case "エース":
                    Console.WriteLine("エースに入りました！");
                    foreach (var x in rolls)
                    {
                        if (x == 1)
                        {
                            Points[0] += x;
                        }
                    }
                    break;
                case "デュース":
                    Console.WriteLine("デュースに入りました！");
                    foreach (var x in rolls)
                    {
                        if (x == 2)
                        {
                            Points[1] += x;
                        }
                    }
                    break;
                case "トレイ":
                    Console.WriteLine("トレイに入りました！");
                    foreach (var x in rolls)
                    {
                        if (x == 3)
                        {
                            Points[2] += x;
                        }
                    }
                    break;
                case "フォー":
                    Console.WriteLine("フォーに入りました！");
                    foreach (var x in rolls)
                    {
                        if (x == 4)
                        {
                            Points[3] += x;
                        }
                    }

                    break;
                case "ファイブ":
                    Console.WriteLine("ファイブに入りました！");
                    foreach (var x in rolls)
                    {
                        if (x == 5)
                        {
                            Points[4] += x;
                        }
                    }
                    break;
                case "シックス":
                    Console.WriteLine("シックスに入りました！");
                    foreach (var x in rolls)
                    {
                        if (x == 6)
                        {
                            Points[5] += x;
                        }
                    }
                    break;

                case "チョイス":
                    Console.WriteLine("チョイスに入りました！");
                    foreach (var x in rolls)
                    {
                        Points[7] += x;
                    }
                    break;
                case "S・ストレート":
                    if (dice.RoleJudge(rolls) == "S・ストレート")
                    {
                        Console.WriteLine("S・ストレートに入りました！");
                        Points[8] += 15;
                    }
                    break;
                case "B・ストレート":
                    if (dice.RoleJudge(rolls) == "B・ストレート")
                    {
                        Console.WriteLine("B・ストレートに入りました！");
                        Points[9] += 30;
                    }
                    break;
                case "フルハウス":
                    if (dice.RoleJudge(rolls) == "フルハウス")
                    {
                        Console.WriteLine("フルハウスに入りました！");
                        foreach (var x in rolls)
                        {
                            Points[10] += x;
                        }
                    }
                    break;
                case "フォーダイス":
                    if (dice.RoleJudge(rolls) == "フォーダイス")
                    {
                        Console.WriteLine("フォーダイスに入りました！");
                        foreach (var x in rolls)
                        {
                            Points[11] += x;
                        }
                    }
                    break;
                case "ヨット":
                    if (dice.RoleJudge(rolls) == "ヨット")
                    {
                        Console.WriteLine("ヨットに入りました！");
                        Points[12] += 50;
                    }
                    break;
            }
            for (var j = 0; j < 6; j++)
            {
                sum1to6 += Points[j];
            }
            if (sum1to6 >= 63)
            {
                Points[6] += 35;
            }

            foreach (var x in Points)
            {
                sum += x;
            }
            Points[13] = sum - Points[13];
            return Points;

        }
        /// <summary>
        /// 敵オブジェクト用にInputRoleをオーバロード
        /// </summary>
        /// <param name="rolls">出目</param>
        /// <returns></returns>
        public int[] InputRole(int[] rolls)
        {
            var dice = new Dice();
            var judge = dice.RoleJudge(rolls); // 役があるかどうか判断
            var count = new int[6]; // 同じ出目が何個あるかの配列
            var max = 0; // 出目の重複がが最も多い変数
            var maxNum = 0;// 同じ出目がもっとも多い場所変数
            if (judge != "役なし")
            {
                for (var i = 8; i < 14; i++)
                {
                    // 役が一致かつポイント０なら入力
                    if (judge == Items[i] && Points[i] == 0)
                    {
                        InputRole(judge, rolls);
                        break;
                    }
                }
            }
            else
            {
                //得点０であるかつ最も多いものをカウントアップ
                for (var i = 0; i < 6; i++)
                {
                    if (Points[i] == 0)
                    {
                        for (var j = 0; j < 5; j++)
                        {
                            if (rolls[j] == i + 1)
                            {
                                count[i]++;
                            }
                        }
                    }
                }
                // カウントが最大となる場所を格納
                for (var i = 0; i < count.Length; i++)
                {
                    if (count[i] > max)
                    {
                        max = count[i];
                        maxNum = i;

                    }
                }

                // 代入
                InputRole(Items[maxNum], rolls);


            }
            return Points;
        }
    }
}
