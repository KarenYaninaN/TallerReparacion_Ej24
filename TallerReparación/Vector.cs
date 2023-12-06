namespace TallerReparación
{
    public class Vector
    {
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
        private double acum_recaudacion;
        private double costo_repuestos;
        private double beneficios;
        private double acum_costo_garantia;

        public Vector(string evento, double reloj_sistema, double rnd_lleg_cliente, double tmp_ent_lleg,
            double prox_lleg_cliente, int cola_reparador, string estado_reparador1, string estado_reparador2,
            string estado_reparador3, double rnd_reparacion, double tmp_reparacion, double fin_reparacion1,
            double fin_reparacion2, double fin_reparacion3, double rnd_monto, double monto_cobrar, string esGratuito,
            double r_recaudacion, double r_costo_repuesto, double r_beneficio, double r_costo_garantia,
            double acum_recaudacion, double costo_repuestos, double beneficios, double acum_costo_garantia)
        {
            this.evento = evento;
            this.reloj_sistema = reloj_sistema;
            this.rnd_lleg_cliente = rnd_lleg_cliente;
            this.tmp_ent_lleg = tmp_ent_lleg;
            this.prox_lleg_cliente = prox_lleg_cliente;
            this.cola_reparador = cola_reparador;
            this.estado_reparador1 = estado_reparador1;
            this.estado_reparador2 = estado_reparador2;
            this.estado_reparador3 = estado_reparador3;
            this.rnd_reparacion = rnd_reparacion;
            this.tmp_reparacion = tmp_reparacion;
            this.fin_reparacion1 = fin_reparacion1;
            this.fin_reparacion2 = fin_reparacion2;
            this.fin_reparacion3 = fin_reparacion3;
            this.rnd_monto = rnd_monto;
            this.monto_cobrar = monto_cobrar;
            this.esGratuito = esGratuito;
            this.r_recaudacion = r_recaudacion;
            this.r_costo_repuesto = r_costo_repuesto;
            this.r_beneficio = r_beneficio; 
            this.r_costo_garantia = r_costo_garantia;
            this.acum_recaudacion = acum_recaudacion;
            this.costo_repuestos = costo_repuestos;
            this.beneficios = beneficios;
            this.acum_costo_garantia = acum_costo_garantia;
        }

        public string Evento
        {
            get { return evento; }
        }
        public double Reloj_sistema
        {
            get { return reloj_sistema; }
        }
        public double Rnd_lleg_cliente
        {
            get { return rnd_lleg_cliente; }
        }
        public double Tmp_ent_lleg
        {
            get { return tmp_ent_lleg; }
        }
        public double Prox_lleg_cliente
        {
            get { return prox_lleg_cliente; }
        }
        public double Cola_reparador
        {
            get { return cola_reparador; }
        }
        public string Estado_reparador1
        {
            get { return estado_reparador1; }
        }
        public string Estado_reparador2
        {
            get { return estado_reparador2; }
        }
        public string Estado_reparador3
        {
            get { return estado_reparador3; }
        }
        public double Rnd_reparacion
        {
            get { return rnd_reparacion; }
        }
        public double Tmp_reparacion
        {
            get { return tmp_reparacion; }
        }
        public double Fin_reparacion1
        {
            get { return fin_reparacion1; }
        }
        public double Fin_reparacion2
        {
            get { return fin_reparacion2; }
        }
        public double Fin_reparacion3
        {
            get { return fin_reparacion3; }
        }
        public double Rnd_monto
        {
            get { return rnd_monto; }
        }
        public double Monto_cobrar
        {
            get { return monto_cobrar; }
        }  
        public string EsGratuito
        {
            get { return esGratuito; }
        }
        //
        
        public double R_recaudacion
        {
            get { return r_recaudacion; }
        }
        public double R_costo_repuesto
        {
            get { return r_costo_repuesto; }
        }
        public double Acum_recaudacion
        {
            get { return acum_recaudacion; }
        }
        public double R_beneficio
        {
            get { return r_beneficio; }
        }
        //
        public double R_costo_garantia
        {
            get { return r_costo_garantia; }
        }
        public double Costo_repuestos
        {
            get { return costo_repuestos; }
        }
        public double Beneficios
        {
            get { return beneficios; }
        }
        public double Acum_costo_garantia
        {
            get { return acum_costo_garantia; }
        }
    }
}
