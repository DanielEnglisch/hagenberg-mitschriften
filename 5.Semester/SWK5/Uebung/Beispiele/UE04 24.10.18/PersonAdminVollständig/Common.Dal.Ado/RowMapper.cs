using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dal.Ado
{
    public delegate T RowMapper<T>(IDataRecord row);
}
