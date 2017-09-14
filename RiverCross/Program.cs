using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiverCross
{
    class Program
    {
        static void Main(string[] args)
        {
            HashSet<string> leftBank = new HashSet<string>() { "H", "F", "G", "W" };
            HashSet<string> rightBank = new HashSet<string>();
            Solution s = new Solution();
            s.backTrack.Add(s.Format(leftBank, rightBank));
            s.successfulTrack.Add(s.Format(leftBank, rightBank));
            s.FindPath(leftBank, rightBank);

            Console.ReadLine();
        }
    }
}
