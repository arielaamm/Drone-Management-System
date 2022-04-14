using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL;
namespace BlApi
{
    public static class BLFactory
    {
        public static IBL GetBL(string type) => type switch
        {
            "BL" => BL.BL.GetInstance(),
            _ => throw new NotImplementedException()
        };
    }
}
