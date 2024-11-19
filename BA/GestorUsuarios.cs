using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CRUDProductos
{
    public class Usuario
    {
        public string NombreUsuario { get; set; }
        public string Contraseña { get; set; }
    }

    public class Usuarios
    {
        public DataTable Lista { get; set; }

        public Usuarios()
        {
            Lista = new DataTable();
            Lista.TableName = "Usuarios";
            Lista.Columns.Add("NombreUsuario");
            Lista.Columns.Add("Contraseña");

            LeerArchivo();
        }

        private void LeerArchivo()
        {
            if (System.IO.File.Exists("Usuarios.xml"))
            {
                Lista.ReadXml("Usuarios.xml");
            }
        }

        public void GuardarArchivo()
        {
            Lista.WriteXml("Usuarios.xml");
        }

        public void RegistrarUsuario(Usuario usuario)
        {
            if (Lista.Select($"NombreUsuario = '{usuario.NombreUsuario}'").Length > 0)
            {
                throw new Exception("El usuario ya existe.");
            }

            Lista.Rows.Add(usuario.NombreUsuario, usuario.Contraseña);
            GuardarArchivo();
        }

        public bool ValidarUsuario(string nombreUsuario, string contraseña)
        {
            var usuario = Lista.Select($"NombreUsuario = '{nombreUsuario}' AND Contraseña = '{contraseña}'");
            return usuario.Length > 0;
        }
    }
}