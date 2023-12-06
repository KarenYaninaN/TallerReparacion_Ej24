using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace TallerReparación
{
    public class Simulacion
    {
        private Vector[] v;
        private Vector v0;
        private Vector v1;
        private double tiempo_sim;
        private int desde;
        private int hasta;
        private List<Cliente> listaClientes = new List<Cliente>();
        DataTable dtVector = new DataTable();
        public Random rnd = new Random();
        private string evento;
        private double reloj_sistema;
        private double rnd_lleg_cliente;
        private double tmp_ent_lleg;
        private double prox_lleg_cliente;
        private int cola_reparador;
        private string estado_reparador1;
        private string estado_reparador2;
        private string estado_reparador3;
        private double rnd_reparacion;
        private double tmp_reparacion;
        private double fin_reparacion1;
        private double fin_reparacion2;
        private double fin_reparacion3;
        private double rnd_monto;
        private double monto_cobrar;
        private string esGratuito;
        private double r_recaudacion;
        private double r_costo_repuesto;
        private double r_beneficio;
        private double r_costo_garantia;
        private double recaudacion;
        private double costo_repuesto;
        private double beneficio;
        private double costo_garantia;

        //ACUMULADORES
        private double acum_recaudacion;
        private double acum_costo_repuestos;
        private double acum_beneficios;
        private double acum_costo_garantia;
        private Cliente cliente = new Cliente();

        // Variables
        private double var_prox_lleg_cli;
        private double var_fin_repar1;
        private double var_fin_repar2;
        private double var_fin_repar3;

        //Eventos
        static string est_inicial = "Est. Incial";
        static string lleg_cliente = "LLeg. Cliente";
        static string fin_repar1 = "Fin Reparación (R1)";
        static string fin_repar2 = "Fin Reparación (R2)";
        static string fin_repar3 = "Fin Reparación (R3)";

        //Estados
        static string lib = "Libre";
        static string ocup = "Ocupado";
        static string no = "NO";
        static string si = "SI";

        //PARAMETROS DE DISTRIBUCIONES
        private double _lambda;
        private double _tiempoRep_desde;
        private double _tiempoRep_hasta;
        private double _monto_Desde;
        private double _monto_Hasta;
        private double _p_costo_repuesto;
        public Simulacion(double tmp_sim, int desde_sim, int hasta_sim, int lambda, int _tiemporep_desde, int _tiemporep_hasta, double _monto_desde, double _monto_hasta)
        {
            tiempo_sim = tmp_sim * 60;
            desde = desde_sim;
            hasta = hasta_sim;
            cola_reparador = 0;
            estado_reparador1 = lib;
            estado_reparador2 = lib;
            estado_reparador3 = lib;
            esGratuito = "-";
            recaudacion = 0;
            costo_repuesto = 0;
            beneficio = 0;
            costo_garantia = 0;
            _lambda = lambda;
            _tiempoRep_desde = _tiemporep_desde;
            _tiempoRep_hasta = _tiemporep_hasta;
            _monto_Desde = _monto_desde;
            _monto_Hasta = _monto_hasta;
            _p_costo_repuesto = 25;

        }

        public List<Vector> ejecutar()
        {
            Random rnd = new Random();
            reloj_sistema = 0;
            List<Vector> v = new List<Vector>();

            //Estado Inicial

            rnd_lleg_cliente = rnd.NextDouble();
            prox_lleg_cliente = tmp_ent_lleg_cliente(rnd_lleg_cliente);
            var_prox_lleg_cli = prox_lleg_cliente;
            v0 = new Vector(est_inicial, reloj_sistema, rnd_lleg_cliente, prox_lleg_cliente,
                prox_lleg_cliente, 0, estado_reparador1, estado_reparador2, estado_reparador3,
                0, 0, 0, 0, 0, 0, 0, " - ", 0, 0, 0, 0, 0, 0, 0, 0);
            v.Add(v0);

            // Lleg. Cliente
            reloj_sistema = var_prox_lleg_cli;
            obtenerProxLlegadaCliente(rnd);

            obtenerTiempoFinReparacion(rnd, 1);
            Cliente nuevoCliente = new Cliente();
            nuevoCliente.estado = "SA";
            nuevoCliente.tiempo_llegada = reloj_sistema;
            nuevoCliente.tiempo_espera_acum = 0;
            nuevoCliente.num_reparador_atendio = 1;
            listaClientes.Add(nuevoCliente);

            v1 = new Vector(lleg_cliente, reloj_sistema, rnd_lleg_cliente, tmp_ent_lleg,
              prox_lleg_cliente, cola_reparador, estado_reparador1, estado_reparador2,
              estado_reparador3, rnd_reparacion, tmp_reparacion, var_fin_repar1,
              var_fin_repar2, var_fin_repar3, rnd_monto, monto_cobrar, esGratuito,
              r_recaudacion, r_costo_repuesto, r_beneficio, r_costo_garantia,
              acum_recaudacion, acum_costo_repuestos, acum_beneficios, acum_costo_garantia);
            v.Add(v1);

            for (int i = 2; i < tiempo_sim; i++)
            {
                limpiar();

                evento = obtener_prox_evento(prox_lleg_cliente, var_fin_repar1, var_fin_repar2, var_fin_repar3);

                switch (evento)
                {
                    case "LLeg. Cliente":
                        EventoLlegadaCliente();
                        break;
                    case "Fin Reparación (R1)":
                        EventoFinReparacion();
                        break;
                    case "Fin Reparación (R2)":
                        EventoFinReparacion();
                        break;
                    case "Fin Reparación (R3)":
                        EventoFinReparacion();
                        break;
                }
                Vector vec = new Vector(evento, reloj_sistema, rnd_lleg_cliente, tmp_ent_lleg,
                  prox_lleg_cliente, cola_reparador, estado_reparador1, estado_reparador2,
                  estado_reparador3, rnd_reparacion, tmp_reparacion, var_fin_repar1,
                  var_fin_repar2, var_fin_repar3, rnd_monto, monto_cobrar, esGratuito,
                  r_recaudacion, r_costo_repuesto, r_beneficio, r_costo_garantia,
                  acum_recaudacion, acum_costo_repuestos, acum_beneficios, acum_costo_garantia);
                v.Add(vec);

                if (reloj_sistema > tiempo_sim)// Terminar (Simule el trabajo de x hs pasadas por parámetro.)
                {
                    break;
                }
            }
            return v;
        }

        //BUSQUEDA DE PRÓX EVENTO
        private string obtener_prox_evento(double proxLlegada, double finReparacion1, double finReparacion2, double finReparacion3)
        {
            List<double> valoresNoCero = new List<double>
            {
                proxLlegada,
                finReparacion1,
                finReparacion2,
                finReparacion3
            }.Where(valor => valor != 0).ToList();

            // Obtiene el mínimo de los valores no cero
            double menor = valoresNoCero.Count > 0 ? valoresNoCero.Min() : 0;

            if (menor == proxLlegada)
            {
                reloj_sistema = proxLlegada;
                evento = lleg_cliente;
            }
            if (menor == finReparacion1)
            {
                reloj_sistema = finReparacion1;
                var_fin_repar1 = reloj_sistema;
                evento = fin_repar1;
            }
            if (menor == finReparacion2)
            {
                reloj_sistema = finReparacion2;
                var_fin_repar2 = reloj_sistema;
                evento = fin_repar2;
            }
            if (menor == finReparacion3)
            {
                reloj_sistema = finReparacion3;
                var_fin_repar3 = reloj_sistema;
                evento = fin_repar3;
            }
            return evento;
        }


        //EVENTOS
        private void EventoLlegadaCliente()
        {
            reloj_sistema = var_prox_lleg_cli;
            obtenerProxLlegadaCliente(rnd);

            Cliente nuevoCliente = new Cliente();
            nuevoCliente.tiempo_llegada = reloj_sistema;
            nuevoCliente.tiempo_espera_acum = 0;

            if (estado_reparador1 == lib)
            {
                nuevoCliente.num_reparador_atendio = 1;
                obtenerTiempoFinReparacion(rnd, 1);
                nuevoCliente.estado = "SA";
                estado_reparador1 = ocup;
            }
            else
            {
                if (estado_reparador2 == lib)
                {
                    nuevoCliente.num_reparador_atendio = 2;
                    obtenerTiempoFinReparacion(rnd, 2);
                    nuevoCliente.estado = "SA";
                    estado_reparador2 = ocup;
                }
                else
                {
                    if (estado_reparador3 == lib)
                    {
                        nuevoCliente.num_reparador_atendio = 3;
                        obtenerTiempoFinReparacion(rnd, 3);
                        nuevoCliente.estado = "SA";
                        estado_reparador3 = ocup;
                    }
                    else
                    {
                        cola_reparador++;
                        nuevoCliente.estado = "EA";
                        nuevoCliente.num_reparador_atendio = 0;
                    }
                }
            }
            listaClientes.Add(nuevoCliente);
        }
        private void EventoFinReparacion()
        {
            Cliente cliente = new Cliente();
            double tiempoEspera = 0;
            if (cola_reparador == 0)
            {
                if (reloj_sistema == var_fin_repar1)
                {
                    estado_reparador1 = lib;
                    tiempoEspera = buscarClienteAtendido(1);
                    var_fin_repar1 = 0;
                }
                if (reloj_sistema == var_fin_repar2)
                {
                    estado_reparador2 = lib;
                    tiempoEspera = buscarClienteAtendido(2);
                    var_fin_repar2 = 0;
                }
                if (reloj_sistema == var_fin_repar3)
                {
                    estado_reparador3 = lib;
                    tiempoEspera = buscarClienteAtendido(3);
                    var_fin_repar3 = 0;
                }

                if (tiempoEspera > 30)
                {
                    esGratuito = si;
                    monto_cobrar = obtener_monto_cobrar(rnd);
                    double monto_No_cobrado = obtener_monto_cobrar(rnd);
                    acum_costo_repuestos += monto_cobrar * 25 / 100;
                    acum_beneficios = acum_recaudacion - acum_costo_repuestos;
                    acum_costo_garantia += monto_cobrar;
                    r_recaudacion = 0;
                    r_costo_repuesto = monto_cobrar * 25 / 100;
                    r_beneficio = r_recaudacion - r_costo_repuesto;
                    r_costo_garantia = monto_cobrar;
                }
                else
                {
                    esGratuito = no;
                    monto_cobrar = obtener_monto_cobrar(rnd);
                    acum_recaudacion += monto_cobrar;
                    acum_costo_repuestos += monto_cobrar * 25 / 100;
                    acum_beneficios = acum_recaudacion - acum_costo_repuestos;
                    acum_costo_garantia += 0;
                    r_recaudacion = monto_cobrar;
                    r_costo_repuesto = monto_cobrar * 25 / 100;
                    r_beneficio = r_recaudacion - r_costo_repuesto;
                    r_costo_garantia = 0;
                }
            }
            else
            {
                if (cola_reparador > 0)
                {
                    if (reloj_sistema == var_fin_repar1)
                    {
                        estado_reparador1 = ocup;
                        tiempoEspera = buscarClienteAtendido(1);
                        obtenerTiempoFinReparacion(rnd, 1);
                        buscarClienteEsperandoParaSerAtendido(1);
                    }
                    if (reloj_sistema == var_fin_repar2)
                    {
                        estado_reparador2 = ocup;
                        tiempoEspera = buscarClienteAtendido(2);
                        obtenerTiempoFinReparacion(rnd, 2);
                        buscarClienteEsperandoParaSerAtendido(2);

                    }
                    if (reloj_sistema == var_fin_repar3)
                    {
                        estado_reparador3 = ocup;
                        tiempoEspera = buscarClienteAtendido(3);
                        obtenerTiempoFinReparacion(rnd, 3);
                        buscarClienteEsperandoParaSerAtendido(3);
                    }
                    cola_reparador--;
                    if (tiempoEspera > 30)
                    {
                        esGratuito = si;
                        monto_cobrar = obtener_monto_cobrar(rnd);
                        double monto_No_cobrado = obtener_monto_cobrar(rnd);
                        acum_costo_repuestos += monto_cobrar * 25 / 100;
                        acum_beneficios = acum_recaudacion - acum_costo_repuestos;
                        acum_costo_garantia += monto_cobrar;
                        r_recaudacion = 0;
                        r_costo_repuesto = monto_cobrar * 25 / 100;
                        r_beneficio = r_recaudacion - r_costo_repuesto;
                        r_costo_garantia = monto_cobrar;
                    }
                    else
                    {
                        esGratuito = no;
                        monto_cobrar = obtener_monto_cobrar(rnd);
                        acum_recaudacion += monto_cobrar;
                        acum_costo_repuestos += monto_cobrar * 25 / 100;
                        acum_beneficios = acum_recaudacion - acum_costo_repuestos;
                        acum_costo_garantia += 0;
                        r_recaudacion = monto_cobrar;
                        r_costo_repuesto = monto_cobrar * 25 / 100;
                        r_beneficio = r_recaudacion - r_costo_repuesto;
                        r_costo_garantia = 0;
                    }
                }
            }
        }

        private void buscarClienteEsperandoParaSerAtendido(int numReparador)
        {
            Cliente clienteAtendidoPorReparador = listaClientes
               .Where(cliente => cliente.estado == "EA" && cliente.tiempo_espera_acum == 0)
               .OrderBy(cliente => cliente.tiempo_llegada) // Ordena por tiempo de llegada (el más antiguo primero)
               .FirstOrDefault();

            if (clienteAtendidoPorReparador != null)
            {
                clienteAtendidoPorReparador.estado = "SA";
                clienteAtendidoPorReparador.num_reparador_atendio = numReparador;
            }
        }

        private double buscarClienteAtendido(int cli)
        {
            Cliente clienteAtendidoPorReparador = listaClientes
                .Where(cliente => cliente.num_reparador_atendio == cli && cliente.tiempo_espera_acum == 0)
                .OrderBy(cliente => cliente.tiempo_llegada) // Ordena por tiempo de llegada (el más antiguo primero)
                .FirstOrDefault();

            if (clienteAtendidoPorReparador != null)
            {
                double tiempoLlegada = clienteAtendidoPorReparador.tiempo_llegada;
                double tiempoEspera = reloj_sistema - tiempoLlegada;

                // Actualiza el estado y tiempo de espera acumulado del cliente
                clienteAtendidoPorReparador.estado = "FA";
                clienteAtendidoPorReparador.tiempo_espera_acum = tiempoEspera;

                return tiempoEspera;
            }
            else { return 0; }
        }


        //CALCULOS DE TIEMPOS
        //T. Llegada
        private double tmp_ent_lleg_cliente(double rnd)
        {
            double tiempo;
            return tiempo = -1 / (_lambda / 60) * Math.Log(1 - rnd); // lambda = 7/60 (llegan siete aparatos por hora)
        }
        private void obtenerProxLlegadaCliente(Random rnd)
        {
            rnd_lleg_cliente = rnd.NextDouble();
            tmp_ent_lleg = tmp_ent_lleg_cliente(rnd_lleg_cliente);
            prox_lleg_cliente = reloj_sistema + tmp_ent_lleg;
            var_prox_lleg_cli = prox_lleg_cliente;
        }

        //T. Reparación
        private double tiempo_reparacion(double rnd)
        {
            double tiempo = _tiempoRep_desde + (_tiempoRep_hasta - _tiempoRep_desde) * (rnd);
            return tiempo;
        }
        private void obtenerTiempoFinReparacion(Random rnd, int? numReparadorAtendio)
        {
            estado_reparador1 = ocup;
            // Reparación
            rnd_reparacion = rnd.NextDouble();
            tmp_reparacion = tiempo_reparacion(rnd_reparacion);
            //Calcular Fin Reparación
            double fin_reparacion = reloj_sistema + tmp_reparacion;
            if (numReparadorAtendio == 1) { var_fin_repar1 = fin_reparacion; }
            if (numReparadorAtendio == 2) { var_fin_repar2 = fin_reparacion; }
            if (numReparadorAtendio == 3) { var_fin_repar3 = fin_reparacion; }
        }

        //CALCULOS AUX DE MÉTRICAS
        private double obtener_monto_cobrar(Random rnd)
        {
            rnd_monto = rnd.NextDouble();
            double monto_cobrar = _monto_Desde + (_monto_Hasta - _monto_Desde) * (rnd_monto);
            return monto_cobrar;
        }
        public (double, double, double, double) obtenerMetricas()
        {
            return (Math.Round(acum_beneficios, 2), Math.Round(acum_costo_garantia, 2), Math.Round(acum_costo_repuestos, 2), Math.Round(acum_recaudacion, 2));
        }

        //MOSTRAR DATOS
        public DataTable getDtLista(List<Vector> lista)
        {
            dtVector.TableName = "dtSimCola";
            dtVector.Columns.Add("Evento", typeof(string));
            dtVector.Columns.Add("Reloj Sistema", typeof(double));
            dtVector.Columns.Add("RND LLeg. CLiente", typeof(double));
            dtVector.Columns.Add("Tiempo entre llegada Cliente", typeof(double));
            dtVector.Columns.Add("Próxima llegada Cliente", typeof(double));
            dtVector.Columns.Add("Cola Reparadores", typeof(int));
            dtVector.Columns.Add("Estado R1", typeof(string));
            dtVector.Columns.Add("Estado R2", typeof(string));
            dtVector.Columns.Add("Estado R3", typeof(string));
            dtVector.Columns.Add("RND Tiempo Reparación", typeof(double));
            dtVector.Columns.Add("Tiempo Reparación", typeof(double));
            dtVector.Columns.Add("Fin Reparación 1", typeof(double));
            dtVector.Columns.Add("Fin Reparación 2", typeof(double));
            dtVector.Columns.Add("Fin Reparación 3", typeof(double));
            dtVector.Columns.Add("RND Cobro", typeof(double));
            dtVector.Columns.Add("Monto a Cobrar", typeof(double));
            dtVector.Columns.Add("¿Es Gratuito?", typeof(string));
            dtVector.Columns.Add("Recaudación", typeof(double));
            dtVector.Columns.Add("Costo de Repuestos (25%)", typeof(double));
            dtVector.Columns.Add("Beneficio", typeof(double));
            dtVector.Columns.Add("Costo de Garantía", typeof(double));
            dtVector.Columns.Add("Acum de Recaudación", typeof(double));
            dtVector.Columns.Add("Acum Costo de Repuestos", typeof(double));
            dtVector.Columns.Add("Acum Beneficios", typeof(double));
            dtVector.Columns.Add("Acum Costo de Garantía", typeof(double));

            if (desde == 0 && hasta == 0)
            {
                foreach (Vector v0 in lista)
                {
                    if (v0 != null)
                    {
                        DataRow dr = CreateDataRowFromVector(v0);
                        dtVector.Rows.Add(dr);
                    }
                }
            }
            else
            {
                if (desde > 0)
                {
                    Vector primerElemento = lista[0];

                    if (primerElemento != null)
                    {
                        DataRow dr = CreateDataRowFromVector(primerElemento);
                        dtVector.Rows.InsertAt(dr, 0);  // Insertar en el primer lugar
                    }
                }

                for (int i = desde; i <= hasta && i < lista.Count; i++)
                {
                    Vector v0 = lista[i];
                    if (v0 != null)
                    {
                        DataRow dr = CreateDataRowFromVector(v0);
                        dtVector.Rows.Add(dr);
                    }
                }
                if (hasta < lista.Count)
                {
                    // Si el índice HASTA es mayor o igual al tamaño de la lista,
                    // agrega el último elemento de la lista a dtVector.
                    Vector ultimoElemento = lista[lista.Count - 1];

                    if (ultimoElemento != null)
                    {
                        DataRow dr = CreateDataRowFromVector(ultimoElemento);
                        dtVector.Rows.Add(dr);
                    }
                }
            }
            return dtVector;
        }
        private DataRow CreateDataRowFromVector(Vector v0)
        {
            DataRow dr = dtVector.NewRow();
            dr["Evento"] = v0.Evento;
            dr["Reloj Sistema"] = Math.Round(v0.Reloj_sistema, 2);
            dr["RND LLeg. CLiente"] = Math.Round(v0.Rnd_lleg_cliente, 4);
            dr["Tiempo entre llegada Cliente"] = Math.Round(v0.Tmp_ent_lleg, 2);
            dr["Próxima llegada Cliente"] = Math.Round(v0.Prox_lleg_cliente, 2);
            dr["Cola Reparadores"] = v0.Cola_reparador;
            dr["Estado R1"] = v0.Estado_reparador1;
            dr["Estado R2"] = v0.Estado_reparador2;
            dr["Estado R3"] = v0.Estado_reparador3;
            dr["RND Tiempo Reparación"] = Math.Round(v0.Rnd_reparacion, 4);
            dr["Tiempo Reparación"] = Math.Round(v0.Tmp_reparacion, 2);
            dr["Fin Reparación 1"] = Math.Round(v0.Fin_reparacion1, 2);
            dr["Fin Reparación 2"] = Math.Round(v0.Fin_reparacion2, 2);
            dr["Fin Reparación 3"] = Math.Round(v0.Fin_reparacion3, 2);
            dr["RND Cobro"] = Math.Round(v0.Rnd_monto, 4);
            dr["Monto a Cobrar"] = Math.Round(v0.Monto_cobrar, 2);
            dr["¿Es Gratuito?"] = v0.EsGratuito;
            dr["Recaudación"] = Math.Round(v0.R_recaudacion, 2);
            dr["Costo de Repuestos (25%)"] = Math.Round(v0.R_costo_repuesto, 2);
            dr["Beneficio"] = Math.Round(v0.R_beneficio, 2);
            dr["Costo de Garantía"] = Math.Round(v0.R_costo_garantia, 2);
            dr["Acum de Recaudación"] = Math.Round(v0.Acum_recaudacion, 2);
            dr["Acum Costo de Repuestos"] = Math.Round(v0.Costo_repuestos, 2);
            dr["Acum Beneficios"] = Math.Round(v0.Beneficios, 2);
            dr["Acum Costo de Garantía"] = Math.Round(v0.Acum_costo_garantia, 2);
            return dr;
        }
        public List<Cliente> obtenerListClientes()
        {
            return listaClientes;
        }
        //OTROS 
        public void limpiar()
        {
            rnd_lleg_cliente = 0;
            tmp_ent_lleg = 0;
            rnd_reparacion = 0;
            tmp_reparacion = 0;
            rnd_monto = 0;
            monto_cobrar = 0;
            esGratuito = string.Empty;
            _lambda = 7;
            _tiempoRep_desde = 13;
            _tiempoRep_hasta = 17;
        }
    }
}
