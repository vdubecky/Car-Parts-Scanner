namespace scanner_desktop
{
    partial class Form1
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
            textBox1 = new TextBox();
            groupBox1 = new GroupBox();
            button1 = new Button();
            label2 = new Label();
            label1 = new Label();
            button2 = new Button();
            listView1 = new ListView();
            columnHeader1 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            nameRadio = new RadioButton();
            codeRadio = new RadioButton();
            label3 = new Label();
            label4 = new Label();
            groupBox2 = new GroupBox();
            textBox3 = new TextBox();
            textBox2 = new TextBox();
            button3 = new Button();
            label5 = new Label();
            label6 = new Label();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBox1.Location = new Point(104, 17);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(393, 27);
            textBox1.TabIndex = 1;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            groupBox1.Controls.Add(button1);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(752, 58);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(418, 160);
            groupBox1.TabIndex = 4;
            groupBox1.TabStop = false;
            groupBox1.Text = "Vybraný produkt";
            // 
            // button1
            // 
            button1.Location = new Point(311, 115);
            button1.Name = "button1";
            button1.Size = new Size(94, 29);
            button1.TabIndex = 2;
            button1.Text = "Smazat";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(15, 89);
            label2.Name = "label2";
            label2.Size = new Size(39, 20);
            label2.TabIndex = 1;
            label2.Text = "Kód:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(15, 40);
            label1.Name = "label1";
            label1.Size = new Size(53, 20);
            label1.TabIndex = 0;
            label1.Text = "Název:";
            // 
            // button2
            // 
            button2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button2.Location = new Point(1063, 23);
            button2.Name = "button2";
            button2.Size = new Size(107, 29);
            button2.TabIndex = 5;
            button2.Text = "Konfigurace";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // listView1
            // 
            listView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            listView1.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2 });
            listView1.Location = new Point(22, 58);
            listView1.MinimumSize = new Size(100, 250);
            listView1.Name = "listView1";
            listView1.Size = new Size(710, 510);
            listView1.TabIndex = 6;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.View = View.Details;
            listView1.ItemSelectionChanged += listView1_ItemSelectionChanged;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "Kód";
            columnHeader1.Width = 200;
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "Název";
            columnHeader2.Width = 500;
            // 
            // nameRadio
            // 
            nameRadio.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            nameRadio.AutoSize = true;
            nameRadio.Checked = true;
            nameRadio.Location = new Point(583, 18);
            nameRadio.Name = "nameRadio";
            nameRadio.Size = new Size(71, 24);
            nameRadio.TabIndex = 7;
            nameRadio.TabStop = true;
            nameRadio.Text = "Název";
            nameRadio.UseVisualStyleBackColor = true;
            nameRadio.CheckedChanged += nameRadio_CheckedChanged;
            // 
            // codeRadio
            // 
            codeRadio.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            codeRadio.AutoSize = true;
            codeRadio.Location = new Point(660, 18);
            codeRadio.Name = "codeRadio";
            codeRadio.Size = new Size(57, 24);
            codeRadio.TabIndex = 8;
            codeRadio.Text = "Kód";
            codeRadio.UseVisualStyleBackColor = true;
            codeRadio.CheckedChanged += codeRadio_CheckedChanged;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label3.AutoSize = true;
            label3.Location = new Point(515, 20);
            label3.Name = "label3";
            label3.Size = new Size(49, 20);
            label3.TabIndex = 9;
            label3.Text = "Podle:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(22, 20);
            label4.Name = "label4";
            label4.Size = new Size(67, 20);
            label4.TabIndex = 10;
            label4.Text = "Vyhledat";
            // 
            // groupBox2
            // 
            groupBox2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            groupBox2.Controls.Add(textBox3);
            groupBox2.Controls.Add(textBox2);
            groupBox2.Controls.Add(button3);
            groupBox2.Controls.Add(label5);
            groupBox2.Controls.Add(label6);
            groupBox2.Location = new Point(752, 259);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(418, 250);
            groupBox2.TabIndex = 5;
            groupBox2.TabStop = false;
            groupBox2.Text = "Nový produkt";
            // 
            // textBox3
            // 
            textBox3.Location = new Point(15, 134);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(390, 27);
            textBox3.TabIndex = 4;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(15, 63);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(390, 27);
            textBox2.TabIndex = 3;
            // 
            // button3
            // 
            button3.Location = new Point(311, 203);
            button3.Name = "button3";
            button3.Size = new Size(94, 29);
            button3.TabIndex = 2;
            button3.Text = "Přidat";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(15, 111);
            label5.Name = "label5";
            label5.Size = new Size(36, 20);
            label5.TabIndex = 1;
            label5.Text = "Kód";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(15, 40);
            label6.Name = "label6";
            label6.Size = new Size(50, 20);
            label6.TabIndex = 0;
            label6.Text = "Název";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1182, 653);
            Controls.Add(groupBox2);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(codeRadio);
            Controls.Add(nameRadio);
            Controls.Add(listView1);
            Controls.Add(button2);
            Controls.Add(groupBox1);
            Controls.Add(textBox1);
            Margin = new Padding(3, 4, 3, 4);
            Name = "Form1";
            Text = "Databáze produktů";
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label testLavel;
        private TextBox textBox1;
        private GroupBox groupBox1;
        private Button button1;
        private Label label2;
        private Label label1;
        private Button button2;
        private ListView listView1;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private RadioButton nameRadio;
        private RadioButton codeRadio;
        private Label label3;
        private Label label4;
        private GroupBox groupBox2;
        private Button button3;
        private Label label5;
        private Label label6;
        private TextBox textBox2;
        private TextBox textBox3;
    }
}