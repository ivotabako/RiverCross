using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiverCross
{
    public class Solution
    {
        public HashSet<string> successfulTrack = new HashSet<string>();
        public HashSet<string> backTrack = new HashSet<string>();
        List<HashSet<string>> leftBankOld = new List<HashSet<string>>();
        List<HashSet<string>> rightBankOld = new List<HashSet<string>>();

        public void FindPath(HashSet<string> leftBank, HashSet<string> rightBank)
        {
            if (rightBank.Contains("H") && rightBank.Contains("F") && rightBank.Contains("G") && rightBank.Contains("W"))
            {
                Console.WriteLine("win");

                foreach (var item in successfulTrack)
                {
                    Console.WriteLine(item);
                }

                Environment.Exit(0);
             
            }

            for (int i = 1; i <= 8; i++)
            {
                CrossTheRiver(leftBank, rightBank, i);

                if (rightBank.Contains("H") && rightBank.Contains("F") && rightBank.Contains("G") && rightBank.Contains("W"))
                {
                    return;
                }
            }

            if (rightBank.Contains("H") && rightBank.Contains("F") && rightBank.Contains("G") && rightBank.Contains("W"))
            {
                return;
            }

            successfulTrack.RemoveWhere(m => successfulTrack.Last() == m);

            leftBankOld.RemoveAt(leftBankOld.Count - 1);
            rightBankOld.RemoveAt(rightBankOld.Count - 1);

            FindPath(leftBankOld.Last(), rightBankOld.Last());
        }

        private void CrossTheRiver(HashSet<string> leftBank, HashSet<string> rightBank, int crossType)
        {
            switch (crossType)
            {
                case 1 :
                    SailLeftToRight(leftBank, rightBank, "H", "F");                      
                    break;
                case 2:
                    SailLeftToRight(leftBank, rightBank, "H", "G");
                    break;
                case 3:
                    SailLeftToRight(leftBank, rightBank, "H", "W");
                    break;
                case 4:
                    SailLeftToRight(leftBank, rightBank, "H", "");
                    break;
                case 5:
                    SailRightToLeft(leftBank, rightBank, "H", "F");
                    break;
                case 6:
                    SailRightToLeft(leftBank, rightBank, "H", "G");
                    break;
                case 7:
                    SailRightToLeft(leftBank, rightBank, "H", "W");
                    break;
                case 8:
                    SailRightToLeft(leftBank, rightBank, "H", "");
                    break;
                default:
                    break;
            }            

        }

        public string Format(HashSet<string> leftBank, HashSet<string> rightBank)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" Left: ");
            var left = leftBank.ToList();
            left.Sort();
            foreach (var item in left)
            {
                sb.Append(item);
                sb.Append("-");
            }
            sb.Append(" Right: ");
            var right = rightBank.ToList();
            right.Sort();
            foreach (var item in right)
            {
                sb.Append(item);
                sb.Append("-");
            }

            return sb.ToString();
        }        

        private void SailLeftToRight(HashSet<string> leftBank, HashSet<string> rightBank, string item1, string item2)
        {
            if (leftBank.Contains(item1) && (leftBank.Contains(item2) || string.IsNullOrWhiteSpace(item2)))
            {
                MoveLeftToRight(leftBank, rightBank, item1);
                if (!string.IsNullOrWhiteSpace(item2))                
                    MoveLeftToRight(leftBank, rightBank, item2);
                if (IsNotValid(leftBank, rightBank))
                {
                    MoveRightToLeft(leftBank, rightBank, item1);
                    if (!string.IsNullOrWhiteSpace(item2))
                        MoveRightToLeft(leftBank, rightBank, item2);
                }
                else
                {
                    var line = Format(leftBank, rightBank);

                    if (!backTrack.Contains(line))
                    {
                        successfulTrack.Add(line);
                        backTrack.Add(line);
                    }
                    LastValid(leftBank, rightBank);

                    FindPath(leftBank, rightBank);
                }
            }
        }

        private void LastValid(HashSet<string> leftBank, HashSet<string> rightBank)
        {
            HashSet<string> left = new HashSet<string>();
            foreach (var item in leftBank)
            {
                left.Add(item);
            }
            leftBankOld.Add(left);
            HashSet<string> right = new HashSet<string>();
            foreach (var item in rightBank)
            {
                right.Add(item);
            }
            rightBankOld.Add(right);
        }

        private void SailRightToLeft(HashSet<string> leftBank, HashSet<string> rightBank, string item1, string item2)
        {
            if (rightBank.Contains(item1) && (rightBank.Contains(item2) || string.IsNullOrWhiteSpace(item2)))
            {
                MoveRightToLeft(leftBank, rightBank, item1);
                if (!string.IsNullOrWhiteSpace(item2))
                    MoveRightToLeft(leftBank, rightBank, item2);
                if (IsNotValid(leftBank, rightBank))
                {
                    MoveLeftToRight(leftBank, rightBank, item1);
                    if (!string.IsNullOrWhiteSpace(item2))
                        MoveLeftToRight(leftBank, rightBank, item2);
                }
                else
                {
                    var line = Format(leftBank, rightBank);

                    if (!backTrack.Contains(line))
                    {
                        successfulTrack.Add(line);
                        backTrack.Add(line);
                    }
                    LastValid(leftBank, rightBank);

                    FindPath(leftBank, rightBank);
                }
            }
        }

        private void MoveLeftToRight(HashSet<string> leftBank, HashSet<string> rightBank, string item)
        {
            leftBank.Remove(item);            
            rightBank.Add(item);            
        }

        private void MoveRightToLeft(HashSet<string> leftBank, HashSet<string> rightBank, string item)
        {
            rightBank.Remove(item);
            leftBank.Add(item);
        }

        private bool IsNotValid(HashSet<string> leftBank, HashSet<string> rightBank)
        {
            if (leftBank.Contains("H") && rightBank.Contains("F") && rightBank.Contains("G"))
                return true;
            if (leftBank.Contains("H") && rightBank.Contains("W") && rightBank.Contains("G"))
                return true;
            if (rightBank.Contains("H") && leftBank.Contains("F") && leftBank.Contains("G"))
                return true;
            if (rightBank.Contains("H") && leftBank.Contains("W") && leftBank.Contains("G"))
                return true;
            if (IsInBackTrackList(leftBank, rightBank))
                return true;

            return false;
        }

        private bool IsInBackTrackList(HashSet<string> leftBank, HashSet<string> rightBank)
        {
            var line = Format(leftBank, rightBank);
            return backTrack.Contains(line);
        }
    }
}
