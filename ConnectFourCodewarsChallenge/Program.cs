using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ConnectFourCodewarsChallenge
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> piecesPositionList = new List<string>()
            {

             "C_Yellow",
             "B_Red",
             "B_Yellow",
             "E_Red",
             "D_Yellow",
             "G_Red",
             "B_Yellow",
             "G_Red",
             "E_Yellow",
             "A_Red",
             "G_Yellow",
             "C_Red",
             "A_Yellow",
             "A_Red",
             "D_Yellow",
             "B_Red",
             "G_Yellow",
             "A_Red",
             "F_Yellow",
             "B_Red",
             "D_Yellow",
             "A_Red",
             "F_Yellow",
             "F_Red",
             "B_Yellow",
             "F_Red",
             "F_Yellow",
             "G_Red",
             "A_Yellow",
             "F_Red",
             "C_Yellow",
             "C_Red",
             "G_Yellow",
             "C_Red",
             "D_Yellow",
             "D_Red",
             "E_Yellow",
             "D_Red",
             "E_Yellow",
             "C_Red",
             "E_Yellow",
             "E_Red",

            };

            Console.WriteLine(WhoIsWinner(piecesPositionList));

            Console.ReadKey();
        }

        public static string WhoIsWinner(List<string> piecesPositionList)
        {
            var playersMoves = ConvertListTo2DArray(piecesPositionList);
            return AnyMatches(playersMoves);
        }

        private static string AnyMatches(string[,] playersMoves)
        {
            var diagonalUpperCheck = new int[7, 6];
            var diagonalLowerCheck = new int[7, 6];
            var horizontalMatchCheck = new int[7, 6];
            var verticalMatchCheck = new int[7, 6];
            List<string> whoWonFirst = new List<string>();

            //Horizontal Method
            for (int y = 0; y < 6; y++)
            {
                for (int x = 0; x < 7; x++)
                {
                    if (x != 0 && playersMoves[x, y]?.FirstOrDefault() != null && playersMoves[x - 1, y]?.FirstOrDefault() != null
                        && playersMoves[x, y]?.FirstOrDefault() == playersMoves[x - 1, y]?.FirstOrDefault())
                    {
                        horizontalMatchCheck[x, y]++;

                        if (horizontalMatchCheck[x - 1, y] != 0)
                        {
                            horizontalMatchCheck[x, y] += horizontalMatchCheck[x - 1, y];

                            if (horizontalMatchCheck[x, y] == 3)
                            {
                                var nextColour = playersMoves[x + 1, y]?.FirstOrDefault();
                                var currentColour = playersMoves[x, y]?.FirstOrDefault();
                                var nextPosition = playersMoves[x + 1, y] != null ? int.Parse(playersMoves[x + 1, y].Substring(1)) : default;
                                int firstPosition = playersMoves[x - 3, y] != null ? int.Parse(playersMoves[x - 3, y].Substring(1)) : default;


                                if (x + 1 != 7 && !(nextColour == currentColour && nextPosition < firstPosition))
                                {
                                    List<string> selectHighest = new List<string>()
                                        {
                                           playersMoves[x-3, y],
                                           playersMoves[x-2, y],
                                           playersMoves[x-1, y],
                                           playersMoves[x, y]
                                        };

                                    var result = selectHighest.Select(d => int.Parse(d.Substring(1))).Max();
                                    var yy = selectHighest.First(p => p.Contains(result.ToString()));
                                    whoWonFirst.Add(yy);
                                }
                                else
                                    horizontalMatchCheck[x, y]--;
                            }
                        }
                    }
                }
            }


            for (int x = 0; x < 7; x++)
            {
                for (int y = 0; y < 6; y++)
                {
                    // Vertical Check
                    if (y != 0 && playersMoves[x, y]?.FirstOrDefault() != null && playersMoves[x, y - 1]?.FirstOrDefault() != null
                          && playersMoves[x, y]?.FirstOrDefault() == playersMoves[x, y - 1]?.FirstOrDefault())
                    {
                        verticalMatchCheck[x, y]++;
                        if (verticalMatchCheck[x, y - 1] != 0)
                        {
                            verticalMatchCheck[x, y] += verticalMatchCheck[x, y - 1];

                            if (verticalMatchCheck[x, y] == 3)
                            {
                                //Debugging Purpose
                                var nextColour = playersMoves[x, y + 1]?.FirstOrDefault();
                                var currentColour = playersMoves[x, y]?.FirstOrDefault();
                                var nextPosition = playersMoves[x, y + 1] != null ? int.Parse(playersMoves[x, y + 1]?.Substring(1)) : default;
                                var firstPosition = playersMoves[x, y - 3] != null ? int.Parse(playersMoves[x, y - 3]?.Substring(1)) : default;

                                if (y + 1 != 6 && !(nextColour == currentColour && nextPosition < firstPosition))
                                {
                                    List<string> selectHighest = new List<string>()
                                    {
                                         playersMoves[x, y-3],
                                         playersMoves[x, y-2],
                                         playersMoves[x, y-1],
                                         playersMoves[x, y]
                                    };

                                    var result = selectHighest.Select(d => int.Parse(d.Substring(1))).Max();
                                    var yy = selectHighest.First(p => p.Contains(result.ToString()));
                                    whoWonFirst.Add(yy);
                                }
                                else verticalMatchCheck[x, y]--;

                            }
                        }
                    }

                    // Upward Diagonal Check
                    if (x != 0 && y != 0 && playersMoves[x, y]?.FirstOrDefault() != null && playersMoves[x - 1, y - 1]?.First() != null)
                    {
                        if (playersMoves[x, y]?.First() == playersMoves[x - 1, y - 1]?.First())
                        {
                            diagonalUpperCheck[x, y]++;

                            if (diagonalUpperCheck[x - 1, y - 1] != 0)
                            {

                                diagonalUpperCheck[x, y] += diagonalUpperCheck[x - 1, y - 1];

                                if (diagonalUpperCheck[x, y] == 3)
                                {
                                    if (!(x + 1 == 7 && y + 1 == 6))
                                    {
                                        var nextColour = playersMoves[x + 1, y + 1]?.FirstOrDefault();
                                        var currentColour = playersMoves[x, y]?.FirstOrDefault();
                                        var nextPosition = playersMoves[x + 1, y + 1] != null ? int.Parse(playersMoves[x + 1, y + 1]?.Substring(1)) : default;
                                        var firstPosition = playersMoves[x - 3, y - 3] != null ? int.Parse(playersMoves[x - 3, y - 3]?.Substring(1)) : default;

                                        if (!(nextColour == currentColour && nextPosition < firstPosition))
                                        {
                                            List<string> selectHighest = new List<string>()
                                                {
                                                     playersMoves[x - 3, y - 3],
                                                     playersMoves[x - 2, y - 2],
                                                     playersMoves[x - 1, y - 1],
                                                     playersMoves[x, y]
                                                };
                                            var result = selectHighest.Select(d => int.Parse(d.Substring(1))).Max();
                                            var yy = selectHighest.First(p => p.Contains(result.ToString()));
                                            whoWonFirst.Add(yy);
                                        }
                                        else
                                            diagonalUpperCheck[x, y]--;


                                    }
                                    else
                                    {
                                        List<string> selectHighest = new List<string>()
                                        {
                                            playersMoves[x - 3, y - 3],
                                            playersMoves[x - 2, y - 2],
                                            playersMoves[x - 1, y - 1],
                                            playersMoves[x, y]
                                        };

                                        var result = selectHighest.Select(d => int.Parse(d.Substring(1))).Max();
                                        var yy = selectHighest.First(p => p.Contains(result.ToString()));
                                        whoWonFirst.Add(yy);
                                    }
                                }
                                else
                                    diagonalUpperCheck[x, y]--;
                            }
                        }
                    }

                    // Downward Diagonal Check
                    if (x != 0 && y != 5 && playersMoves[x, y]?.FirstOrDefault() != null && playersMoves[x - 1, y + 1]?.FirstOrDefault() != null)
                    {
                        if (playersMoves[x, y]?.FirstOrDefault() == playersMoves[x - 1, y + 1]?.FirstOrDefault())
                        {

                            diagonalLowerCheck[x, y]++;

                            if (diagonalLowerCheck[x - 1, y + 1] != 0)
                            {
                                diagonalLowerCheck[x, y] += diagonalLowerCheck[x - 1, y + 1];
                                if (diagonalLowerCheck[x, y] == 3)
                                {
                                    //Debugging Purpose
                                    var nextColour = playersMoves[x + 1, y - 1]?.FirstOrDefault();
                                    var currentColour = playersMoves[x, y]?.FirstOrDefault();
                                    var nextPosition = playersMoves[x + 1, y - 1] != null ? int.Parse(playersMoves[x + 1, y - 1]?.Substring(1)) : default;
                                    var firstPosition = playersMoves[x - 3, y - 3] != null ? int.Parse(playersMoves[x - 3, y - 3]?.Substring(1)) : default;

                                    if (y - 1 != 0 && x - 1 != 0 && !(nextColour == currentColour && nextPosition < firstPosition))
                                    {
                                        List<string> selectHighest = new List<string>()
                                            {
                                                playersMoves[x - 3, y + 3],
                                                playersMoves[x - 2, y + 2],
                                                playersMoves[x - 1, y + 1],
                                                playersMoves[x, y]
                                            };

                                        var result = selectHighest.Select(d => int.Parse(d.Substring(1))).Max();
                                        var yy = selectHighest.First(p => p.Contains(result.ToString()));
                                        whoWonFirst.Add(yy);
                                    }
                                    else
                                        diagonalLowerCheck[x, y]--;
                                }
                            }
                        }
                    }
                }
            }

            if (whoWonFirst.Count > 0)
                return whoWonFirstMethod(whoWonFirst);

            return "Draw";
        }

        static string whoWonFirstMethod(List<string> whoWonFirst)
        {
            if (whoWonFirst.Count == 1)
                return (string)whoWonFirst[0].Substring(0, 1) == "R" ? "Red" : "Yellow";

            var item = whoWonFirst.Select(x => int.Parse(x.Substring(1))).Min();
            var y = whoWonFirst.FirstOrDefault(x => x.Contains(item.ToString()));
            return y.Substring(0, 1) == "R" ? "Red" : "Yellow";
        }


        static string[,] ConvertListTo2DArray(List<string> piecesPositionList)
        {
            var playerInputPositions = string.Join(",", piecesPositionList);

            string stringFormatted = Regex.Replace(playerInputPositions, "[^A-Z]", "");

            char[] stringCharArray = stringFormatted.ToCharArray();
            string[,] charPlayer = new string[7, 6];

            int aCount = -1, bCount = -1, cCount = -1, dCount = -1, eCount = -1, fCount = -1, gCount = -1;
            int positionCount = -1;

            for (int i = 0; i < stringCharArray.Length; i += 2)
            {
                positionCount++;
                switch (stringCharArray[i])
                {
                    case 'A':
                        aCount++;
                        charPlayer[0, aCount] = stringCharArray[i + 1] + positionCount.ToString();
                        break;
                    case 'B':
                        bCount++;
                        charPlayer[1, bCount] = stringCharArray[i + 1] + positionCount.ToString();
                        break;
                    case 'C':
                        cCount++;
                        charPlayer[2, cCount] = stringCharArray[i + 1] + positionCount.ToString();
                        break;
                    case 'D':
                        dCount++;
                        charPlayer[3, dCount] = stringCharArray[i + 1] + positionCount.ToString();
                        break;
                    case 'E':
                        eCount++;
                        charPlayer[4, eCount] = stringCharArray[i + 1] + positionCount.ToString();
                        break;
                    case 'F':
                        fCount++;
                        charPlayer[5, fCount] = stringCharArray[i + 1] + positionCount.ToString();
                        break;
                    case 'G':
                        gCount++;
                        charPlayer[6, gCount] = stringCharArray[i + 1] + positionCount.ToString();
                        break;
                    default:
                        Console.WriteLine("Error Occured");
                        break;
                }
            }

            for (int x = 0; x < 7; x++)
            {
                Console.Write($"{x}");
                for (int y = 0; y < 6; y++)
                {
                    Console.Write($"  {charPlayer[x, y]}");
                }
                Console.WriteLine();
            }

            return charPlayer;
        }
    }
}