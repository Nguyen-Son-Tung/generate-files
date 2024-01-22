using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIF.Core.DataPools
{
    public class MasterDataPool
    {
        public IEnumerable<string> GetSearchCodes(string postcode, int fromNumber, int toNumber)
        {
            for (int i = fromNumber; i <= toNumber; i++)
            {
                yield return $"{postcode.ToUpper()}|{i}||";
            }
        }

        public string GetCo()
        {
            return "0701-01";
        }

        
    }
}
