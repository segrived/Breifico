using System.IO;
using System.Windows.Forms;
using Breifico.Algorithms.Formats;
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
                this.UpdatePicture(new BmpFile(fileName));
            } catch (InvalidBmpImageException ex) {
                MessageBox.Show($"BMP processing exception: {ex.Message}");
            } catch (IOException ex) {
                MessageBox.Show($"IO Exception: {ex.Message}");
            }
        }

        private void tsmiInvert_Click(object sender, System.EventArgs e) {
            this.UpdatePicture(new InvertTransformation().Tranform(this._inputImage));
        }

        private void UpdatePicture(IImage image) {
            this._inputImage = image;
            this.pbImage.Image = this._inputImage.ToBitmap();
        }

        private void tsmiSepia_Click(object sender, System.EventArgs e) {
            this.UpdatePicture(new SepiaTransformation().Tranform(this._inputImage));
        }
    }
}
