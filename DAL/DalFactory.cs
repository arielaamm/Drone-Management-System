using DAL;
using System;

namespace DalApi
{
    /// <summary>
    /// Defines the <see cref="DalFactory" />.
    /// </summary>
    public static class DalFactory
    {
        public static IDal GetDal(string type) => type switch
        {
            "DalObject" => DalObject.GetInstance(),
            "DalXml" => DalXml.GetInstance(),
            _ => throw new NotImplementedException()
        };
    }
}
