namespace WinFormsApp1
{
	public partial class ProductEditForm : Form
	{
		private string _currentArticle;

		public ProductEditForm(string article)
		{
			InitializeComponent();
			_currentArticle = article;
		}

		public ProductEditForm()
		{
			InitializeComponent();
		}
        public void Init(string article)
        {
            _currentArticle = article;
        }
        private void ProductEditForm_Load(object sender, EventArgs e)
		{
			using (ApplicationDbContext db = new ApplicationDbContext())
			{
				var categories = db.Categories
					.GroupBy(c => c.Name)
					.Select(g => g.First())
					.ToList();

				cmbCategory.DataSource = categories;
				cmbCategory.DisplayMember = "Name";
				cmbCategory.ValueMember = "Id";
				cmbCategory.DropDownStyle = ComboBoxStyle.DropDownList;

				var product = db.Products.FirstOrDefault(p => p.Article == _currentArticle);

				if (product != null)
				{
					txtArticle.Text = product.Article;
					txtArticle.Enabled = false;
					txtBrand.Text = product.Brand;
					txtName.Text = product.Name;
					cmbCategory.SelectedValue = product.CategoryId;
					LoadSizesByCategory(product.CategoryId, product.Size);
				}
				else
				{
					MessageBox.Show("Товар не найден!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
					this.Close();
				}
			}
		}

		private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (cmbCategory.SelectedItem == null) return;

			Category selectedCategory = (Category)cmbCategory.SelectedItem;

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

		private void LoadSizesByCategory(int categoryId, string currentSize)
		{
			using (ApplicationDbContext db = new ApplicationDbContext())
			{
				var category = db.Categories.Find(categoryId);
				if (category != null)
				{
					string categoryName = category.Name.ToLower();

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

					if (cmbSize.Items.Contains(currentSize))
					{
						cmbSize.Text = currentSize;
					}
					else
					{
						cmbSize.Items.Add(currentSize);
						cmbSize.Text = currentSize;
						cmbSize.Items.Remove(currentSize);
					}
				}
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
			cmbSize.DropDownStyle = ComboBoxStyle.DropDownList;
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
			cmbSize.DropDownStyle = ComboBoxStyle.DropDownList;
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
			cmbSize.DropDownStyle = ComboBoxStyle.DropDownList;
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
			cmbSize.DropDownStyle = ComboBoxStyle.DropDownList;
			cmbSize.SelectedIndex = 0;
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			string brand = txtBrand.Text.Trim();
			string name = txtName.Text.Trim();
			string size = cmbSize.Text.Trim();

			if (string.IsNullOrEmpty(brand) || string.IsNullOrEmpty(name))
			{
				MessageBox.Show("Бренд и Название не могут быть пустыми!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			if (size == "Выберите размер" || string.IsNullOrEmpty(size))
			{
				MessageBox.Show("Пожалуйста, выберите размер из списка!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			if ((int)cmbCategory.SelectedValue == 0)
			{
				MessageBox.Show("Пожалуйста, выберите категорию!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			using (ApplicationDbContext db = new ApplicationDbContext())
			{
				var product = db.Products.FirstOrDefault(p => p.Article == _currentArticle);
				if (product != null)
				{
					product.Brand = brand;
					product.Name = name;
					product.Size = size;
					product.CategoryId = (int)cmbCategory.SelectedValue;

					db.SaveChanges();

					MessageBox.Show("Данные товара успешно обновлены!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
					this.Close();
				}
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
