using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjetoJeffersonADM
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            string connectionString = "server=localhost;port=3306;Database=GerenciamentoDeLojasADM;uid=root;pwd='';";
            MySqlConnection connection = new MySqlConnection(connectionString);
        
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Registro());
        }
    }
}
