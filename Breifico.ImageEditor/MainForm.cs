using System.IO;
using System.Windows.Forms;
using Breifico.Algorithms.Formats;
using Breifico.Algorithms.Formats.BMP;
using Breifico.Algorithms.ImageProcessing;

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
                this.UpdatePicture();
            } catch (InvalidBmpImageException ex) {
                MessageBox.Show($"BMP processing exception: {ex.Message}");
            } catch (IOException ex) {
                MessageBox.Show($"IO Exception: {ex.Message}");
            }
        }

        private void tsmiInvert_Click(object sender, System.EventArgs e) {
            var transform = new InvertTransformation();
            this._inputImage = transform.Tranform(this._inputImage);
            this.UpdatePicture();
        }

        private void UpdatePicture() {
            this.pbImage.Image = this._inputImage.ToBitmap();
        }
    }
}
