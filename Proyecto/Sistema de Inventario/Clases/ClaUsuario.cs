using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_de_Inventario.Clases
{
    class ClaUsuario
    {
        private int id;
        private string usuario;
        private string pws;
        private string nombre;
        public ClaUsuario()
        {
            id = 0;
            usuario = "taller";
            pws = "taller01";
            nombre = "Taller de Radiadores Montufar";
        }
        public Boolean Login(string user, string pass)
        {
            if (user == "taller" && pass == "taller01")
                return true;
            else
                return false;
        }
        public int Id
        {
            get { return id; }
        }
        public string Usuario
        {
            get
            {
                return usuario;
            }
            set
            {
                usuario = value;
            }
        }
        public string Pws
        {
            get
            {
                return pws;
            }
            set
            {
                pws = value;
            }
        }
        public string Nombre
        {
            get { return nombre; }
        }
    }
}
