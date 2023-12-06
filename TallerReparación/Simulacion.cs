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
        private double fin_reparacion1;//ver
        private double fin_reparacion2;
        private double fin_reparacion3;
        private double rnd_monto;
        private double monto_cobrar;
        private string esGratuito;
        // 
        private double r_recaudacion;
        private double r_costo_repuesto;
        private double r_beneficio;
        private double r_costo_garantia;
        //

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
        public Simulacion(double tmp_sim, int desde_sim, int hasta_sim)
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
        }

        public Vector[] ejecutar()
        {
            Random rnd = new Random();
            reloj_sistema = 0;
            v = new Vector[(int)tiempo_sim];

            //Estado Inicial

            rnd_lleg_cliente = rnd.NextDouble();
            //rnd_lleg_cliente = 0.26;
            prox_lleg_cliente = tmp_ent_lleg_cliente(rnd_lleg_cliente);
            var_prox_lleg_cli = prox_lleg_cliente;
            v0 = new Vector(est_inicial, reloj_sistema, rnd_lleg_cliente, prox_lleg_cliente,
                prox_lleg_cliente, 0, estado_reparador1, estado_reparador2, estado_reparador3,
                0, 0, 0, 0, 0, 0, 0, " - ",0,0,0,0, 0, 0, 0, 0);

            v[0] = v0; //inserto la linea en cero


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
              r_recaudacion, r_costo_repuesto, r_beneficio,r_costo_garantia,
              acum_recaudacion, acum_costo_repuestos, acum_beneficios, acum_costo_garantia);


            v[1] = v1;

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

                v[i] = vec;
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
            //nuevoCliente.estado = "SA";
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
                        //TODOS OCUPADOS, creo ese Cliente y lo sumo a la lista pero en estado de Espera
                        cola_reparador++;
                        nuevoCliente.estado = "EA";
                        nuevoCliente.num_reparador_atendio = 0;
                    }
                }
            }
            // Actualizar lista de clientes
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
                    monto_cobrar = obtener_monto_cobrar(rnd); //DUDA, estaba en 0
                    double monto_No_cobrado = obtener_monto_cobrar(rnd);
                    acum_costo_repuestos += monto_cobrar * 25 / 100;
                    acum_beneficios = acum_recaudacion - acum_costo_repuestos;
                    acum_costo_garantia += monto_cobrar;//DUDA SI ES monto_cobrar
                    //
                    r_recaudacion =0; 
                    r_costo_repuesto = monto_cobrar * 25 / 100;
                    r_beneficio = r_recaudacion - r_costo_repuesto;
                    //if (r_recaudacion > 0)
                    //{
                    //    r_beneficio = r_recaudacion - r_costo_repuesto;
                    //}
                    //else 
                    //{
                    //    r_beneficio = 0;
                    //}
                   
                    r_costo_garantia = monto_cobrar;
                    //
                }
                else
                {
                    esGratuito = no;
                    monto_cobrar = obtener_monto_cobrar(rnd);
                    acum_recaudacion += monto_cobrar;
                    acum_costo_repuestos += monto_cobrar * 25 / 100;
                    acum_beneficios = acum_recaudacion - acum_costo_repuestos;
                    acum_costo_garantia += 0;
                    //
                    r_recaudacion = monto_cobrar;
                    r_costo_repuesto = monto_cobrar * 25 / 100;
                    r_beneficio = r_recaudacion - r_costo_repuesto;
                    //if (r_recaudacion > 0)
                    //{
                    //    r_beneficio = r_recaudacion - r_costo_repuesto;
                    //}
                    //else
                    //{
                    //    r_beneficio = 0;
                    //}

                    r_costo_garantia = 0;
                    //
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

                    //Ver si como la cola tiene Clientes, al liberarse un reparador, este pasa a atenderlo, por lo que se debe buscar al
                    // cliente más antiguo de la cola que esta en estado "EA", como ya existe hay que cambiarle su estado a "SA" y seterale 
                    // el número de reparador que le está atendiendo, LUEGO SI REDUIR LA COLA
                    cola_reparador--;
                    if (tiempoEspera > 30)
                    {
                        esGratuito = si;
                        monto_cobrar = obtener_monto_cobrar(rnd);
                        double monto_No_cobrado = obtener_monto_cobrar(rnd);
                        acum_costo_repuestos += monto_cobrar * 25 / 100;
                        acum_beneficios = acum_recaudacion - acum_costo_repuestos;
                        acum_costo_garantia += monto_cobrar;
                        //
                        r_recaudacion = 0;
                        r_costo_repuesto = monto_cobrar * 25 / 100;
                        r_beneficio = r_recaudacion - r_costo_repuesto;
                        //if (r_recaudacion > 0)
                        //{
                        //    r_beneficio = r_recaudacion - r_costo_repuesto;
                        //}
                        //else
                        //{
                        //    r_beneficio = 0;
                        //}

                        r_costo_garantia = monto_cobrar;
                        //

                    }
                    else
                    {
                        esGratuito = no;
                        monto_cobrar = obtener_monto_cobrar(rnd);
                        acum_recaudacion += monto_cobrar;
                        acum_costo_repuestos += monto_cobrar * 25 / 100;
                        acum_beneficios = acum_recaudacion - acum_costo_repuestos;
                        acum_costo_garantia += 0;
                        //
                        r_recaudacion = monto_cobrar;
                        r_costo_repuesto = monto_cobrar * 25 / 100;
                        r_beneficio = r_recaudacion - r_costo_repuesto;
                        //if (r_recaudacion > 0)
                        //{
                        //    r_beneficio = r_recaudacion - r_costo_repuesto;
                        //}
                        //else
                        //{
                        //    r_beneficio = 0;
                        //}

                        r_costo_garantia = 0;
                        //
                    }

                }

            }

            //if (cola_reparador == 0)
            //{
            //    estado_reparador1 = lib;
            //}
            //else
            //{
            //    cola_reparador--;
            //    //reparacion(rnd);
            //}
        }

        private void buscarClienteEsperandoParaSerAtendido(int numReparador)
        {
            //Cliente clienteAtendidoPorReparador = listaClientes.Where(cliente => cliente.estado == "EA");
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
            //var clienteAtendidoPorReparador = listaClientes.Where(cliente => cliente.num_reparador_atendio == cli);// && cliente == cli.tiempo_espera_acum == 0
            ////if (clienteAtendidoPorReparador.Any())
            ////{
            ////    double tiempoLlegada = clienteAtendidoPorReparador.First().tiempo_llegada;
            ////}

            //double tiempoLlegada = clienteAtendidoPorReparador.First().tiempo_llegada;
            //double tiempoEspera = reloj_sistema - tiempoLlegada;
            //clienteAtendidoPorReparador.First().estado = "FA";
            //clienteAtendidoPorReparador.First().tiempo_espera_acum = tiempoEspera;
            //return tiempoEspera;
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
            return tiempo = -1 / (7.0 / 60) * Math.Log(1 - rnd); // lambda = 7/60 (llegan siete aparatos por hora)
        }
        private void obtenerProxLlegadaCliente(Random rnd)
        {
            rnd_lleg_cliente = rnd.NextDouble();
            //rnd_lleg_cliente = 0.84;
            tmp_ent_lleg = tmp_ent_lleg_cliente(rnd_lleg_cliente);

            prox_lleg_cliente = reloj_sistema + tmp_ent_lleg;

            var_prox_lleg_cli = prox_lleg_cliente;

            //if (estado_reparador1 == ocup && estado_reparador2 == ocup && estado_reparador3 == ocup)
            //{
            //    cola_reparador++;
            //}
        }

        //T. Reparación
        private double tiempo_reparacion(double rnd)
        {

            return 13 + (17 - 13) * (rnd);
        }
        private void obtenerTiempoFinReparacion(Random rnd, int? numReparadorAtendio)
        {

            estado_reparador1 = ocup;

            // Reparación
            rnd_reparacion = rnd.NextDouble();
            //rnd_reparacion = 0.49;
            tmp_reparacion = tiempo_reparacion(rnd_reparacion);

            //Calcular Fin Reparación
            double fin_reparacion = reloj_sistema + tmp_reparacion;
            if (numReparadorAtendio == 1) { var_fin_repar1 = fin_reparacion; }
            if (numReparadorAtendio == 2) { var_fin_repar2 = fin_reparacion; }
            if (numReparadorAtendio == 3) { var_fin_repar3 = fin_reparacion; }


        }

        //CALCULO DE MÉTRICAS
        private void reparacion(Random rnd)
        {
            rnd_reparacion = rnd.Next(100);
            tmp_reparacion = tiempo_reparacion(rnd_reparacion);
            fin_reparacion1 = reloj_sistema + tmp_reparacion;
            var_fin_repar1 = fin_reparacion1;
            // FALTA PARA LOS DEMÁS REPARADORES 
        }

        //private void actualizar_estado_reparador()
        //{
        //    if (cola_reparador > 0)
        //    {
        //        cola_reparador--;
        //    }
        //    else if (cola_reparador == 0)
        //    {
        //        estado_reparador1 = lib;
        //        //FALTA PARA DEMÁS REPARADORES
        //        //estado_reparador2 = lib;
        //        //estado_reparador3 = lib;
        //    }
        //}

        private double obtener_monto_cobrar(Random rnd)
        {
            rnd_monto = rnd.NextDouble();
            //rnd_monto = 0.32;
            return 100 + (400 - 100) * (rnd_monto);
        }
        public (double, double, double, double) obtenerMetricas()
        {
            return (Math.Round(acum_beneficios, 2), Math.Round(acum_costo_garantia, 2), Math.Round(acum_costo_repuestos, 2), Math.Round(acum_recaudacion, 2));
        }
        //MOSTRAR DATOS
        public DataTable getDtVector(Vector[] vec)
        {
            DataTable dtVector = new DataTable();

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
            dtVector.Columns.Add("Acum Costo de Repuestos (25%)", typeof(double));
            dtVector.Columns.Add("Acum Beneficios", typeof(double));
            dtVector.Columns.Add("Acum Costo de Garantía", typeof(double));

            for (int i = 0; i < v.Length; i++)
            {
                if (v[i] != null)
                {
                    v0 = v[i];
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
                    //
                    dr["Recaudación"] = Math.Round(v0.R_recaudacion, 2);
                    dr["Costo de Repuestos (25%)"] = Math.Round(v0.R_costo_repuesto, 2);
                    dr["Beneficio"] = Math.Round(v0.R_beneficio, 2);
                    dr["Costo de Garantía"] = Math.Round(v0.R_costo_garantia, 2);
                    //Acumuladores
                    dr["Acum de Recaudación"] = Math.Round(v0.Acum_recaudacion, 2);
                    dr["Acum Costo de Repuestos (25%)"] = Math.Round(v0.Costo_repuestos, 2);
                    dr["Acum Beneficios"] = Math.Round(v0.Beneficios, 2);
                    dr["Acum Costo de Garantía"] = Math.Round(v0.Acum_costo_garantia, 2);

                   
                    // VER que tambien aparezcan las columnas con los atributos de cada cliente!!!
                    //foreach (string cliente in listaClientes)
                    //{
                    //    dr["Estado"] = v0.estadoCliente1;
                    //    dr["Tiempo Llegada"] = v0.Prox_lleg_cliente;
                    //dr["Tiempo de Espera Acum"] = v0.Reloj_sistema - v0.Prox_lleg_cliente;
                    //dr["Atendido por"] = v0.Atendido_por_Reparador;
                    //
                    //}

                    dtVector.Rows.Add(dr);
                }
            }

            return dtVector;
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
        }

    }

}
