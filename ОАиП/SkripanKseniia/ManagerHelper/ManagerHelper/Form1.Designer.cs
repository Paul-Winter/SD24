namespace ManagerHelper
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
            splitContainer1 = new SplitContainer();
            label1 = new Label();
            optionsPanel = new Panel();
            btnQueries = new Button();
            btnObjections = new Button();
            btnPrice = new Button();
            btnDesc = new Button();
            OfflineOnline = new TabControl();
            OnlinePage = new TabPage();
            CollegeOnline = new Button();
            VUZOnline = new Button();
            AcademyOnline = new Button();
            OfflinePage = new TabPage();
            SchoolOffline = new Button();
            CollegeOffline = new Button();
            VUZOffline = new Button();
            AcademyOffline = new Button();
            actionPanelForm1 = new RichTextBox();
            SearchText = new TextBox();
            Search = new Button();
            Form2TestButton = new Button();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            optionsPanel.SuspendLayout();
            OfflineOnline.SuspendLayout();
            OnlinePage.SuspendLayout();
            OfflinePage.SuspendLayout();
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
            splitContainer1.Panel1.Controls.Add(Form2TestButton);
            splitContainer1.Panel1.Controls.Add(label1);
            splitContainer1.Panel1.Controls.Add(optionsPanel);
            splitContainer1.Panel1.Controls.Add(OfflineOnline);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(actionPanelForm1);
            splitContainer1.Panel2.Controls.Add(SearchText);
            splitContainer1.Panel2.Controls.Add(Search);
            splitContainer1.Size = new Size(800, 450);
            splitContainer1.SplitterDistance = 229;
            splitContainer1.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(10, 178);
            label1.Name = "label1";
            label1.Size = new Size(211, 15);
            label1.TabIndex = 2;
            label1.Text = "—————————————————";
            label1.Click += label1_Click;
            // 
            // optionsPanel
            // 
            optionsPanel.AutoScroll = true;
            optionsPanel.Controls.Add(btnQueries);
            optionsPanel.Controls.Add(btnObjections);
            optionsPanel.Controls.Add(btnPrice);
            optionsPanel.Controls.Add(btnDesc);
            optionsPanel.Location = new Point(7, 200);
            optionsPanel.Name = "optionsPanel";
            optionsPanel.Size = new Size(215, 221);
            optionsPanel.TabIndex = 2;
            // 
            // btnQueries
            // 
            btnQueries.Dock = DockStyle.Top;
            btnQueries.Font = new Font("Segoe UI", 12F);
            btnQueries.Location = new Point(0, 99);
            btnQueries.Name = "btnQueries";
            btnQueries.Size = new Size(215, 33);
            btnQueries.TabIndex = 4;
            btnQueries.Text = "Запросы";
            btnQueries.TextAlign = ContentAlignment.MiddleLeft;
            btnQueries.UseVisualStyleBackColor = true;
            // 
            // btnObjections
            // 
            btnObjections.Dock = DockStyle.Top;
            btnObjections.Font = new Font("Segoe UI", 12F);
            btnObjections.Location = new Point(0, 66);
            btnObjections.Name = "btnObjections";
            btnObjections.Size = new Size(215, 33);
            btnObjections.TabIndex = 3;
            btnObjections.Text = "Возражения";
            btnObjections.TextAlign = ContentAlignment.MiddleLeft;
            btnObjections.UseVisualStyleBackColor = true;
            // 
            // btnPrice
            // 
            btnPrice.Dock = DockStyle.Top;
            btnPrice.Font = new Font("Segoe UI", 12F);
            btnPrice.Location = new Point(0, 33);
            btnPrice.Name = "btnPrice";
            btnPrice.Size = new Size(215, 33);
            btnPrice.TabIndex = 2;
            btnPrice.Text = "Цены";
            btnPrice.TextAlign = ContentAlignment.MiddleLeft;
            btnPrice.UseVisualStyleBackColor = true;
            // 
            // btnDesc
            // 
            btnDesc.Dock = DockStyle.Top;
            btnDesc.Font = new Font("Segoe UI", 12F);
            btnDesc.Location = new Point(0, 0);
            btnDesc.Name = "btnDesc";
            btnDesc.Size = new Size(215, 33);
            btnDesc.TabIndex = 1;
            btnDesc.Text = "Описание курса";
            btnDesc.TextAlign = ContentAlignment.MiddleLeft;
            btnDesc.UseVisualStyleBackColor = true;
            // 
            // OfflineOnline
            // 
            OfflineOnline.Controls.Add(OnlinePage);
            OfflineOnline.Controls.Add(OfflinePage);
            OfflineOnline.Location = new Point(3, 0);
            OfflineOnline.Name = "OfflineOnline";
            OfflineOnline.SelectedIndex = 0;
            OfflineOnline.Size = new Size(223, 179);
            OfflineOnline.TabIndex = 1;
            // 
            // OnlinePage
            // 
            OnlinePage.AutoScroll = true;
            OnlinePage.Controls.Add(CollegeOnline);
            OnlinePage.Controls.Add(VUZOnline);
            OnlinePage.Controls.Add(AcademyOnline);
            OnlinePage.Location = new Point(4, 24);
            OnlinePage.Name = "OnlinePage";
            OnlinePage.Padding = new Padding(3);
            OnlinePage.Size = new Size(215, 151);
            OnlinePage.TabIndex = 1;
            OnlinePage.Text = "Онлайн";
            OnlinePage.UseVisualStyleBackColor = true;
            OnlinePage.Click += OnlinePage_Click;
            // 
            // CollegeOnline
            // 
            CollegeOnline.Dock = DockStyle.Top;
            CollegeOnline.Font = new Font("Segoe UI", 12F);
            CollegeOnline.Location = new Point(3, 69);
            CollegeOnline.Name = "CollegeOnline";
            CollegeOnline.Size = new Size(209, 33);
            CollegeOnline.TabIndex = 2;
            CollegeOnline.Text = "Колледж";
            CollegeOnline.TextAlign = ContentAlignment.MiddleLeft;
            CollegeOnline.UseVisualStyleBackColor = true;
            // 
            // VUZOnline
            // 
            VUZOnline.Dock = DockStyle.Top;
            VUZOnline.Font = new Font("Segoe UI", 12F);
            VUZOnline.Location = new Point(3, 36);
            VUZOnline.Name = "VUZOnline";
            VUZOnline.Size = new Size(209, 33);
            VUZOnline.TabIndex = 1;
            VUZOnline.Text = "ВУЗ";
            VUZOnline.TextAlign = ContentAlignment.MiddleLeft;
            VUZOnline.UseVisualStyleBackColor = true;
            // 
            // AcademyOnline
            // 
            AcademyOnline.Dock = DockStyle.Top;
            AcademyOnline.Font = new Font("Segoe UI", 12F);
            AcademyOnline.Location = new Point(3, 3);
            AcademyOnline.Name = "AcademyOnline";
            AcademyOnline.Size = new Size(209, 33);
            AcademyOnline.TabIndex = 0;
            AcademyOnline.Text = "Академия";
            AcademyOnline.TextAlign = ContentAlignment.MiddleLeft;
            AcademyOnline.UseVisualStyleBackColor = true;
            // 
            // OfflinePage
            // 
            OfflinePage.Controls.Add(SchoolOffline);
            OfflinePage.Controls.Add(CollegeOffline);
            OfflinePage.Controls.Add(VUZOffline);
            OfflinePage.Controls.Add(AcademyOffline);
            OfflinePage.Location = new Point(4, 24);
            OfflinePage.Name = "OfflinePage";
            OfflinePage.Padding = new Padding(3);
            OfflinePage.Size = new Size(215, 151);
            OfflinePage.TabIndex = 0;
            OfflinePage.Text = "Оффлайн";
            OfflinePage.UseVisualStyleBackColor = true;
            OfflinePage.Click += OfflinePage_Click_1;
            // 
            // SchoolOffline
            // 
            SchoolOffline.Dock = DockStyle.Top;
            SchoolOffline.Font = new Font("Segoe UI", 12F);
            SchoolOffline.Location = new Point(3, 102);
            SchoolOffline.Name = "SchoolOffline";
            SchoolOffline.Size = new Size(209, 33);
            SchoolOffline.TabIndex = 4;
            SchoolOffline.Text = "Школа";
            SchoolOffline.TextAlign = ContentAlignment.MiddleLeft;
            SchoolOffline.UseVisualStyleBackColor = true;
            // 
            // CollegeOffline
            // 
            CollegeOffline.Dock = DockStyle.Top;
            CollegeOffline.Font = new Font("Segoe UI", 12F);
            CollegeOffline.Location = new Point(3, 69);
            CollegeOffline.Name = "CollegeOffline";
            CollegeOffline.Size = new Size(209, 33);
            CollegeOffline.TabIndex = 3;
            CollegeOffline.Text = "Колледж";
            CollegeOffline.TextAlign = ContentAlignment.MiddleLeft;
            CollegeOffline.UseVisualStyleBackColor = true;
            // 
            // VUZOffline
            // 
            VUZOffline.Dock = DockStyle.Top;
            VUZOffline.Font = new Font("Segoe UI", 12F);
            VUZOffline.Location = new Point(3, 36);
            VUZOffline.Name = "VUZOffline";
            VUZOffline.Size = new Size(209, 33);
            VUZOffline.TabIndex = 2;
            VUZOffline.Text = "ВУЗ";
            VUZOffline.TextAlign = ContentAlignment.MiddleLeft;
            VUZOffline.UseVisualStyleBackColor = true;
            // 
            // AcademyOffline
            // 
            AcademyOffline.Dock = DockStyle.Top;
            AcademyOffline.Font = new Font("Segoe UI", 12F);
            AcademyOffline.Location = new Point(3, 3);
            AcademyOffline.Name = "AcademyOffline";
            AcademyOffline.Size = new Size(209, 33);
            AcademyOffline.TabIndex = 1;
            AcademyOffline.Text = "Академия";
            AcademyOffline.TextAlign = ContentAlignment.MiddleLeft;
            AcademyOffline.UseVisualStyleBackColor = true;
            // 
            // actionPanelForm1
            // 
            actionPanelForm1.BorderStyle = BorderStyle.None;
            actionPanelForm1.Location = new Point(3, 37);
            actionPanelForm1.Name = "actionPanelForm1";
            actionPanelForm1.Size = new Size(552, 384);
            actionPanelForm1.TabIndex = 2;
            actionPanelForm1.Text = "";
            // 
            // SearchText
            // 
            SearchText.Location = new Point(100, 7);
            SearchText.Name = "SearchText";
            SearchText.Size = new Size(418, 23);
            SearchText.TabIndex = 1;
            // 
            // Search
            // 
            Search.Location = new Point(3, 4);
            Search.Name = "Search";
            Search.Size = new Size(91, 27);
            Search.TabIndex = 0;
            Search.Text = "🔍 Поиск";
            Search.UseVisualStyleBackColor = true;
            Search.Click += button2_Click_1;
            // 
            // Form2TestButton
            // 
            Form2TestButton.Location = new Point(7, 424);
            Form2TestButton.Name = "Form2TestButton";
            Form2TestButton.Size = new Size(134, 23);
            Form2TestButton.TabIndex = 3;
            Form2TestButton.Text = "форма заполнения";
            Form2TestButton.UseVisualStyleBackColor = true;
            Form2TestButton.Click += Form2TestButton_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(splitContainer1);
            Name = "Form1";
            Text = "Form1";
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel1.PerformLayout();
            splitContainer1.Panel2.ResumeLayout(false);
            splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            optionsPanel.ResumeLayout(false);
            OfflineOnline.ResumeLayout(false);
            OnlinePage.ResumeLayout(false);
            OfflinePage.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private SplitContainer splitContainer1;
        private TabControl OfflineOnline;
        private TabPage OnlinePage;
        private Button CollegeOnline;
        private Button VUZOnline;
        private Button AcademyOnline;
        private TabPage OfflinePage;
        private Button SchoolOffline;
        private Button CollegeOffline;
        private Button VUZOffline;
        private Button AcademyOffline;
        private Button Search;
        private TextBox SearchText;
        private Panel optionsPanel;
        private Button btnQueries;
        private Button btnObjections;
        private Button btnPrice;
        private Button btnDesc;
        private Label label1;
        private RichTextBox actionPanelForm1;
        private Button Form2TestButton;
    }
}
