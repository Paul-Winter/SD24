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
            GoToEditingButton = new Button();
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
            NotChoosenType = new Label();
            ResultsPanel = new FlowLayoutPanel();
            SelectedResultLabel = new Label();
            ChoosenType = new Label();
            SearchText = new TextBox();
            Search = new Button();
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
            splitContainer1.Panel1.Controls.Add(GoToEditingButton);
            splitContainer1.Panel1.Controls.Add(label1);
            splitContainer1.Panel1.Controls.Add(optionsPanel);
            splitContainer1.Panel1.Controls.Add(OfflineOnline);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(NotChoosenType);
            splitContainer1.Panel2.Controls.Add(ResultsPanel);
            splitContainer1.Panel2.Controls.Add(SelectedResultLabel);
            splitContainer1.Panel2.Controls.Add(ChoosenType);
            splitContainer1.Panel2.Controls.Add(SearchText);
            splitContainer1.Panel2.Controls.Add(Search);
            splitContainer1.Size = new Size(800, 450);
            splitContainer1.SplitterDistance = 229;
            splitContainer1.TabIndex = 2;
            // 
            // GoToEditingButton
            // 
            GoToEditingButton.BackColor = SystemColors.ActiveCaption;
            GoToEditingButton.Font = new Font("Segoe UI", 10F);
            GoToEditingButton.Location = new Point(7, 408);
            GoToEditingButton.Name = "GoToEditingButton";
            GoToEditingButton.Size = new Size(212, 33);
            GoToEditingButton.TabIndex = 3;
            GoToEditingButton.Text = "Ввод данных";
            GoToEditingButton.UseVisualStyleBackColor = false;
            GoToEditingButton.Click += GoToEditingButton_Click;
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
            optionsPanel.Size = new Size(215, 202);
            optionsPanel.TabIndex = 2;
            optionsPanel.Paint += optionsPanel_Paint;
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
            btnQueries.Click += QueriesButton_Click;
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
            btnObjections.Click += ObjectionsButton_Click;
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
            btnPrice.Click += PriceButton_Click;
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
            btnDesc.Click += DescButton_Click;
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
            OfflineOnline.SelectedIndexChanged += OfflineOnline_SelectedIndexChanged;
            OfflineOnline.Click += OfflinePage_Click_1;
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
            CollegeOnline.Click += CollegeOnlineButton_Click;
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
            VUZOnline.Click += VUZOnlineButton_Click;
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
            AcademyOnline.Click += AcademyOnlineButton_Click;
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
            SchoolOffline.Click += SchoolOfflineButton_Click;
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
            CollegeOffline.Click += CollegeOfflineButton_Click;
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
            VUZOffline.Click += VUZOfflineButton_Click;
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
            AcademyOffline.Click += AcademyOfflineButton_Click;
            // 
            // NotChoosenType
            // 
            NotChoosenType.AutoSize = true;
            NotChoosenType.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            NotChoosenType.Location = new Point(13, 41);
            NotChoosenType.Name = "NotChoosenType";
            NotChoosenType.Size = new Size(232, 15);
            NotChoosenType.TabIndex = 5;
            NotChoosenType.Text = "Выберите тип и уровень образования.";
            NotChoosenType.Click += NotChoosenType_Click;
            // 
            // ResultsPanel
            // 
            ResultsPanel.AutoScroll = true;
            ResultsPanel.Location = new Point(13, 60);
            ResultsPanel.Name = "ResultsPanel";
            ResultsPanel.Size = new Size(542, 361);
            ResultsPanel.TabIndex = 7;
            // 
            // SelectedResultLabel
            // 
            SelectedResultLabel.AutoSize = true;
            SelectedResultLabel.Location = new Point(74, 41);
            SelectedResultLabel.Name = "SelectedResultLabel";
            SelectedResultLabel.Size = new Size(0, 15);
            SelectedResultLabel.TabIndex = 6;
            SelectedResultLabel.Visible = false;
            // 
            // ChoosenType
            // 
            ChoosenType.AutoSize = true;
            ChoosenType.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            ChoosenType.Location = new Point(13, 41);
            ChoosenType.Name = "ChoosenType";
            ChoosenType.Size = new Size(65, 15);
            ChoosenType.TabIndex = 4;
            ChoosenType.Text = "Выбрано: ";
            ChoosenType.Click += ChoosenType_Click;
            // 
            // SearchText
            // 
            SearchText.Location = new Point(100, 7);
            SearchText.Name = "SearchText";
            SearchText.Size = new Size(418, 23);
            SearchText.TabIndex = 1;
            SearchText.TextChanged += SearchTextButton_TextChanged;
            // 
            // Search
            // 
            Search.Location = new Point(3, 4);
            Search.Name = "Search";
            Search.Size = new Size(91, 27);
            Search.TabIndex = 0;
            Search.Text = "🔍 Поиск";
            Search.UseVisualStyleBackColor = true;
            Search.Click += SearchButton_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(splitContainer1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
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
        private Label ChoosenType;
        private Label NotChoosenType;
        private Label SelectedResultLabel;
        private FlowLayoutPanel ResultsPanel;
        private Button GoToEditingButton;
    }
}
