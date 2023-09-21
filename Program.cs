using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yotto
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*
             * 12ターン繰り返す
             *  「
             *   |サイコロ振る
             *   |サイコロ振り直し
             *   |役判定
             *   |役指定
             *   |合計
             *   L
             */
            var count = 0; // 入力の例外処理のためのカウンタ(４８行~)
            string input; // 指定役の入力
            var dice1 = new Dice(); // オブジェクト生成
            var point1 = new Point();

            var dice2 = new Dice();
            var point2 = new Point();

            int turn; // ターン数
            int retry; // 振り直す回数
            string yesNo;

            // スタート画面
            Console.WriteLine("ヨット！");

            // ルール設定
            while (true)
            {
                while (true)
                {
                    Console.Write("ターン数を設定してください > ");
                    var sTurn = Console.ReadLine();
                    if (int.TryParse(sTurn, out var x) && x > 0)
                    {
                        turn = x;
                        break; // 自然数であれば脱出
                    }
                }

                while (true)
                {
                    Console.Write("振り直す回数を設定してください > ");
                    var sRetry = Console.ReadLine();
                    if (int.TryParse(sRetry, out var x) && x >= 0)
                    {
                        retry = x;
                        break; // 0以上の整数であれば脱出
                    }
                }
                Console.WriteLine($"ターン数:{turn}ターン\t振り直す回数：{retry}回");
                do
                {
                    Console.WriteLine("この設定でよろしければはy,ルールを変更したい場合はnを入力して下さい");
                    yesNo = Console.ReadLine();
                } while (!(yesNo == "y" || yesNo == "n")); // y,n以外が入力されたらループ
                if (yesNo == "y")
                {
                    break; // yであれば本ループ脱出
                }
            }


            // ターン数文繰り返す
            for (var i = 0; i < turn; i++)
            {
                Console.WriteLine($"{i + 1}ターン目");
                Console.WriteLine($"あなたのターンです");


                // 5個振る
                dice1.Roll(5);
                // ポイント表生成
                point1.PointsDisplay();
                // 振り直し
                dice1.Retry(retry, 5);
                // リセット
                Console.Clear();

                // 出目表示
                dice1.RollsDisplay(dice1.Rolls);
                // ポイント表生成
                point1.PointsDisplay();
                // 役指定
                do
                {
                    Console.WriteLine("指定したい役を入力して下さい。");
                    Console.Write("指定 > ");
                    input = Console.ReadLine(); // 入力の変数
                    for (var j = 0; j < 12; j++)
                    {
                        if (j == 6)
                        {
                            continue;
                        }
                        if (input == point1.Items[j])
                        {
                            count++;
                        }
                    }
                } while (count == 0); // どれも入力と一致しなかったら繰り返す


                // 役に代入
                point1.InputRole(input, dice1.Rolls);

                Console.WriteLine(" 得点を表示します。Enterキーをを押してください。");
                Console.ReadLine();

                Console.Clear();

                // 表示
                point1.PointsDisplay();

                Console.WriteLine("次は敵のターンです。Enterキーをを押してください。");
                Console.ReadLine();

                // リセット
                Console.Clear();


                // 敵のターン
                Console.WriteLine($"{i + 1}ターン目");
                Console.WriteLine($"敵のターンです");

                //サイコロを回す
                dice2.Roll(5);
                point2.InputRole(dice2.Sort(dice2.Rolls));

                Console.WriteLine(" 得点を表示します。Enterキーをを押してください。");
                Console.ReadLine();
                Console.Clear();

                point2.PointsDisplay();

                if (i == turn - 1)
                {
                    Console.WriteLine("最終結果を表示します。Enterキーをを押してください。");
                }
                else
                {
                    Console.WriteLine("次のターンに移ります。Enterキーをを押してください。");
                }
                Console.ReadLine();

                // リセット
                Console.Clear();
            }

            // 最終結果表示
            Console.WriteLine("自分");
            point1.PointsDisplay();
            Console.WriteLine("相手");
            point2.PointsDisplay();
            if (point1.Points[13] > point2.Points[13])
            {
                Console.WriteLine("あなたの勝利です！");
            }
            else if (point1.Points[13] == point2.Points[13])
            {
                Console.WriteLine("引き分けです");
            }
            else
            {
                Console.WriteLine("あなたの敗北です...");
            }

        }
    }
}
