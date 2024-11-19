using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CRUDProductos
{
    public class Producto
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
    }

    public class Productos
    {
        public DataTable Lista { get; set; }

        public Productos()
        {
            Lista = new DataTable();
            Lista.TableName = "Productos";
            Lista.Columns.Add("ID", typeof(int));
            Lista.Columns.Add("Nombre");
            Lista.Columns.Add("Precio", typeof(decimal));
            Lista.Columns.Add("Stock", typeof(int));

            LeerArchivo();
        }

        private void LeerArchivo()
        {
            if (System.IO.File.Exists("Productos.xml"))
            {
                Lista.ReadXml("Productos.xml");
            }
        }

        public void GuardarArchivo()
        {
            Lista.WriteXml("Productos.xml");
        }

        public void Insert(Producto producto)
        {
            if (ExisteProducto(producto.ID))
            {
                throw new Exception("Ya existe un producto con este ID.");
            }

            Lista.Rows.Add(producto.ID, producto.Nombre, producto.Precio, producto.Stock);
            GuardarArchivo();
        }

        public void Update(Producto producto)
        {
            var fila = Lista.AsEnumerable().FirstOrDefault(row => Convert.ToInt32(row["ID"]) == producto.ID);
            if (fila != null)
            {
                fila["Nombre"] = producto.Nombre;
                fila["Precio"] = producto.Precio;
                fila["Stock"] = producto.Stock;
                GuardarArchivo();
            }
            else
            {
                throw new Exception("El producto no existe.");
            }
        }

        public void Delete(int id)
        {
            var fila = Lista.AsEnumerable().FirstOrDefault(row => Convert.ToInt32(row["ID"]) == id);
            if (fila != null)
            {
                Lista.Rows.Remove(fila);
                GuardarArchivo();
            }
            else
            {
                throw new Exception("El producto no existe.");
            }
        }

        public bool ExisteProducto(int id)
        {
            return Lista.AsEnumerable().Any(row => Convert.ToInt32(row["ID"]) == id);
        }

        public DataTable GetAll()
        {
            return Lista;
        }
    }
}