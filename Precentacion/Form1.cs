using CRUDveterinaria.Datos;
using CRUDveterinaria.Dominio;
using CRUDveterinaria.Servicios.Interfaz;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUDveterinaria
{
    public partial class frmAltaMascota : Form
    {
        private IServicio gestor;
        private Mascota nueva;
        public frmAltaMascota()
        {
            InitializeComponent();
            gestor = new Servicio();
            nueva = new Mascota();
        }

        private void frmAltaMascota_Load(object sender, EventArgs e)
        {
            ObtenerProximo();
            CargarTipos();
            CargarClientes();
        }

        private void CargarClientes()
        {
            cboCliente.ValueMember = "IdCliente";
            cboCliente.DisplayMember = "Nombre";
            cboCliente.DataSource = gestor.ObtenerClientes();
            cboCliente.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void CargarTipos()
        {
            cboTipo.ValueMember = "IdTipo";
            cboTipo.DisplayMember = "NombreTipo";
            cboTipo.DataSource = gestor.ObtenerTipos();
            cboTipo.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void ObtenerProximo()
        {
            int next = gestor.ObtenerProximo();
            if (next > 0)
            {
                lblNro.Text = "Mascota N°: " + next.ToString();
            }
            else
            {
                MessageBox.Show("Error al obtener el número de mascota", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if(txtAtencion.Text == string.Empty)
            {
                MessageBox.Show("Ingrese una descripción de la atención", "Control", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            if(txtImporte.Text == string.Empty)
            {
                MessageBox.Show("Ingrese un importe", "Control", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            string descripcion = txtAtencion.Text;
            double importe = double.Parse(txtImporte.Text);
            DateTime fecha = dtpFecha.Value;
            Atencion a = new Atencion(descripcion, importe, fecha);

            nueva.AgregarAtencion(a);
            dgvMascotas.Rows.Add(a.Descripcion, a.Importe, a.Fecha);

            txtTotal.Text = nueva.CalcularTotal().ToString();

            LimpiarAtencion();
        }

        private void LimpiarAtencion()
        {
            txtAtencion.Text = "";
            txtImporte.Text = "";
            dtpFecha.Value = DateTime.Now;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if(txtNombre.Text == string.Empty)
            {
                MessageBox.Show("Ingrese un nombre de mascota", "Control", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            if(txtEdad.Text == string.Empty)
            {
                MessageBox.Show("Ingrese una edad de mascota", "Control", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            if(cboTipo.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar un tipo de mascota", "Control", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            if(cboCliente.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar un cliente", "Control", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            GuardarAtencion();
        }

        private void GuardarAtencion()
        {
            Cliente c = (Cliente)cboCliente.SelectedItem;
            TipoMascota t = (TipoMascota)cboTipo.SelectedItem;
            nueva.Nombre = txtNombre.Text;
            nueva.Edad = int.Parse(txtEdad.Text);
            nueva.Cliente = c;
            nueva.Tipo = t;

            if(Helper.ObtenerInstancia().ConfirmarMascota(nueva))
            {
                MessageBox.Show("Se inserto con exito la mascota", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                MessageBox.Show("No se pudo insertar la mascoota", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            LimpiarCampos();
        }

        private void LimpiarCampos()
        {
            txtAtencion.Text = "";
            txtNombre.Text = "";
            txtEdad.Text = "";
            txtImporte.Text = "";
            cboCliente.SelectedIndex = -1;
            cboTipo.SelectedIndex = -1;
        }
    }
}
