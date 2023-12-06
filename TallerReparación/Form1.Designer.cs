namespace TallerReparación
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgvSimular = new System.Windows.Forms.DataGridView();
            this.btnSimular = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTiempoSim = new System.Windows.Forms.TextBox();
            this.btnSalir = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textDesde = new System.Windows.Forms.TextBox();
            this.textHasta = new System.Windows.Forms.TextBox();
            this.textRecaudacion = new System.Windows.Forms.TextBox();
            this.textCostoRepuesto = new System.Windows.Forms.TextBox();
            this.textBeneficios = new System.Windows.Forms.TextBox();
            this.textCostoGarantia = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.limpiar = new System.Windows.Forms.Button();
            this.dgvClientes = new System.Windows.Forms.DataGridView();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Clientes = new System.Windows.Forms.TabControl();
            this.cli = new System.Windows.Forms.TabPage();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSimular)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvClientes)).BeginInit();
            this.Clientes.SuspendLayout();
            this.cli.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvSimular
            // 
            this.dgvSimular.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSimular.GridColor = System.Drawing.SystemColors.ActiveCaption;
            this.dgvSimular.Location = new System.Drawing.Point(6, 5);
            this.dgvSimular.Name = "dgvSimular";
            this.dgvSimular.Size = new System.Drawing.Size(846, 545);
            this.dgvSimular.TabIndex = 0;
            // 
            // btnSimular
            // 
            this.btnSimular.Location = new System.Drawing.Point(1120, 33);
            this.btnSimular.Name = "btnSimular";
            this.btnSimular.Size = new System.Drawing.Size(98, 32);
            this.btnSimular.TabIndex = 1;
            this.btnSimular.Text = "Iniciar Simulación";
            this.btnSimular.UseVisualStyleBackColor = true;
            this.btnSimular.Click += new System.EventHandler(this.btnSimular_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(180, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Tiempo de Simulación (hs)";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // txtTiempoSim
            // 
            this.txtTiempoSim.Location = new System.Drawing.Point(317, 37);
            this.txtTiempoSim.Name = "txtTiempoSim";
            this.txtTiempoSim.Size = new System.Drawing.Size(100, 20);
            this.txtTiempoSim.TabIndex = 3;
            // 
            // btnSalir
            // 
            this.btnSalir.Location = new System.Drawing.Point(1261, 608);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(75, 23);
            this.btnSalir.TabIndex = 4;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(534, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Desde";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(718, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Hasta";
            // 
            // textDesde
            // 
            this.textDesde.Location = new System.Drawing.Point(578, 37);
            this.textDesde.Name = "textDesde";
            this.textDesde.Size = new System.Drawing.Size(100, 20);
            this.textDesde.TabIndex = 7;
            // 
            // textHasta
            // 
            this.textHasta.Location = new System.Drawing.Point(759, 37);
            this.textHasta.Name = "textHasta";
            this.textHasta.Size = new System.Drawing.Size(100, 20);
            this.textHasta.TabIndex = 8;
            // 
            // textRecaudacion
            // 
            this.textRecaudacion.BackColor = System.Drawing.SystemColors.Info;
            this.textRecaudacion.Font = new System.Drawing.Font("MS Reference Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textRecaudacion.Location = new System.Drawing.Point(1117, 403);
            this.textRecaudacion.Name = "textRecaudacion";
            this.textRecaudacion.Size = new System.Drawing.Size(187, 33);
            this.textRecaudacion.TabIndex = 9;
            // 
            // textCostoRepuesto
            // 
            this.textCostoRepuesto.BackColor = System.Drawing.SystemColors.Info;
            this.textCostoRepuesto.Font = new System.Drawing.Font("MS Reference Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textCostoRepuesto.Location = new System.Drawing.Point(1117, 445);
            this.textCostoRepuesto.Name = "textCostoRepuesto";
            this.textCostoRepuesto.Size = new System.Drawing.Size(187, 33);
            this.textCostoRepuesto.TabIndex = 10;
            // 
            // textBeneficios
            // 
            this.textBeneficios.BackColor = System.Drawing.SystemColors.Info;
            this.textBeneficios.Font = new System.Drawing.Font("MS Reference Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBeneficios.Location = new System.Drawing.Point(1117, 523);
            this.textBeneficios.Name = "textBeneficios";
            this.textBeneficios.Size = new System.Drawing.Size(187, 33);
            this.textBeneficios.TabIndex = 11;
            // 
            // textCostoGarantia
            // 
            this.textCostoGarantia.BackColor = System.Drawing.SystemColors.Info;
            this.textCostoGarantia.Font = new System.Drawing.Font("MS Reference Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textCostoGarantia.Location = new System.Drawing.Point(1117, 484);
            this.textCostoGarantia.Name = "textCostoGarantia";
            this.textCostoGarantia.Size = new System.Drawing.Size(187, 33);
            this.textCostoGarantia.TabIndex = 12;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Franklin Gothic Medium", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Indigo;
            this.label4.Location = new System.Drawing.Point(978, 410);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(128, 26);
            this.label4.TabIndex = 13;
            this.label4.Text = "Recaudación";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Franklin Gothic Medium", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Indigo;
            this.label5.Location = new System.Drawing.Point(915, 452);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(191, 26);
            this.label5.TabIndex = 14;
            this.label5.Text = "Costo en Repuestos";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Franklin Gothic Medium", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Indigo;
            this.label6.Location = new System.Drawing.Point(1000, 530);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(106, 26);
            this.label6.TabIndex = 15;
            this.label6.Text = "Beneficios";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Franklin Gothic Medium", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Indigo;
            this.label7.Location = new System.Drawing.Point(935, 491);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(171, 26);
            this.label7.TabIndex = 16;
            this.label7.Text = "Costo en Garantía";
            this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // limpiar
            // 
            this.limpiar.Location = new System.Drawing.Point(1133, 4);
            this.limpiar.Name = "limpiar";
            this.limpiar.Size = new System.Drawing.Size(75, 23);
            this.limpiar.TabIndex = 17;
            this.limpiar.Text = "Limpiar";
            this.limpiar.UseVisualStyleBackColor = true;
            this.limpiar.Click += new System.EventHandler(this.limpiar_Click);
            // 
            // dgvClientes
            // 
            this.dgvClientes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvClientes.Location = new System.Drawing.Point(6, 5);
            this.dgvClientes.Name = "dgvClientes";
            this.dgvClientes.Size = new System.Drawing.Size(425, 258);
            this.dgvClientes.TabIndex = 18;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(1254, 75);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(82, 24);
            this.label8.TabIndex = 19;
            this.label8.Text = "Clientes ";
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(891, 376);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(445, 213);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Métricas";
            // 
            // Clientes
            // 
            this.Clientes.Controls.Add(this.cli);
            this.Clientes.Location = new System.Drawing.Point(891, 75);
            this.Clientes.Name = "Clientes";
            this.Clientes.SelectedIndex = 0;
            this.Clientes.Size = new System.Drawing.Size(445, 295);
            this.Clientes.TabIndex = 22;
            // 
            // cli
            // 
            this.cli.BackColor = System.Drawing.Color.Thistle;
            this.cli.Controls.Add(this.dgvClientes);
            this.cli.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cli.ForeColor = System.Drawing.Color.Black;
            this.cli.Location = new System.Drawing.Point(4, 22);
            this.cli.Name = "cli";
            this.cli.Padding = new System.Windows.Forms.Padding(3);
            this.cli.Size = new System.Drawing.Size(437, 269);
            this.cli.TabIndex = 1;
            this.cli.Text = "Clientes";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(12, 75);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(866, 582);
            this.tabControl1.TabIndex = 23;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.Thistle;
            this.tabPage1.Controls.Add(this.dgvSimular);
            this.tabPage1.ForeColor = System.Drawing.Color.Black;
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(858, 556);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Simulación Reparación de Electrodomésticos";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1358, 668);
            this.Controls.Add(this.Clientes);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.limpiar);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textCostoGarantia);
            this.Controls.Add(this.textBeneficios);
            this.Controls.Add(this.textCostoRepuesto);
            this.Controls.Add(this.textRecaudacion);
            this.Controls.Add(this.textHasta);
            this.Controls.Add(this.textDesde);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.txtTiempoSim);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSimular);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "Simulación";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSimular)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvClientes)).EndInit();
            this.Clientes.ResumeLayout(false);
            this.cli.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvSimular;
        private System.Windows.Forms.Button btnSimular;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTiempoSim;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textDesde;
        private System.Windows.Forms.TextBox textHasta;
        private System.Windows.Forms.TextBox textRecaudacion;
        private System.Windows.Forms.TextBox textCostoRepuesto;
        private System.Windows.Forms.TextBox textBeneficios;
        private System.Windows.Forms.TextBox textCostoGarantia;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button limpiar;
        private System.Windows.Forms.DataGridView dgvClientes;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TabControl Clientes;
        private System.Windows.Forms.TabPage cli;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
    }
}

