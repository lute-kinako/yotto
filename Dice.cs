using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yotto
{
    internal class Dice
    {
        /* サイコロ
         *  サイコロを振るメソッド
         *  ソートを行うメソッド
         *  振り直すメソッド
         *  役判定メソッド
         */

        // フィールド
        // プロパティ
        public int[] Rolls { get; set; }
        // コンストラクタ
        public Dice()
        {
            Rolls = new int[5];
        }
        // メソッド

        /// <summary>
        /// サイコロを指定個数振るメソッド
        /// </summary>
        /// <param name="num">何個振るか</param>
        /// <returns>サイコロの出目の配列</returns>
        public int[] Roll(int num)
        {
            Console.WriteLine("サイコロを振ってください");
            for (var j = 0; j < num; j++)
            {
                Console.Write($"{j + 1}個目 > ");
                Console.ReadLine();
                var rnd = new Random();
                Rolls[j] = rnd.Next(1, 7);
            }
            // ソートして表示
            Rolls = Sort(Rolls);
            RollsDisplay(Rolls);
            return Rolls;
        }

        /// <summary>
        /// サイコロを指定回、指定個数振り直すメソッド
        /// </summary>
        /// <param name="time">振る回数</param>
        /// <param name="num">振るサイコロの個数</param>
        /// <returns></returns>
        public int[] Retry(int time, int num)
        {
            string retry;

            for (var j = 0; j < time; j++)
            {
                var strInput = ""; // 指定場所の入力
                var strPlace = ""; // 場所表記
                string[] placeRetry; // 指定場所の文字列配列
                do
                {
                    Console.WriteLine("振り直したいときはy,このままで良かったらnを入力して下さい");
                    retry = Console.ReadLine();
                } while (!(retry == "y" || retry == "n"));

                if (retry == "y")
                {
                    // やり直しの処理
                    Console.WriteLine("やり直したいサイコロを書きなさい");
                    for (var k = 0; k < num; k++)
                    {
                        strInput = Console.ReadLine();

                        if (strInput == "")
                        {
                            break;　// Enterで脱出
                        }

                        // 規定外の入力をはじく
                        else if (!(int.TryParse(strInput, out var x)) || (x < 1 || x > 6))
                        {
                            Console.WriteLine("規定外の入力です");
                            k--;
                            continue; // やり直し
                        }
                        else
                        {
                            if (k != 0)
                            {
                                strPlace += ",";
                            }
                            strPlace += strInput.Trim();
                        }
                    }

                    if (strPlace == "")
                    {
                        Console.WriteLine("未入力");
                        j--;
                        continue;
                    }
                    placeRetry = strPlace.Split(',');

                    for (var k = 0; k < placeRetry.Length; k++)
                    {
                        Console.Write($"{int.Parse(placeRetry[k])}個目：");
                        Console.ReadLine();
                        var rnd = new Random();
                        Rolls[int.Parse(placeRetry[k]) - 1] = rnd.Next(1, 7);
                    }
                    // ソートして表示
                    Rolls = Sort(Rolls);
                    RollsDisplay(Rolls);
                }
                else
                {
                    break;
                }
            }
            return Rolls;
        }

        /// <summary>
        /// 出目を昇順にソートする
        /// </summary>
        /// <param name="rolls">出目</param>
        /// <returns>ソートされた出目</returns>
        public int[] Sort(int[] rolls)
        {
            int print; // 入れ替え時の保持用変数
            int min; // 位置の最小値

            // 位置の並び変え
            for (var j = 0; j < rolls.Length - 1; j++)
            {
                min = j; // i番目を最左とする
                for (var k = j + 1; k < rolls.Length; k++) // iから右の要素について調べる
                {
                    if (rolls[k] < rolls[min])
                    {
                        min = k; // j番目が最左(要素が最小)に更新
                    }
                }
                // 入れ替え
                print = rolls[j];
                rolls[j] = rolls[min];
                rolls[min] = print;
            }
            return rolls;

        }

        /// <summary>
        /// 画面に出目を表示をするメソッド
        /// </summary>
        /// <param name="rolls">出目</param>
        public void RollsDisplay(int[] rolls)
        {
            // 画面表示
            Console.Write("場所 ： ");
            for (var j = 0; j < rolls.Length; j++)
            {
                Console.Write("{0} ", j + 1);
            }
            Console.WriteLine();
            // 出目の表示
            Console.Write("出目 ： ");
            foreach (var x in rolls)
            {
                Console.Write("{0} ", x);
            }
            Console.WriteLine();
            // 役の表示
            Console.WriteLine("役：{0}", RoleJudge(Rolls));
        }

        /// <summary>
        /// 役を判定するメソッド
        /// </summary>
        /// <param name="rolls">出目</param>
        public string RoleJudge(int[] rolls)
        {
            string role = ""; // 役名
            var equal = new int[6]; // 一致数を格納
            var succeed = new int[6]; // 連続数を格納。１：1,2 、２：2,3、...６：なし
            int plus = 1; // 加算数
            var maxSucceed = 0; // 最大連続数
            int fullHouse = 0; // フルハウスを判断する


            for (var i = 0; i < rolls.Length - 1; i++)
            {
                if (equal[rolls[i] - 1] != 0) { continue; }

                for (var j = i + 1; j < rolls.Length; j++)
                {

                    // 一致
                    if (rolls[i] == rolls[j])
                    {
                        equal[rolls[i] - 1]++;
                    }
                    // 連続
                    if (rolls[i] + plus == rolls[j])
                    {
                        succeed[rolls[i] - 1]++;
                        plus++;
                    }
                }
            }
            // 最大連続数
            foreach (var x in succeed)
            {
                if (x > maxSucceed)
                {
                    maxSucceed = x;
                }
            }
            // 判定
            for (var i = 0; i < 6; i++)
            {
                if (equal[i] == 4)
                {
                    role = "ヨット";
                    break;
                }
                else if (equal[i] == 3)
                {
                    role = "フォーダイス";
                    break;
                }
                else if (equal[i] == 1 || equal[i] == 2)
                {
                    if ((fullHouse == 1 && equal[i] == 2) || (fullHouse == 2 && equal[i] == 1))
                    {
                        role = "フルハウス";
                        break;
                    }
                    fullHouse = equal[i];
                }
                else if (maxSucceed == 4)
                {
                    role = "B・ストレート";
                    break;
                }
                else if (maxSucceed == 3)
                {
                    role = "S・ストレート";
                    break;
                }
                if (i >= 5)
                {

                    return "役なし";
                }
            }
            return role;
        }
    }
}
