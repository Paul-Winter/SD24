using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ManagerHelper
{
    public partial class FillForm : Form
    {
        private string _templateText;
        public FillForm(string text)
        {
            InitializeComponent();
            _templateText = text;
            actionPanelForm2.Text = _templateText;
        }

        private void FillForm_Load(object sender, EventArgs e)
        {

        }

        private void NameClient_Click(object sender, EventArgs e)
        {

        }

        private void NameClientText_TextChanged(object sender, EventArgs e)
        {

        }

        private void NameManager_Click(object sender, EventArgs e)
        {

        }

        private void NameManagerText_TextChanged(object sender, EventArgs e)
        {

        }

        private void Payment_Click(object sender, EventArgs e)
        {

        }

        private void PaymentCheckBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void FillButton_Click(object sender, EventArgs e)
        {
            string clientName = NameClientText.Text;
            string managerName = NameManagerText.Text;

            if (string.IsNullOrWhiteSpace(clientName) || string.IsNullOrWhiteSpace(managerName))
            {
                MessageBox.Show("Заполните ФИО клиента/менеджера", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            List<string> selectedPayments = new List<string>();
            foreach (var item in PaymentCheckBox.CheckedItems)
            {
                selectedPayments.Add(item?.ToString() ?? "");
            }

            string paymentText = selectedPayments.Count > 0 ? string.Join(", ", selectedPayments) : "не выбрана";

            string resultText = _templateText;
            resultText = resultText.Replace("(1)", clientName);
            resultText = resultText.Replace("(2)", managerName);
            resultText = resultText.Replace("(3)", paymentText);

            actionPanelForm2.Text = resultText;
        }

        private void CopyButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(actionPanelForm2.Text))
            {
                Clipboard.SetText(actionPanelForm2.Text);
            }
        }

        private void actionPanelForm2_TextChanged(object sender, EventArgs e)
        {

        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
