namespace ManagerHelper
{
    public partial class Form1 : Form
    {
        private string selectedLevel = "";
        private string selectedOption = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void OfflinePage_Click_1(object sender, EventArgs e)
        {

        }

        private void OnlinePage_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }


        private void Form1_Load(object sender, EventArgs e)
        {
            selectedLevel = "";
            selectedOption = "";
        }
        private void GoToEditingButton_Click(object sender, EventArgs e)
        {
            //проверка, выбрана ли карточка
            if (string.IsNullOrEmpty(selectedCardText))
            {
                MessageBox.Show("Пожалуйста, сначала выберите одну из карточек.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //выбрана карточка = передача текста в FillFOrm
            FillForm fillForm = new FillForm(selectedCardText);
            fillForm.ShowDialog();
        }
        private void NotChoosenType_Click(object sender, EventArgs e)
        {

        }

        private void ChoosenType_Click(object sender, EventArgs e)
        {

        }



        //ТИПЫ ОБРАЗОВАНИЯ
        //ОНЛАЙН
        private void VUZOnlineButton_Click(object sender, EventArgs e)
        {
            selectedLevel = "ВУЗ";
            UpdateScreen();
        }

        private void CollegeOnlineButton_Click(object sender, EventArgs e)
        {
            selectedLevel = "Колледж";
            UpdateScreen();
        }

        private void AcademyOnlineButton_Click(object sender, EventArgs e)
        {
            selectedLevel = "Академия";
            UpdateScreen();
        }
        //ОФФЛАЙН
        private void AcademyOfflineButton_Click(object sender, EventArgs e)
        {
            selectedLevel = "Академия";
            UpdateScreen();
        }

        private void VUZOfflineButton_Click(object sender, EventArgs e)
        {
            selectedLevel = "ВУЗ";
            UpdateScreen();
        }

        private void CollegeOfflineButton_Click(object sender, EventArgs e)
        {
            selectedLevel = "Колледж";
            UpdateScreen();
        }

        private void SchoolOfflineButton_Click(object sender, EventArgs e)
        {
            selectedLevel = "Школа";
            UpdateScreen();
        }
        //... возможно будет что-то ещё (маловероятно)
        //---------------------------




        private void SearchTextButton_TextChanged(object sender, EventArgs e)
        {

        }

        private void optionsPanel_Paint(object sender, PaintEventArgs e)
        {

        }




        //КНОПКИ ОПЦИЙ
        private void DescButton_Click(object sender, EventArgs e)
        {
            selectedOption = "Описание курса";
            UpdateScreen();

        }

        private void PriceButton_Click(object sender, EventArgs e)
        {
            selectedOption = "Цены";
            UpdateScreen();
        }

        private void ObjectionsButton_Click(object sender, EventArgs e)
        {
            selectedOption = "Возражения";
            UpdateScreen();
        }

        private void QueriesButton_Click(object sender, EventArgs e)
        {
            selectedOption = "Запросы";
            UpdateScreen();
        }
        //... потом ещё может что-то добавится (веротно)
        //---------------------------




        //обновление экрана при выборе Онлайн или Оффлайн
        private void OfflineOnline_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateScreen();
        }

        //система поиска
        private void SearchButton_Click(object sender, EventArgs e)
        {

        }




        //---------------------------
        //МЕТОДЫ
        private void UpdateScreen()
        {
            string currentTab = OfflineOnline.SelectedTab.Text;

            //надпись что выбрал пользователь ("Онлайн/Оффлайн + тип образования")
            //НЕ ВЫБРАН
            if (string.IsNullOrEmpty(selectedLevel))
            {
                NotChoosenType.Text = "Выберите тип и уровень образования.";
                NotChoosenType.Visible = true;
                SelectedResultLabel.Visible = false;
                ResultsPanel.Controls.Clear(); //чистка карточек
            }
            //выбран
            else
            {
                NotChoosenType.Visible = false;
                ChoosenType.Text = "Выбрано: ";
                SelectedResultLabel.Text = $"{currentTab} {selectedLevel}";
                SelectedResultLabel.Visible = true;

                //пользователь НЕ выбрал опцию
                if (string.IsNullOrEmpty(selectedOption))
                {
                    string keyDesc = $"{currentTab} {selectedLevel}. Описание курса";
                    string keyPrice = $"{currentTab} {selectedLevel}. Цены";

                    //списки текстов и цен
                    List<string> descTexts = GetTextsForCombo(keyDesc);
                    List<string> priceTexts = GetTextsForCombo(keyPrice);

                    //совмещение в один текст
                    List<string> allTexts = new List<string>();
                    allTexts.AddRange(descTexts);
                    allTexts.AddRange(priceTexts);

                    //отрисовка карточек
                    DisplayCards(allTexts);
                }
                //опция ВЫБРАНА
                else
                {
                    string comboKey = $"{currentTab} {selectedLevel}. {selectedOption}";

                    //списки текстов
                    List<string> texts = GetTextsForCombo(comboKey);

                    //отрисовка карточек
                    DisplayCards(texts);
                }
            }
        }

        //---------------------------------------------------------------------------------------------------------
        //карточки текстов ЗАГЛУШКИ
        private string selectedCardText = "";
        private void DisplayCards(List<string> texts)
        {
            //очситка старых карточек
            ResultsPanel.Controls.Clear();
            selectedCardText = ""; //сброс выбора

            foreach (string text in texts)
            {
                //создавние самой карточки (Panel)
                Panel card = new Panel();
                card.Width = ResultsPanel.Width - 40;
                card.Height = 100;
                card.BackColor = Color.White;
                card.BorderStyle = BorderStyle.FixedSingle;
                card.Margin = new Padding(10);

                //создание текста в карточке (Label)
                Label lbl = new Label();
                lbl.Text = text;
                lbl.Dock = DockStyle.Fill;
                lbl.Padding = new Padding(10);
                lbl.Font = new Font("Segoe UI", 10);

                // ВАЖНЫЙ МОМЕНТ: клик по тексту внутри карточки тоже должен работать
                lbl.Click += (sender, e) =>
                {
                    SelectCard(card, text);
                };

                card.Controls.Add(lbl);

                //при выборе карточки
                card.Click += (sender, e) =>
                {
                    SelectCard(card, text);
                };

                //добавка карточки на панель
                ResultsPanel.Controls.Add(card);
            }
        }

        //---------------------------------------------------------------------------------------------------------
        //метод выделения карточки
        private void SelectCard(Panel clickedCard, string text)
        {
            // Сначала возвращаем всем карточкам белый цвет
            foreach (Control c in ResultsPanel.Controls)
            {
                if (c is Panel p) p.BackColor = Color.White;
            }

            // Выделяем ту, на которую нажали
            clickedCard.BackColor = Color.LightGray;

            // Запоминаем текст
            selectedCardText = text;
        }

        //---------------------------------------------------------------------------------------------------------
        //выбор нужного ответа(текста) карточкой + ВРЕМЕННО ТЕКСТЫ ЗАГЛУШКИ
        private List<string> GetTextsForCombo(string comboKey)
        {
            List<string> texts = new List<string>();

            switch (comboKey)
            {
                //карточки с текстом
                case "Онлайн Академия. Цены":
                    texts.Add("Здравствуйте, (1)! Меня зовут (2)\nСтоимость обучения 15 000 рублей. Доступная оплата: (3).");
                    texts.Add("Добрый день, (1)! Для вас действует скидка. Цена — 12 000 рублей. Доступная оплата: (3).");
                    texts.Add("Ознакомьтесь с нашими тарифами. Базовая цена 15 000 руб. Доступная оплата: (3).");
                    texts.Add("Тест для скроллинга");
                    texts.Add("Тест для скроллинга");
                    texts.Add("Тест для скроллинга");
                    break;

                case "Онлайн Академия. Описание курса":
                    texts.Add("ЗАГЛУШКА 1 (Описание): Академия онлайн — это современные IT-курсы...");
                    texts.Add("ЗАГЛУШКА 2 (Описание): У нас вы научитесь программировать с нуля...");
                    break;

                default:
                    texts.Add($"[ЗАГЛУШКА: Текст для '{comboKey}' еще не прописан]");
                    break;
            }
            return texts;
        }
        //---------------------------------------------------------------------------------------------------------
    }
}
