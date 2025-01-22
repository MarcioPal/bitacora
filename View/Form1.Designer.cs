namespace Bitacora
{
    partial class MiBitacora
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MiBitacora));
            tabPage1 = new TabPage();
            button3 = new Button();
            button1 = new Button();
            pictureBox1 = new PictureBox();
            boxModulo = new ComboBox();
            label7 = new Label();
            minutos = new NumericUpDown();
            btnRegistrar = new Button();
            txtObservaciones = new RichTextBox();
            txtDecripTarea = new RichTextBox();
            label8 = new Label();
            horas = new NumericUpDown();
            button2 = new Button();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            boxRecurso = new ComboBox();
            label2 = new Label();
            boxBanco = new ComboBox();
            label1 = new Label();
            boxtipoTarea = new ComboBox();
            calendario = new MonthCalendar();
            tabControl1 = new TabControl();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            colorDialog1 = new ColorDialog();
            tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)minutos).BeginInit();
            ((System.ComponentModel.ISupportInitialize)horas).BeginInit();
            tabControl1.SuspendLayout();
            SuspendLayout();
            // 
            // tabPage1
            // 
            tabPage1.BackColor = Color.WhiteSmoke;
            tabPage1.Controls.Add(button3);
            tabPage1.Controls.Add(button1);
            tabPage1.Controls.Add(pictureBox1);
            tabPage1.Controls.Add(boxModulo);
            tabPage1.Controls.Add(label7);
            tabPage1.Controls.Add(minutos);
            tabPage1.Controls.Add(btnRegistrar);
            tabPage1.Controls.Add(txtObservaciones);
            tabPage1.Controls.Add(txtDecripTarea);
            tabPage1.Controls.Add(label8);
            tabPage1.Controls.Add(horas);
            tabPage1.Controls.Add(button2);
            tabPage1.Controls.Add(label6);
            tabPage1.Controls.Add(label5);
            tabPage1.Controls.Add(label4);
            tabPage1.Controls.Add(label3);
            tabPage1.Controls.Add(boxRecurso);
            tabPage1.Controls.Add(label2);
            tabPage1.Controls.Add(boxBanco);
            tabPage1.Controls.Add(label1);
            tabPage1.Controls.Add(boxtipoTarea);
            tabPage1.Controls.Add(calendario);
            tabPage1.Location = new Point(4, 34);
            tabPage1.Margin = new Padding(4, 5, 4, 5);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(4, 5, 4, 5);
            tabPage1.Size = new Size(1169, 589);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Home";
            tabPage1.Click += tabPage1_Click;
            // 
            // button3
            // 
            button3.Location = new Point(43, 375);
            button3.Name = "button3";
            button3.Size = new Size(173, 36);
            button3.TabIndex = 79;
            button3.Text = "Consultar";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button1
            // 
            button1.Location = new Point(223, 441);
            button1.Name = "button1";
            button1.Size = new Size(173, 38);
            button1.TabIndex = 78;
            button1.Text = "Enviar";
            button1.UseVisualStyleBackColor = true;
            button1.Click += enviar_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(373, -67);
            pictureBox1.Margin = new Padding(4, 5, 4, 5);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(415, 103);
            pictureBox1.TabIndex = 77;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click_1;
            // 
            // boxModulo
            // 
            boxModulo.DropDownStyle = ComboBoxStyle.DropDownList;
            boxModulo.FormattingEnabled = true;
            boxModulo.Items.AddRange(new object[] { "Finesse", "Clientes", "Préstamos y Garantías", "Cuentas Vista (CC y CA)", "Valores", "Plazo Fijo", "Generales", "Apoyo y Seguridad", "Host to Host", "Contabilidad", "Regimenes Informativos (BCRA)" });
            boxModulo.Location = new Point(455, 382);
            boxModulo.Margin = new Padding(4, 5, 4, 5);
            boxModulo.Name = "boxModulo";
            boxModulo.Size = new Size(187, 33);
            boxModulo.TabIndex = 30;
            boxModulo.SelectedIndexChanged += boxModulo_SelectedIndexChanged;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(865, 352);
            label7.Margin = new Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new Size(152, 25);
            label7.TabIndex = 76;
            label7.Text = "Tiempo (Minutos)";
            // 
            // minutos
            // 
            minutos.Increment = new decimal(new int[] { 5, 0, 0, 0 });
            minutos.Location = new Point(867, 380);
            minutos.Margin = new Padding(4, 5, 4, 5);
            minutos.Maximum = new decimal(new int[] { 55, 0, 0, 0 });
            minutos.Name = "minutos";
            minutos.Size = new Size(171, 31);
            minutos.TabIndex = 75;
            // 
            // btnRegistrar
            // 
            btnRegistrar.Location = new Point(223, 373);
            btnRegistrar.Margin = new Padding(4, 5, 4, 5);
            btnRegistrar.Name = "btnRegistrar";
            btnRegistrar.Size = new Size(173, 38);
            btnRegistrar.TabIndex = 74;
            btnRegistrar.Text = "Registrar";
            btnRegistrar.UseVisualStyleBackColor = true;
            btnRegistrar.Click += btnRegistrar_Click;
            // 
            // txtObservaciones
            // 
            txtObservaciones.Location = new Point(684, 259);
            txtObservaciones.Margin = new Padding(4, 5, 4, 5);
            txtObservaciones.Name = "txtObservaciones";
            txtObservaciones.Size = new Size(395, 81);
            txtObservaciones.TabIndex = 73;
            txtObservaciones.Text = "";
            // 
            // txtDecripTarea
            // 
            txtDecripTarea.Location = new Point(681, 105);
            txtDecripTarea.Margin = new Padding(4, 5, 4, 5);
            txtDecripTarea.Name = "txtDecripTarea";
            txtDecripTarea.Size = new Size(398, 124);
            txtDecripTarea.TabIndex = 72;
            txtDecripTarea.Text = "";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(685, 352);
            label8.Margin = new Padding(4, 0, 4, 0);
            label8.Name = "label8";
            label8.Size = new Size(134, 25);
            label8.TabIndex = 41;
            label8.Text = "Tiempo (Horas)";
            // 
            // horas
            // 
            horas.Location = new Point(687, 380);
            horas.Margin = new Padding(4, 5, 4, 5);
            horas.Maximum = new decimal(new int[] { 24, 0, 0, 0 });
            horas.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            horas.Name = "horas";
            horas.Size = new Size(171, 31);
            horas.TabIndex = 40;
            horas.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // button2
            // 
            button2.Location = new Point(43, 441);
            button2.Margin = new Padding(4, 5, 4, 5);
            button2.Name = "button2";
            button2.Size = new Size(173, 38);
            button2.TabIndex = 38;
            button2.Text = "Ver Bitacora";
            button2.UseVisualStyleBackColor = true;
            button2.Click += ver_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(680, 232);
            label6.Margin = new Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new Size(128, 25);
            label6.TabIndex = 34;
            label6.Text = "Observaciones";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(681, 75);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(126, 25);
            label5.TabIndex = 32;
            label5.Text = "Tarea realizada";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(471, 352);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(75, 25);
            label4.TabIndex = 31;
            label4.Text = "Modulo";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(471, 75);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(74, 25);
            label3.TabIndex = 29;
            label3.Text = "Recurso";
            // 
            // boxRecurso
            // 
            boxRecurso.FormattingEnabled = true;
            boxRecurso.Items.AddRange(new object[] { "Alejandra Chevillard ", "Corina Fitzpatrick", "Cecilia Gonzalez", "Carlos Romano", "Daniel Palavecino", "Daniel Nuñez", "Daniel Pizarro", "Daniel Marzellino", "Diego Bruses", "Diego Fraiese", "Fernando Sottano", "Fernando Sato", "Galo Olguin", "Gabriela Piro", "Ivan Monges", "Jannet Arribasplata", "Laura Ozcoidi", "Marcia Garcia", "Marcio Palazzo", "Miguel Ponzo", "Maximiliano Primi", "Martin Ale", "Mariela Zanuttini", "Oscar Tello", "Roberto LoBue", "Rodrigo Peralta" });
            boxRecurso.Location = new Point(455, 105);
            boxRecurso.Margin = new Padding(4, 5, 4, 5);
            boxRecurso.Name = "boxRecurso";
            boxRecurso.Size = new Size(187, 33);
            boxRecurso.TabIndex = 28;
            boxRecurso.SelectedIndexChanged += boxRecurso_SelectedIndexChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(471, 262);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(60, 25);
            label2.TabIndex = 27;
            label2.Text = "Banco";
            // 
            // boxBanco
            // 
            boxBanco.DropDownStyle = ComboBoxStyle.DropDownList;
            boxBanco.FormattingEnabled = true;
            boxBanco.Items.AddRange(new object[] { "SC", "SJ", "NB", "UN", "MI" });
            boxBanco.Location = new Point(455, 292);
            boxBanco.Margin = new Padding(4, 5, 4, 5);
            boxBanco.Name = "boxBanco";
            boxBanco.Size = new Size(187, 33);
            boxBanco.TabIndex = 26;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(471, 169);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(117, 25);
            label1.TabIndex = 25;
            label1.Text = "Tipo de Tarea";
            // 
            // boxtipoTarea
            // 
            boxtipoTarea.DropDownStyle = ComboBoxStyle.DropDownList;
            boxtipoTarea.FormattingEnabled = true;
            boxtipoTarea.Items.AddRange(new object[] { "MC-NORM", "MC-APLIC", "DE-NORM", "DE-APLIC", "ASIST-PROD", "ASIST-TEST", "ASIST-REQ", "ASIST-TEC", "OT-OT" });
            boxtipoTarea.Location = new Point(455, 199);
            boxtipoTarea.Margin = new Padding(4, 5, 4, 5);
            boxtipoTarea.Name = "boxtipoTarea";
            boxtipoTarea.Size = new Size(183, 33);
            boxtipoTarea.TabIndex = 24;
            // 
            // calendario
            // 
            calendario.BackColor = SystemColors.Window;
            calendario.Location = new Point(49, 88);
            calendario.Margin = new Padding(13, 15, 13, 15);
            calendario.Name = "calendario";
            calendario.TabIndex = 23;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 0);
            tabControl1.Margin = new Padding(4, 5, 4, 5);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1177, 627);
            tabControl1.TabIndex = 22;
            // 
            // MiBitacora
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Menu;
            ClientSize = new Size(1177, 627);
            Controls.Add(tabControl1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 5, 4, 5);
            Name = "MiBitacora";
            Text = "MiBitacora";
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)minutos).EndInit();
            ((System.ComponentModel.ISupportInitialize)horas).EndInit();
            tabControl1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private TabPage tabPage1;
        private Label label7;
        private NumericUpDown minutos;
        private Button btnRegistrar;
        private RichTextBox txtObservaciones;
        private RichTextBox txtDecripTarea;
        private Label label8;
        private NumericUpDown horas;
        private Button button2;
        private Label label6;
        private Label label5;
        private Label label4;
        private ComboBox boxModulo;
        private Label label3;
        private ComboBox boxRecurso;
        private Label label2;
        private ComboBox boxBanco;
        private Label label1;
        private ComboBox boxtipoTarea;
        private MonthCalendar calendario;
        private TabControl tabControl1;
        private PictureBox pictureBox1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private Button button1;
        private ColorDialog colorDialog1;
        private Button button3;
    }
}
