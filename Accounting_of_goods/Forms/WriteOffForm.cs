namespace WinFormsApp1
{
    public partial class WriteOffForm : Form
    {
        private readonly IWriteOffService _writeOffService;
        private int _userId;
        private string _currentArticle;

        public WriteOffForm(IWriteOffService writeOffService)
        {
            InitializeComponent();
            _writeOffService = writeOffService;
        }

        public void Init(int userId, string article = null)
        {
            _userId = userId;
            _currentArticle = article;
        }

        private void WriteOffForm_Load(object sender, EventArgs e)
        {
            cmbProduct.DataSource = _writeOffService.GetProductNames();
            cmbProduct.SelectedIndex = -1;

            if (!string.IsNullOrEmpty(_currentArticle))
            {
                var prodInfo = _writeOffService.GetProductByArticle(_currentArticle);
                if (prodInfo != null)
                {
                    dynamic d = prodInfo;
                    cmbProduct.SelectedItem = d.Name;
                    cmbSize.SelectedItem = d.Size;
                }
            }
        }

        private void cmbProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProduct.SelectedItem == null) return;
            cmbSize.DataSource = _writeOffService.GetSizesForProduct(cmbProduct.Text);
        }

        private void cmbSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSize.SelectedItem == null) return;

            var details = _writeOffService.GetProductDetails(cmbProduct.Text, cmbSize.Text);
            if (details != null)
            {
                dynamic d = details;
                _currentArticle = d.Article;
                lblAvailable.Text = d.CurrentStock.ToString();
            }
        }

        private void btnWriteOff_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_currentArticle))
            {
                MessageBox.Show("РџРѕР¶Р°Р»СѓР№СЃС‚Р°, РІС‹Р±РµСЂРёС‚Рµ С‚РѕРІР°СЂ Рё СЂР°Р·РјРµСЂ!", "Р’РЅРёРјР°РЅРёРµ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int qty = (int)numQty.Value;

            if (qty <= 0)
            {
                MessageBox.Show("РЈРєР°Р¶РёС‚Рµ РєРѕР»РёС‡РµСЃС‚РІРѕ РґР»СЏ СЃРїРёСЃР°РЅРёСЏ!", "Р’РЅРёРјР°РЅРёРµ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string reason = txtReason.Text.Trim(); 
            if (string.IsNullOrEmpty(reason))
            {
                MessageBox.Show("РЈРєР°Р¶РёС‚Рµ РїСЂРёС‡РёРЅСѓ СЃРїРёСЃР°РЅРёСЏ!", "Р’РЅРёРјР°РЅРёРµ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int currentUserId = _userId > 0 ? _userId : 1;

                _writeOffService.ProcessWriteOff(currentUserId, _currentArticle, qty, reason);
                MessageBox.Show("РўРѕРІР°СЂ СѓСЃРїРµС€РЅРѕ СЃРїРёСЃР°РЅ!", "РЈСЃРїРµС…", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("РћС€РёР±РєР°: " + ex.Message, "РћС€РёР±РєР°", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
