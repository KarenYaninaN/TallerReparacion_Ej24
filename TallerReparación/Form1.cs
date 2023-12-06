using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;

using System.Windows.Forms;

namespace TallerReparación
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            dgvSimular.RowPrePaint += dgvSimular_RowPrePaint;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnSimular_Click(object sender, EventArgs e)
        {
            int desde = 0;
            int hasta = 0;
            int lambda = 7;
            int _tiemporep_hasta = 17;
            int _tiemporep_desde = 13;
            double beneficio;
            double costoGarantia;
            double costoRepuesto;
            double recaudacion;
            double _monto_desde = 100;
            double _monto_hasta = 400;
            double _p_costo_Repuesto = 25;



            if (!string.IsNullOrEmpty(txtTiempoSim.Text) && ( !string.IsNullOrEmpty(textDesde.Text) || string.IsNullOrEmpty(textDesde.Text)) && (!string.IsNullOrEmpty(textHasta.Text) || string.IsNullOrEmpty(textHasta.Text)))
            {
                if ((double.TryParse(txtTiempoSim.Text, out double tiempoSim) || int.TryParse(txtTiempoSim.Text, out int tiempoSimEntero)) &&
                    (double.TryParse(textDesde.Text, out double desdeSim) || int.TryParse(textDesde.Text, out int desdeSimEntero) || textDesde.Text == "") &&
                    (double.TryParse(textHasta.Text, out double hastaSim) || int.TryParse(textHasta.Text, out int hastaSimEntero) || textHasta.Text == ""))
                {
                    double tiempo_sim = double.Parse(txtTiempoSim.Text);
                    //Validaciones lambda
                    if (txtLambda.Text != "")
                    {
                        if (int.TryParse(txtLambda.Text, out int lambdaValue) && lambdaValue > 0)
                        {
                            lambda = lambdaValue;
                        }
                        else
                        {
                            MessageBox.Show("La cantidad de clientes con aparatos a reparar debe ser un valor MAYOR A CERO Y ENTERO");
                            txtLambda.Text = "7";
                        }
                    }
                    else
                    {
                        txtLambda.Text = "7";
                    }
                    //Validaciones Tiempo Desde y Tiempo Hasta
                    if (txt_tiemporep_desde.Text != "" && txt_tiemporep_hasta.Text != "")
                    {
                        if (int.Parse(txt_tiemporep_desde.Text) > 0 && int.Parse(txt_tiemporep_hasta.Text) > 0)
                        {
                            _tiemporep_desde = int.Parse(txt_tiemporep_desde.Text);
                            _tiemporep_hasta= int.Parse(txt_tiemporep_hasta.Text);
                        }
                        else
                        {
                            MessageBox.Show("El tiempo ingresado de reparación debe ser mayor a cero y positivo");
                            txt_tiemporep_desde.Text = "13";
                            txt_tiemporep_hasta.Text = "17";
                        }
                        if (int.Parse(txt_tiemporep_desde.Text) > int.Parse(txt_tiemporep_hasta.Text))
                        {
                            MessageBox.Show("ERROR, el Tiempo de Reparación DESDE debe ser menor que el tiempo HASTA");
                            txt_tiemporep_desde.Text = "13";
                            txt_tiemporep_hasta.Text = "17";
                        }
                    }
                    else 
                    {
                        txt_tiemporep_desde.Text = "13";
                        txt_tiemporep_hasta.Text = "17";
                    }
                    // Validaciones de Monto Desde y Monto Hasta
                    if (text_monto_desde.Text != "" && text_monto_hasta.Text != "")
                    {
                        if (int.Parse(text_monto_desde.Text) > 0 && int.Parse(text_monto_hasta.Text) > 0)
                        {
                            _monto_desde = double.Parse(text_monto_desde.Text);
                            _monto_hasta = double.Parse(text_monto_hasta.Text);
                        }
                        else
                        {
                            MessageBox.Show("El valor del monto a cobrar Desde y Hasta deben  ser mayores a cero y positivos");
                            text_monto_desde.Text = "100";
                            text_monto_hasta.Text = "400";
                        }
                        if (int.Parse(text_monto_desde.Text) > int.Parse(text_monto_hasta.Text))
                        {
                            MessageBox.Show("ERROR, el Monto a Cobrar DESDE debe ser menor que el monto HASTA");
                            text_monto_desde.Text = "100";
                            text_monto_hasta.Text = "400";
                        }
                    }
                    else
                    {
                        text_monto_desde.Text = "100";
                        text_monto_hasta.Text = "400";
                    }
                    // Validación de Rango de Renglones a Mostrar Desde - Hasta
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
                            MessageBox.Show("ERROR, el DESDE debe ser menor que el tiempo HASTA");
                            textDesde.Clear();
                            textHasta.Clear();
                            textDesde.Focus();
                            return;
                        }

                    }
                    //Validaciones Porcentaje % Costo de Respuestos
                    if (txt_porc_costo_rep.Text != "")
                    {
                        if (int.TryParse(txt_porc_costo_rep.Text, out int porcentRepValue) && porcentRepValue > 0)
                        {
                            _p_costo_Repuesto = porcentRepValue;
                        }
                        else
                        {
                            MessageBox.Show("El porcentaje de Costo de Repuesto debe ser un valor POSITIVO y MAYOR A CERO ");
                            txt_porc_costo_rep.Text = "25";
                        }
                    }
                    else
                    {
                        txt_porc_costo_rep.Text = "25";
                    }
                    Simulacion sim = new Simulacion(tiempo_sim, desde, hasta, lambda, _tiemporep_desde, _tiemporep_hasta, _monto_desde, _monto_hasta, _p_costo_Repuesto);
                    List<Vector> vec = sim.ejecutar();
                    dgvClientes.DataSource = sim.obtenerListClientes();
                    
                   
                    //colorear columnas
                    dgvSimular.DataSource = sim.getDtLista(vec);
                    dgvSimular.Columns[1].DefaultCellStyle.BackColor = Color.Yellow;
                    dgvSimular.Columns[4].DefaultCellStyle.BackColor = Color.Yellow;
                    dgvSimular.Columns[11].DefaultCellStyle.BackColor = Color.Yellow;
                    dgvSimular.Columns[12].DefaultCellStyle.BackColor = Color.Yellow;
                    dgvSimular.Columns[13].DefaultCellStyle.BackColor = Color.Yellow;
                    dgvSimular.Columns[16].DefaultCellStyle.BackColor = Color.LightSeaGreen;
                    dgvSimular.Columns[17].DefaultCellStyle.BackColor = Color.Azure;
                    dgvSimular.Columns[18].DefaultCellStyle.BackColor = Color.Azure;
                    dgvSimular.Columns[19].DefaultCellStyle.BackColor = Color.Azure;
                    dgvSimular.Columns[20].DefaultCellStyle.BackColor = Color.Azure;
                    dgvSimular.Columns[21].DefaultCellStyle.BackColor = Color.Transparent;
                    dgvSimular.Columns[22].DefaultCellStyle.BackColor = Color.Transparent;
                    dgvSimular.Columns[23].DefaultCellStyle.BackColor = Color.Transparent;
                    dgvSimular.Columns[24].DefaultCellStyle.BackColor = Color.Transparent;


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
                    txtLambda.Enabled =false;
                    txt_tiemporep_desde.Enabled = false;
                    txt_tiemporep_hasta.Enabled = false;
                    text_monto_desde.Enabled = false;
                    text_monto_hasta.Enabled = false;
                    txt_porc_costo_rep.Enabled = false;
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
            txtLambda.Clear();
            txt_tiemporep_desde.Clear();
            txt_tiemporep_hasta.Clear();
            text_monto_desde.Clear();
            text_monto_hasta.Clear();
            txt_porc_costo_rep.Clear();

            txtTiempoSim.Enabled = true;
            textDesde.Enabled = true;
            textHasta.Enabled = true;
            txtLambda.Enabled = true;
            txt_tiemporep_desde.Enabled = true;
            txt_tiemporep_hasta.Enabled = true;
            text_monto_desde.Enabled = true;
            text_monto_hasta.Enabled = true;
            txt_porc_costo_rep.Enabled = true;

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
                dgvSimular.Columns[21].DefaultCellStyle.BackColor = Color.White;
                dgvSimular.Columns[22].DefaultCellStyle.BackColor = Color.White;
                dgvSimular.Columns[23].DefaultCellStyle.BackColor = Color.White;
                dgvSimular.Columns[24].DefaultCellStyle.BackColor = Color.White;

            }
            dgvClientes.Columns.Clear();

        }
        // Colorea la última simulación
        private void dgvSimular_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            // Obtengo la referencia a la fila actual
            DataGridViewRow row = dgvSimular.Rows[e.RowIndex];
            DataRowView dataRowView = (DataRowView)row.DataBoundItem;

            bool esAnteUltimaFila = e.RowIndex == dgvSimular.Rows.Count - 2;
            bool esUltimaFila = e.RowIndex == dgvSimular.Rows.Count - 1;
            if (esAnteUltimaFila)
            {
                row.DefaultCellStyle.BackColor = Color.LightBlue;
            }
            if (esUltimaFila)
            {
                row.DefaultCellStyle.BackColor = Color.White;
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
