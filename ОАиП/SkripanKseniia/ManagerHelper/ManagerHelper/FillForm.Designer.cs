namespace ManagerHelper
{
    partial class FillForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            splitContainer1 = new SplitContainer();
            PaymentOps = new Label();
            PaymentCheckBox = new CheckedListBox();
            CopyButton = new Button();
            FillButton = new Button();
            NameManagerText = new TextBox();
            NameClientText = new TextBox();
            NameManager = new Label();
            NameClient = new Label();
            actionPanelForm2 = new RichTextBox();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(PaymentOps);
            splitContainer1.Panel1.Controls.Add(PaymentCheckBox);
            splitContainer1.Panel1.Controls.Add(CopyButton);
            splitContainer1.Panel1.Controls.Add(FillButton);
            splitContainer1.Panel1.Controls.Add(NameManagerText);
            splitContainer1.Panel1.Controls.Add(NameClientText);
            splitContainer1.Panel1.Controls.Add(NameManager);
            splitContainer1.Panel1.Controls.Add(NameClient);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(actionPanelForm2);
            splitContainer1.Size = new Size(800, 450);
            splitContainer1.SplitterDistance = 229;
            splitContainer1.TabIndex = 3;
            // 
            // PaymentOps
            // 
            PaymentOps.AutoSize = true;
            PaymentOps.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            PaymentOps.Location = new Point(12, 179);
            PaymentOps.Name = "PaymentOps";
            PaymentOps.Size = new Size(131, 19);
            PaymentOps.TabIndex = 7;
            PaymentOps.Text = "Способы оплаты:";
            // 
            // PaymentCheckBox
            // 
            PaymentCheckBox.FormattingEnabled = true;
            PaymentCheckBox.Items.AddRange(new object[] { "Тест", "тест", "тЕСТ", "тсеТ", "бубубу", "Бабаба", "бЕбЕбЕ", "оыолва", "щзцз", "длыгшцл" });
            PaymentCheckBox.Location = new Point(12, 201);
            PaymentCheckBox.Name = "PaymentCheckBox";
            PaymentCheckBox.Size = new Size(202, 112);
            PaymentCheckBox.TabIndex = 6;
            // 
            // CopyButton
            // 
            CopyButton.Font = new Font("Segoe UI", 10F);
            CopyButton.Location = new Point(13, 387);
            CopyButton.Name = "CopyButton";
            CopyButton.Size = new Size(121, 38);
            CopyButton.TabIndex = 5;
            CopyButton.Text = "Скопировать";
            CopyButton.UseVisualStyleBackColor = true;
            // 
            // FillButton
            // 
            FillButton.Font = new Font("Segoe UI", 10F);
            FillButton.Location = new Point(13, 343);
            FillButton.Name = "FillButton";
            FillButton.Size = new Size(121, 38);
            FillButton.TabIndex = 4;
            FillButton.Text = "Заполнить";
            FillButton.UseVisualStyleBackColor = true;
            // 
            // NameManagerText
            // 
            NameManagerText.Location = new Point(12, 116);
            NameManagerText.Multiline = true;
            NameManagerText.Name = "NameManagerText";
            NameManagerText.Size = new Size(202, 49);
            NameManagerText.TabIndex = 3;
            // 
            // NameClientText
            // 
            NameClientText.Location = new Point(13, 32);
            NameClientText.Multiline = true;
            NameClientText.Name = "NameClientText";
            NameClientText.Size = new Size(202, 49);
            NameClientText.TabIndex = 2;
            // 
            // NameManager
            // 
            NameManager.AutoSize = true;
            NameManager.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            NameManager.Location = new Point(13, 94);
            NameManager.Name = "NameManager";
            NameManager.Size = new Size(133, 19);
            NameManager.TabIndex = 1;
            NameManager.Text = "ФИО менеджера:";
            // 
            // NameClient
            // 
            NameClient.AutoSize = true;
            NameClient.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            NameClient.Location = new Point(13, 10);
            NameClient.Name = "NameClient";
            NameClient.Size = new Size(107, 19);
            NameClient.TabIndex = 0;
            NameClient.Text = "ФИО клиента:";
            // 
            // actionPanelForm2
            // 
            actionPanelForm2.BorderStyle = BorderStyle.None;
            actionPanelForm2.Location = new Point(3, 12);
            actionPanelForm2.Name = "actionPanelForm2";
            actionPanelForm2.Size = new Size(552, 426);
            actionPanelForm2.TabIndex = 2;
            actionPanelForm2.Text = "";
            // 
            // FillForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(splitContainer1);
            Name = "FillForm";
            Text = "FillForm";
            Load += FillForm_Load;
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel1.PerformLayout();
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private SplitContainer splitContainer1;
        private RichTextBox actionPanelForm2;
        private Label NameClient;
        private Button CopyButton;
        private Button FillButton;
        private TextBox NameManagerText;
        private TextBox NameClientText;
        private Label NameManager;
        private Label PaymentOps;
        private CheckedListBox PaymentCheckBox;
    }
}