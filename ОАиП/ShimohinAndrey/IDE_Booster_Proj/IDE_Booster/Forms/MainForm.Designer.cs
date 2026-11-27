using System.Xml;

namespace IDE_Booster
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer? components = null;

        private Button selectButton = null!;
        private Button launchButton = null!;
        private Label nameLabel = null!;
        private Label pathLabel = null!;
        private Button boostButton = null!;
        private Button stopButton = null!;
        private Label statusLabel = null!;
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
            selectButton = new Button();
            launchButton = new Button();
            nameLabel = new Label();
            pathLabel = new Label();
            boostButton = new Button();
            stopButton = new Button();
            statusLabel = new Label();

            SuspendLayout();

            // selectButton
            selectButton.Location = new Point(30, 30);
            selectButton.Size = new Size(150, 40);
            selectButton.Text = "Выбрать IDE";
            selectButton.UseVisualStyleBackColor = true;
            selectButton.Click += selectButton_Click;

            // launchButton
            launchButton.Location = new Point(200, 30);
            launchButton.Size = new Size(150, 40);
            launchButton.Text = "Запустить IDE";
            launchButton.UseVisualStyleBackColor = true;
            launchButton.Click += launchButton_Click;

            // nameLabel
            nameLabel.AutoSize = true;
            nameLabel.Location = new Point(30, 100);
            nameLabel.Text = "IDE не выбрана";

            // pathLabel
            pathLabel.AutoSize = true;
            pathLabel.Location = new Point(30, 130);
            pathLabel.Text = "Путь: отсутствует";

            // MainForm
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(600, 350);
            Controls.Add(selectButton);
            Controls.Add(launchButton);
            Controls.Add(nameLabel);
            Controls.Add(pathLabel);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "IDE Booster";

            // boostButton
            boostButton.Location = new Point(30, 190);
            boostButton.Size = new Size(150, 40);
            boostButton.Text = "Запустить буст";
            boostButton.UseVisualStyleBackColor = true;
            boostButton.Click += boostButton_Click;

            // stopButton
            stopButton.Location = new Point(200, 190);
            stopButton.Size = new Size(150, 40);
            stopButton.Text = "Остановить буст";
            stopButton.UseVisualStyleBackColor = true;
            stopButton.Click += stopButton_Click;

            // statusLabel
            statusLabel.AutoSize = true;
            statusLabel.Location = new Point(30, 250);
            statusLabel.Text = "Статус: ожидание";

            // Добавляем элементы
            Controls.Add(boostButton);
            Controls.Add(stopButton);
            Controls.Add(statusLabel);

            ResumeLayout(false);
            PerformLayout();
        }
    }

        #endregion
}
