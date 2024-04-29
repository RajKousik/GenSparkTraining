using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetcodeApplication
{
    public class ExcelColumnTitle
    {
        public async Task<string> convertToTitle(int columnNumber)
        {
            string res = "";
            while (columnNumber > 0)
            {
                
                char c = (char)('A' + ((columnNumber - 1) % 26));
                res = c + res;
                columnNumber = (columnNumber - 1) / 26;
            }
            await Task.Delay(0);
            return res;
        }
    }
}
