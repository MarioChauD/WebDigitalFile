using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigitalFile_DA;
using DigitalFile_BE;
using System.Data;

namespace DigitalFile_BL
{
   public class DocumentosBL
    {
        public DataTable getDocumentos(string sNumDocumento)
        {
            DocumentosDA oDocumentoDA = new DocumentosDA();
            ConfiguracionBL oConfiguracionBL = new ConfiguracionBL();
            ConfiguracionBE oConfiguracionBE = new ConfiguracionBE();

            DataTable oDocumentosBD = new DataTable();
            DataTable oDocumentos = new DataTable();

            string sPathOrigen = string.Empty;
            string sPathDestino = string.Empty;

            oConfiguracionBE = oConfiguracionBL.getConfiguracion();

            sPathOrigen = oConfiguracionBE.Directorio_Origen;
            sPathDestino = oConfiguracionBE.Directorio_Destino;

            oDocumentosBD = oDocumentoDA.getDocumentosOracle(sNumDocumento);

            oDocumentos.Columns.Add("ANIO");
            oDocumentos.Columns.Add("MES");
            oDocumentos.Columns.Add("MES_LITERAL");
            oDocumentos.Columns.Add("NOMBRE_ARCHIVO");
            oDocumentos.Columns.Add("RUTA");


            foreach (DataRow oRow in oDocumentosBD.Rows)
            {
                DataRow oNewRow = oDocumentos.NewRow();
                oNewRow["ANIO"] = oRow[0].ToString();
                oNewRow["MES"] = oRow[1].ToString();
                oNewRow["MES_LITERAL"] = oRow[2].ToString();
                oNewRow["NOMBRE_ARCHIVO"] = oRow[4].ToString();
                oNewRow["RUTA"] = oRow[5].ToString();

                oDocumentos.Rows.Add(oNewRow);

            }

            return oDocumentos;


        }

    }
}
