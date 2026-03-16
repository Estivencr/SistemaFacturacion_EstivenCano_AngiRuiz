using System;
using System.Windows.Forms;

namespace Pantallas_Sistema_facturación_EstivenCano_AngiRuiz
{
    public partial class FrmAyuda : Form
    {
        private const string HelpUrl = "https://learn.microsoft.com/es-es/dotnet/csharp/";
        private readonly WebBrowser _webBrowser;

        public FrmAyuda()
        {
            InitializeComponent();

            Text = "Ayuda";

            _webBrowser = new WebBrowser
            {
                Dock = DockStyle.Fill,
                ScriptErrorsSuppressed = true
            };

            Controls.Add(_webBrowser);
            Load += FrmAyuda_Load;
        }

        private void FrmAyuda_Load(object sender, EventArgs e)
        {
            try
            {
                _webBrowser.Navigate(HelpUrl);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudo cargar la página de ayuda.\n\n" + ex.Message,
                    "Ayuda",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
