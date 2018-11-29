using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DigitalFile_DA;

namespace DigitalFile_BL
{
    public class ListaBL
    {
        public DataTable getListaTipoEmpleado()
        {
            ListaDA oListaDA = new ListaDA();

            return oListaDA.getListaTipoEmpleado();

        }
    }
}
