using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.SqlClient;

namespace BuscaPizza.Models
{
    public class ModelBase : IDisposable
    {
        const string SEVIDOR = "localhost";
        const string BANCODADOS = "BDFinal";

        //Pertence ao namespace system.data.sqlclient
        protected SqlConnection connection;

        public ModelBase()
        {
            string strConn = $"Data Source={SEVIDOR};Initial Catalog={BANCODADOS};Integrated Security=true; MultipleActiveResultSets = true";//User Id={USUARIO};Password={SENHA}";

            connection = new SqlConnection(strConn);
            connection.Open();
        }
        public void Dispose()
        {
            //Ultima coisa a ser feita pela classe dps ela falece (Fecha a conexão)
            connection.Close();
        }
    }
}