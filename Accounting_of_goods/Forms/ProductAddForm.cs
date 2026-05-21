namespace WinFormsApp1
{
	public partial class ProductAddForm : Form
	{
		public ProductAddForm()
		{
			InitializeComponent();
		}

		private void ProductAddForm_Load(object sender, EventArgs e)
		{
			using (ApplicationDbContext db = new ApplicationDbContext())
			{
				var categories = db.Categories
					.GroupBy(c => c.Name)
					.Select(g => g.First())
					.ToList();

				categories.Insert(0, new Category { Id = 0, Name = "Выберите категорию" });

				cmbCategory.DataSource = categories;
				cmbCategory.DisplayMember = "Name";
				cmbCategory.ValueMember = "Id";
				cmbCategory.DropDownStyle = ComboBoxStyle.DropDownList;

				cmbSize.Items.Clear();
				cmbSize.Items.Add("Выберите категорию");
				cmbSize.SelectedIndex = 0;
				cmbSize.DropDownStyle = ComboBoxStyle.DropDownList;
				cmbSize.Enabled = false;
			}
		}

		private void txtBrand_TextChanged(object sender, EventArgs e)
		{
			if (!string.IsNullOrWhiteSpace(txtBrand.Text))
			{
				txtArticle.Text = GenerateArticle(txtBrand.Text);
			}
			else
			{
				txtArticle.Text = "";
			}
		}

		private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (cmbCategory.SelectedItem == null) return;

			Category selectedCategory = (Category)cmbCategory.SelectedItem;

			if (selectedCategory.Id == 0)
			{
				cmbSize.Items.Clear();
				cmbSize.Items.Add("Выберите категорию сначала");
				cmbSize.SelectedIndex = 0;
				cmbSize.Enabled = false;
				return;
			}

			cmbSize.Enabled = true;

			string categoryName = selectedCategory.Name.ToLower();

			if (categoryName.Contains("футболк") ||
				categoryName.Contains("толстовк") ||
				categoryName.Contains("худи") ||
				categoryName.Contains("свитшот"))
			{
				LoadClothingSizes();
			}
			else if (categoryName.Contains("джинс") ||
					 categoryName.Contains("брюк") ||
					 categoryName.Contains("штан"))
			{
				LoadPantsSizes();
			}
			else if (categoryName.Contains("обув") ||
					 categoryName.Contains("кроссовк") ||
					 categoryName.Contains("туфл") ||
					 categoryName.Contains("ботин"))
			{
				LoadShoesSizes();
			}
			else
			{
				LoadUniversalSizes();
			}
		}

		private void LoadClothingSizes()
		{
			string[] sizes = new string[]
			{
				"Выберите размер",
				"XS", "S", "M", "L", "XL", "XXL", "XXXL"
			};

			cmbSize.Items.Clear();
			cmbSize.Items.AddRange(sizes);
			cmbSize.SelectedIndex = 0;
		}

		private void LoadPantsSizes()
		{
			string[] sizes = new string[]
			{
				"Выберите размер",
				"40", "42", "44", "46", "48", "50", "52", "54", "56",
				"58", "60", "62", "64"
			};

			cmbSize.Items.Clear();
			cmbSize.Items.AddRange(sizes);
			cmbSize.SelectedIndex = 0;
		}

		private void LoadShoesSizes()
		{
			string[] sizes = new string[]
			{
				"Выберите размер",
				"35", "36", "37", "38", "39", "40", "41", "42", "43",
				"44", "45", "46", "47", "48", "49", "50"
			};

			cmbSize.Items.Clear();
			cmbSize.Items.AddRange(sizes);
			cmbSize.SelectedIndex = 0;
		}

		private void LoadUniversalSizes()
		{
			string[] sizes = new string[]
			{
				"Выберите размер",
				"XS", "S", "M", "L", "XL", "XXL", "XXXL",
				"40", "42", "44", "46", "48", "50", "52", "54", "56",
				"35", "36", "37", "38", "39", "40", "41", "42", "43",
				"44", "45", "46", "47"
			};

			cmbSize.Items.Clear();
			cmbSize.Items.AddRange(sizes);
			cmbSize.SelectedIndex = 0;
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			string article = txtArticle.Text.Trim();
			string brand = txtBrand.Text.Trim();
			string name = txtName.Text.Trim();
			string size = cmbSize.Text.Trim();

			if (string.IsNullOrEmpty(article) || string.IsNullOrEmpty(brand) || string.IsNullOrEmpty(name))
			{
				MessageBox.Show("Заполните все текстовые поля (Артикул, Бренд, Название)!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			if ((int)cmbCategory.SelectedValue == 0)
			{
				MessageBox.Show("Пожалуйста, выберите категорию из списка!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			if (size == "Выберите размер" || size == "Выберите категорию сначала" || string.IsNullOrEmpty(size))
			{
				MessageBox.Show("Пожалуйста, выберите размер из списка!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			using (ApplicationDbContext db = new ApplicationDbContext())
			{
				if (db.Products.Any(p => p.Article == article))
				{
					MessageBox.Show("Товар с таким артикулом уже существует в базе!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}

				Product newProduct = new Product
				{
					Article = article,
					Brand = brand,
					Name = name,
					Size = size,
					PurchasePrice = 0,
					CategoryId = (int)cmbCategory.SelectedValue,
					CurrentStock = 0
				};

				db.Products.Add(newProduct);
				db.SaveChanges();

				MessageBox.Show("Товар успешно добавлен в справочник!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
				this.Close();
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void txtName_Leave(object sender, EventArgs e)
		{
			if (!string.IsNullOrWhiteSpace(txtBrand.Text))
			{
				txtArticle.Text = GenerateArticle(txtBrand.Text);
			}
		}

		private string GenerateArticle(string brand)
		{
			string prefix = new string(brand.Where(char.IsLetter).ToArray()).ToUpper();
			if (prefix.Length > 3) prefix = prefix.Substring(0, 3);
			if (prefix.Length == 0) prefix = "ITM";

			using (ApplicationDbContext db = new ApplicationDbContext())
			{
				var existingArticles = db.Products
					.Where(p => p.Article.StartsWith(prefix + "-"))
					.Select(p => p.Article)
					.ToList();

				int maxNumber = 0;
				foreach (var art in existingArticles)
				{
					var parts = art.Split('-');
					if (parts.Length == 2 && int.TryParse(parts[1], out int num))
					{
						if (num > maxNumber) maxNumber = num;
					}
				}

				return $"{prefix}-{maxNumber + 1:D3}";
			}
		}
	}
}
