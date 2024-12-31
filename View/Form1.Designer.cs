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
            pictureBox1 = new PictureBox();
            boxModulo = new ComboBox();
            label7 = new Label();
            minutos = new NumericUpDown();
            btnRegistrar = new Button();
            txtObservaciones = new RichTextBox();
            txtDecripTarea = new RichTextBox();
            label8 = new Label();
            horas = new NumericUpDown();
            button3 = new Button();
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
            tabPage1.Controls.Add(pictureBox1);
            tabPage1.Controls.Add(boxModulo);
            tabPage1.Controls.Add(label7);
            tabPage1.Controls.Add(minutos);
            tabPage1.Controls.Add(btnRegistrar);
            tabPage1.Controls.Add(txtObservaciones);
            tabPage1.Controls.Add(txtDecripTarea);
            tabPage1.Controls.Add(label8);
            tabPage1.Controls.Add(horas);
            tabPage1.Controls.Add(button3);
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
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(888, 500);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Home";
            tabPage1.Click += tabPage1_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(247, 371);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(435, 133);
            pictureBox1.TabIndex = 77;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click_1;
            // 
            // boxModulo
            // 
            boxModulo.DropDownStyle = ComboBoxStyle.DropDownList;
            boxModulo.FormattingEnabled = true;
            boxModulo.Location = new Point(328, 237);
            boxModulo.Name = "boxModulo";
            boxModulo.Size = new Size(121, 23);
            boxModulo.TabIndex = 30;
            boxModulo.SelectedIndexChanged += boxModulo_SelectedIndexChanged;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(601, 255);
            label7.Name = "label7";
            label7.Size = new Size(102, 15);
            label7.TabIndex = 76;
            label7.Text = "Tiempo (Minutos)";
            // 
            // minutos
            // 
            minutos.Increment = new decimal(new int[] { 5, 0, 0, 0 });
            minutos.Location = new Point(603, 272);
            minutos.Maximum = new decimal(new int[] { 55, 0, 0, 0 });
            minutos.Name = "minutos";
            minutos.Size = new Size(120, 23);
            minutos.TabIndex = 75;
            // 
            // btnRegistrar
            // 
            btnRegistrar.Location = new Point(328, 291);
            btnRegistrar.Name = "btnRegistrar";
            btnRegistrar.Size = new Size(121, 23);
            btnRegistrar.TabIndex = 74;
            btnRegistrar.Text = "Registrar\r\n";
            btnRegistrar.UseVisualStyleBackColor = true;
            btnRegistrar.Click += btnRegistrar_Click;
            // 
            // txtObservaciones
            // 
            txtObservaciones.Location = new Point(477, 192);
            txtObservaciones.Name = "txtObservaciones";
            txtObservaciones.Size = new Size(278, 50);
            txtObservaciones.TabIndex = 73;
            txtObservaciones.Text = "";
            // 
            // txtDecripTarea
            // 
            txtDecripTarea.Location = new Point(475, 74);
            txtDecripTarea.Name = "txtDecripTarea";
            txtDecripTarea.Size = new Size(280, 76);
            txtDecripTarea.TabIndex = 72;
            txtDecripTarea.Text = "";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(475, 255);
            label8.Name = "label8";
            label8.Size = new Size(72, 15);
            label8.TabIndex = 41;
            label8.Text = "Tiempo (Hs)";
            // 
            // horas
            // 
            horas.Location = new Point(477, 272);
            horas.Maximum = new decimal(new int[] { 24, 0, 0, 0 });
            horas.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            horas.Name = "horas";
            horas.Size = new Size(120, 23);
            horas.TabIndex = 40;
            horas.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // button3
            // 
            button3.Location = new Point(102, 227);
            button3.Name = "button3";
            button3.Size = new Size(75, 23);
            button3.TabIndex = 39;
            button3.Text = "Consultar";
            button3.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Location = new Point(328, 332);
            button2.Name = "button2";
            button2.Size = new Size(121, 23);
            button2.TabIndex = 38;
            button2.Text = "Ver Bitacora";
            button2.UseVisualStyleBackColor = true;
            button2.Click += ver_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(477, 165);
            label6.Name = "label6";
            label6.Size = new Size(84, 15);
            label6.TabIndex = 34;
            label6.Text = "Observaciones";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(475, 53);
            label5.Name = "label5";
            label5.Size = new Size(83, 15);
            label5.TabIndex = 32;
            label5.Text = "Tarea realizada";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(328, 219);
            label4.Name = "label4";
            label4.Size = new Size(49, 15);
            label4.TabIndex = 31;
            label4.Text = "Modulo";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(328, 53);
            label3.Name = "label3";
            label3.Size = new Size(49, 15);
            label3.TabIndex = 29;
            label3.Text = "Recurso";
            // 
            // boxRecurso
            // 
            boxRecurso.DropDownStyle = ComboBoxStyle.DropDownList;
            boxRecurso.FormattingEnabled = true;
            boxRecurso.Items.AddRange(new object[] { "Alejandra Chevillard ", "Corina Fitzpatrick", "Cecilia Gonzalez", "Carlos Romano", "Daniel Palavecino", "Daniel Nuñez", "Daniel Pizarro", "Daniel Marzellino", "Diego Bruses", "Diego Fraiese", "Fernando Sottano", "Fernando Sato", "Galo Olguin", "Gabriela Piro", "Ivan Monges", "Jannet Arribasplata", "Laura Ozcoidi", "Marcia Garcia", "Marcio Palazzo", "Miguel Ponzo", "Maximilano Primi", "Martin Ale", "Mariela Zanuttini", "Oscar Tello", "Roberto Lo Bue", "Rodrigo Peralta" });
            boxRecurso.Location = new Point(328, 71);
            boxRecurso.Name = "boxRecurso";
            boxRecurso.Size = new Size(121, 23);
            boxRecurso.TabIndex = 28;
            boxRecurso.SelectedIndexChanged += boxRecurso_SelectedIndexChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(328, 165);
            label2.Name = "label2";
            label2.Size = new Size(40, 15);
            label2.TabIndex = 27;
            label2.Text = "Banco";
            // 
            // boxBanco
            // 
            boxBanco.DropDownStyle = ComboBoxStyle.DropDownList;
            boxBanco.FormattingEnabled = true;
            boxBanco.Location = new Point(328, 183);
            boxBanco.Name = "boxBanco";
            boxBanco.Size = new Size(121, 23);
            boxBanco.TabIndex = 26;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(328, 109);
            label1.Name = "label1";
            label1.Size = new Size(76, 15);
            label1.TabIndex = 25;
            label1.Text = "Tipo de Tarea";
            // 
            // boxtipoTarea
            // 
            boxtipoTarea.DropDownStyle = ComboBoxStyle.DropDownList;
            boxtipoTarea.FormattingEnabled = true;
            boxtipoTarea.Items.AddRange(new object[] { "Analisis", "Desarrollo", "Pruebas Unitarias", "Testing" });
            boxtipoTarea.Location = new Point(328, 127);
            boxtipoTarea.Name = "boxtipoTarea";
            boxtipoTarea.Size = new Size(118, 23);
            boxtipoTarea.TabIndex = 24;
            // 
            // calendario
            // 
            calendario.BackColor = SystemColors.Window;
            calendario.Location = new Point(34, 53);
            calendario.Name = "calendario";
            calendario.TabIndex = 23;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(896, 528);
            tabControl1.TabIndex = 22;
            // 
            // MiBitacora
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Menu;
            ClientSize = new Size(896, 528);
            Controls.Add(tabControl1);
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
        private RadioButton radioButton1;
        private TabPage tabPage1;
        private Label label7;
        private NumericUpDown minutos;
        private Button btnRegistrar;
        private RichTextBox txtObservaciones;
        private RichTextBox txtDecripTarea;
        private Label label8;
        private NumericUpDown horas;
        private Button button3;
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
    }
}
