using System.IO;
using System.Windows.Forms;
using Breifico.Algorithms.Formats;

namespace Breifico.ImageEditor
{
    public partial class MainForm : Form
    {
        private IImage _inputImage;

        public MainForm() {
            this.InitializeComponent();
        }

        private void tsmiOpen_Click(object sender, System.EventArgs e) {
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() != DialogResult.OK) {
                return;
            }
            string fileName = openFileDialog.FileName;
            try {
                this._inputImage = new BmpFile(fileName);
                var picture = this._inputImage.ToBitmap();
                this.pbImage.Image = picture;
            } catch (InvalidBmpImageException ex) {
                MessageBox.Show($"BMP processing exception: {ex.Message}");
            } catch (IOException ex) {
                MessageBox.Show($"IO Exception: {ex.Message}");
            }
        }

        private void tsmiInvert_Click(object sender, System.EventArgs e) {

        }
    }
}
