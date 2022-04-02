using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace DalApi
{
    public static class DalFactory
    {
        public static IDal GetDal(string type) => type switch
        {
            "DalObject" => DalObject.GetInstance(),
            "DalXml" => Dal.DalXml.GetInstance()
        };
    }
}
