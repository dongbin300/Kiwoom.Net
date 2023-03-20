using Kiwoom.Net.Clients;

using System;
using System.Windows.Forms;

namespace Kiwoom.Net.Example
{
    public partial class MainForm : Form
    {
        KiwoomClient client;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            client = new KiwoomClient(axKHOpenAPI);
            client.Login();
        }

        private void TestButton_Click(object sender, EventArgs e)
        {
            client.LoadStockItems(Enums.KiwoomMarket.코스닥);
            client.주식기본정보("현우산업");
        }
    }
}
