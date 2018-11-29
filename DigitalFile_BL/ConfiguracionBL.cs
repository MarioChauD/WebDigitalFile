using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigitalFile_BE;
using DigitalFile_DA;

namespace DigitalFile_BL
{
    public class ConfiguracionBL
    {

        public ConfiguracionBE getConfiguracion()
        {
            ConfiguracionDA oConfiguracionDA = new ConfiguracionDA();

            return oConfiguracionDA.getConfiguracionOracle();

        }

    }



}
