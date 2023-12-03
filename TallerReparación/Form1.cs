using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TallerReparación
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnSimular_Click(object sender, EventArgs e)
        {
            int desde = 0;
            int hasta = 0;
            double beneficio;
            double costoGarantia;
            double costoRepuesto;
            double recaudacion;



            if (!string.IsNullOrEmpty(txtTiempoSim.Text) && ( !string.IsNullOrEmpty(textDesde.Text) || string.IsNullOrEmpty(textDesde.Text)) && (!string.IsNullOrEmpty(textHasta.Text) || string.IsNullOrEmpty(textHasta.Text)))
            {
                if ((double.TryParse(txtTiempoSim.Text, out double tiempoSim) || int.TryParse(txtTiempoSim.Text, out int tiempoSimEntero)) &&
                    (double.TryParse(textDesde.Text, out double desdeSim) || int.TryParse(textDesde.Text, out int desdeSimEntero) || textDesde.Text == "") &&
                    (double.TryParse(textHasta.Text, out double hastaSim) || int.TryParse(textHasta.Text, out int hastaSimEntero) || textHasta.Text == ""))
                {
                    double tiempo_sim = double.Parse(txtTiempoSim.Text);
                    if (textDesde.Text == "" || textHasta.Text == "")
                    {
                       desde = 0;
                       hasta = 0;
                    }
                    else
                    {
                        desde = int.Parse(textDesde.Text);
                        hasta = int.Parse(textHasta.Text);
                        if (desde >= hasta)
                        {
                            MessageBox.Show("ERROR, el DESDE debe ser menos que el HASTA");
                            textDesde.Clear();
                            textHasta.Clear();
                            textDesde.Focus();
                            return;
                        }

                    }
                    // El valor ingresado es un número (double o int)
                    Simulacion sim = new Simulacion(tiempo_sim, desde, hasta);
                    Vector[] vec = sim.ejecutar();

                   
                    //colorear columnas
                    dgvSimular.DataSource = sim.getDtVector(vec);
                    dgvSimular.Columns[1].DefaultCellStyle.BackColor = Color.Yellow;
                    dgvSimular.Columns[4].DefaultCellStyle.BackColor = Color.Yellow;
                    dgvSimular.Columns[11].DefaultCellStyle.BackColor = Color.Yellow;
                    dgvSimular.Columns[12].DefaultCellStyle.BackColor = Color.Yellow;
                    dgvSimular.Columns[13].DefaultCellStyle.BackColor = Color.Yellow;
                    dgvSimular.Columns[16].DefaultCellStyle.BackColor = Color.LightGreen;
                    dgvSimular.Columns[17].DefaultCellStyle.BackColor = Color.Transparent;
                    dgvSimular.Columns[18].DefaultCellStyle.BackColor = Color.Transparent;
                    dgvSimular.Columns[19].DefaultCellStyle.BackColor = Color.Transparent;
                    dgvSimular.Columns[20].DefaultCellStyle.BackColor = Color.Transparent;
                    //Mostrar Métricas
                    (beneficio, costoGarantia, costoRepuesto, recaudacion) = sim.obtenerMetricas();
                    textBeneficios.Text = beneficio.ToString();
                    textCostoGarantia.Text = costoGarantia.ToString();
                    textCostoRepuesto.Text = costoRepuesto.ToString();
                    textRecaudacion.Text = recaudacion.ToString();
                    textBeneficios.Enabled = false;
                    textCostoGarantia.Enabled = false;
                    textCostoRepuesto.Enabled = false;
                    textRecaudacion.Enabled = false;
                    txtTiempoSim.Enabled = false;
                    textDesde.Enabled = false;
                    textHasta.Enabled = false;
                }
                else
                {
                    // El valor ingresado no es un número válido
                    MessageBox.Show("Los valores ingresados deben ser NÚMERICOS");
                    txtTiempoSim.Clear();
                    textDesde.Clear();
                    textHasta.Clear();
                    textDesde.Focus();
                    return;
                }
            }
            else
            {
                if (string.IsNullOrEmpty(txtTiempoSim.Text))
                {
                    // El cuadro de texto de tiempo de simulación está vacío
                    MessageBox.Show("Ingrese la cantidad de horas a simular");
                    txtTiempoSim.Clear();
                    textDesde.Clear();
                    textHasta.Clear();
                    textDesde.Focus();
                    return;

                }
            }

        }

        private void limpiar_Click(object sender, EventArgs e)
        {
            textHasta.Clear();
            textDesde.Clear();
            txtTiempoSim.Clear();
            textBeneficios.Clear();
            textCostoGarantia.Clear();
            textCostoRepuesto.Clear();
            textRecaudacion.Clear();
            txtTiempoSim.Enabled = true;
            textDesde.Enabled = true;
            textHasta.Enabled = true;
            if (dgvSimular.DataSource is DataTable dataTable) 
            {
                dataTable.Rows.Clear();
                dataTable.AcceptChanges();

                dgvSimular.Columns[1].DefaultCellStyle.BackColor = Color.White;
                dgvSimular.Columns[4].DefaultCellStyle.BackColor = Color.White;
                dgvSimular.Columns[11].DefaultCellStyle.BackColor = Color.White;
                dgvSimular.Columns[12].DefaultCellStyle.BackColor = Color.White;
                dgvSimular.Columns[13].DefaultCellStyle.BackColor = Color.White;
                dgvSimular.Columns[16].DefaultCellStyle.BackColor = Color.White;
                dgvSimular.Columns[17].DefaultCellStyle.BackColor = Color.White;
                dgvSimular.Columns[18].DefaultCellStyle.BackColor = Color.White;
                dgvSimular.Columns[19].DefaultCellStyle.BackColor = Color.White;
                dgvSimular.Columns[20].DefaultCellStyle.BackColor = Color.White;
            }

        }
    }
}
