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
            FillButton = new Button();
            CopyButton = new Button();
            PaymentCheckBox = new CheckedListBox();
            Payment = new Label();
            NameManagerText = new TextBox();
            NameManager = new Label();
            NameClientText = new TextBox();
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
            splitContainer1.Panel1.Controls.Add(FillButton);
            splitContainer1.Panel1.Controls.Add(CopyButton);
            splitContainer1.Panel1.Controls.Add(PaymentCheckBox);
            splitContainer1.Panel1.Controls.Add(Payment);
            splitContainer1.Panel1.Controls.Add(NameManagerText);
            splitContainer1.Panel1.Controls.Add(NameManager);
            splitContainer1.Panel1.Controls.Add(NameClientText);
            splitContainer1.Panel1.Controls.Add(NameClient);
            splitContainer1.Panel1.Paint += splitContainer1_Panel1_Paint;
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(actionPanelForm2);
            splitContainer1.Panel2.Paint += splitContainer1_Panel2_Paint;
            splitContainer1.Size = new Size(800, 450);
            splitContainer1.SplitterDistance = 266;
            splitContainer1.TabIndex = 0;
            // 
            // FillButton
            // 
            FillButton.BackColor = SystemColors.ActiveCaption;
            FillButton.Font = new Font("Segoe UI", 10F);
            FillButton.Location = new Point(12, 347);
            FillButton.Name = "FillButton";
            FillButton.Size = new Size(215, 41);
            FillButton.TabIndex = 8;
            FillButton.Text = "Заполнить";
            FillButton.UseVisualStyleBackColor = false;
            FillButton.Click += FillButton_Click;
            // 
            // CopyButton
            // 
            CopyButton.BackColor = SystemColors.ActiveCaption;
            CopyButton.Font = new Font("Segoe UI", 10F);
            CopyButton.Location = new Point(12, 394);
            CopyButton.Name = "CopyButton";
            CopyButton.Size = new Size(215, 41);
            CopyButton.TabIndex = 7;
            CopyButton.Text = "Скопировать";
            CopyButton.UseVisualStyleBackColor = false;
            CopyButton.Click += CopyButton_Click;
            // 
            // PaymentCheckBox
            // 
            PaymentCheckBox.FormattingEnabled = true;
            PaymentCheckBox.Items.AddRange(new object[] { "кредит", "рассрочка", "материнский капитал", "СБП", "реквизиты", "сайт", "тест скроллинга", "тест скроллинга", "тест скроллинга", "тест скроллинга", "тест скроллинга" });
            PaymentCheckBox.Location = new Point(12, 199);
            PaymentCheckBox.Name = "PaymentCheckBox";
            PaymentCheckBox.Size = new Size(215, 130);
            PaymentCheckBox.TabIndex = 5;
            PaymentCheckBox.SelectedIndexChanged += PaymentCheckBox_SelectedIndexChanged;
            // 
            // Payment
            // 
            Payment.AutoSize = true;
            Payment.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 204);
            Payment.Location = new Point(12, 179);
            Payment.Name = "Payment";
            Payment.Size = new Size(121, 17);
            Payment.TabIndex = 4;
            Payment.Text = "Способы оплаты:";
            Payment.Click += Payment_Click;
            // 
            // NameManagerText
            // 
            NameManagerText.BorderStyle = BorderStyle.FixedSingle;
            NameManagerText.Location = new Point(12, 111);
            NameManagerText.Multiline = true;
            NameManagerText.Name = "NameManagerText";
            NameManagerText.Size = new Size(215, 38);
            NameManagerText.TabIndex = 3;
            NameManagerText.TextChanged += NameManagerText_TextChanged;
            // 
            // NameManager
            // 
            NameManager.AutoSize = true;
            NameManager.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 204);
            NameManager.Location = new Point(12, 91);
            NameManager.Name = "NameManager";
            NameManager.Size = new Size(120, 17);
            NameManager.TabIndex = 2;
            NameManager.Text = "ФИО менеджера:";
            NameManager.Click += NameManager_Click;
            // 
            // NameClientText
            // 
            NameClientText.BorderStyle = BorderStyle.FixedSingle;
            NameClientText.Location = new Point(12, 29);
            NameClientText.Multiline = true;
            NameClientText.Name = "NameClientText";
            NameClientText.Size = new Size(215, 38);
            NameClientText.TabIndex = 1;
            NameClientText.TextChanged += NameClientText_TextChanged;
            // 
            // NameClient
            // 
            NameClient.AutoSize = true;
            NameClient.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 204);
            NameClient.Location = new Point(12, 9);
            NameClient.Name = "NameClient";
            NameClient.Size = new Size(98, 17);
            NameClient.TabIndex = 0;
            NameClient.Text = "ФИО клиента:";
            NameClient.Click += NameClient_Click;
            // 
            // actionPanelForm2
            // 
            actionPanelForm2.Location = new Point(3, 12);
            actionPanelForm2.Name = "actionPanelForm2";
            actionPanelForm2.Size = new Size(515, 423);
            actionPanelForm2.TabIndex = 0;
            actionPanelForm2.Text = "";
            actionPanelForm2.TextChanged += actionPanelForm2_TextChanged;
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
        private CheckedListBox PaymentCheckBox;
        private Label Payment;
        private TextBox NameManagerText;
        private Label NameManager;
        private TextBox NameClientText;
        private Label NameClient;
        private Button FillButton;
        private Button CopyButton;
        private RichTextBox actionPanelForm2;
    }
}