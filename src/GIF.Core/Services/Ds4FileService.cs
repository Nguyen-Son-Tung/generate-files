using GIF.Core.DataPools;
using GIF.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIF.Core.Services
{
    public class Ds4FileService
    {
        MasterDataPool dataPool = new MasterDataPool();
        public Ds4FileService()
        {
            
        }

        private IEnumerable<Ds4Model> GenerateBasicFile()
        {
            var searchCodes = dataPool.GetSearchCodes("1212TU", 1, 100);
            return Array.Empty<Ds4Model>();
        }
    }
}
